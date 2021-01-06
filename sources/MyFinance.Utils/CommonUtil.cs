using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MyFinance.Utils
{
    public static class CommonUtil
    {
        private static readonly long UnixEpochTicks = (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;
        public static string ToName(this Enum value)
        {
            var type = value.GetType();
            if (!type.IsEnum) throw new ArgumentException(String.Format("Type '{0}' is not Enum", type));

            var members = type.GetMember(value.ToString());
            if (members.Length == 0) throw new ArgumentException(String.Format("Member '{0}' not found in type '{1}'", value, type.Name));

            var member = members[0];
            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);
            if (attributes.Length == 0) throw new ArgumentException(String.Format("'{0}.{1}' doesn't have DisplayAttribute", type.Name, value));

            var attribute = (DisplayAttribute)attributes[0];
            return attribute.GetName();
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }
        public static long? ToJsonTicks(this DateTime? value)
        {
            return value == null ? (long?)null : (value.Value.ToUniversalTime().Ticks - UnixEpochTicks) / 10000;
        }
        public static string ToStringVN(this DateTime Date)
        {
            return Date.ToString(@"dd/MM/yyyy hh:mm:ss tt", new CultureInfo("en-US"));
        }
        public static string ToStringDateVN(this DateTime Date)
        {

            return string.Format("{0}", string.Format("{0}/{1}/{2}",
                                                    Date.Day < 10 ? "0" + Date.Day : Date.Day.ToString(),
                                                    Date.Month < 10 ? "0" + Date.Month : Date.Month.ToString(),
                                                    Date.Year));
        }
        public static string ToStringTimeVN(this DateTime Date)
        {

            return string.Format("{0}", string.Format("{0} giờ: {1} phút ",
                                                    Date.Hour,
                                                    Date.Minute));
        }
        public static DateTime ToMidDate(this DateTime Date)
        {

            return new DateTime(Date.Year, Date.Month, Date.Day);
        }
        public static DateTime ToMaxDate(this DateTime Date)
        {

            return new DateTime(Date.Year, Date.Month, Date.Day, 23, 59, 00);
        }
        public static DateTime ToMinDate(this DateTime Date)
        {

            return new DateTime(Date.Year, Date.Month, Date.Day, 00, 00, 00);
        }

        public static DateTime ToWorkingDate(this DateTime Date)
        {

            return new DateTime(Date.Year, Date.Month, Date.Day, 07, 00, 00);
        }

       
        public static string ConvertDayOfWeekVN(DayOfWeek data)
        {
            string result = string.Empty;
            switch (data)
            {
                case DayOfWeek.Friday:
                    result = "Thứ 6";
                    break;
                case DayOfWeek.Monday:
                    result = "Thứ 2";
                    break;
                case DayOfWeek.Saturday:
                    result = "Thứ 7";
                    break;
                case DayOfWeek.Sunday:
                    result = "Chủ nhật";
                    break;
                case DayOfWeek.Thursday:
                    result = "Thứ 5";
                    break;
                case DayOfWeek.Tuesday:
                    result = "Thứ 3";
                    break;
                case DayOfWeek.Wednesday:
                    result = "Thứ 4";
                    break;

            }
            return result;
        }


        public static string ToAscii(this string str)
        {
            if (string.IsNullOrWhiteSpace(str)) return null;
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            str = str.Normalize(NormalizationForm.FormD);
            str = regex.Replace(str, String.Empty)
                        .Replace('\u0111', 'd').Replace('\u0110', 'D');

            //Remove Special Char
            regex = new Regex(@"[^a-zA-Z0-9_\.]");
            str = regex.Replace(str, "_");

            return str;
        }

        public static string UploadFileEx(Stream file, string fileName, string serviceUrl)
        {

            var fileFormName = "file";
            var contenttype = "application/octet-stream";

            //string postdata = "?";

            //if (formValues != null)
            //{
            //    foreach (string key in formValues.Keys)
            //    {
            //        postdata += key + "=" + formValues.Get(key) + "&";
            //    }
            //}
            Uri uri = new Uri(serviceUrl);//+ postdata

            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
            webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webrequest.Method = "POST";

            // Build up the post message header
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append(fileFormName);
            sb.Append("\"; filename=\"");
            sb.Append(fileName);
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append(contenttype);
            sb.Append("\r\n");
            sb.Append("\r\n");

            string postHeader = sb.ToString();

            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            // Build the trailing boundary string as a byte array
            // ensuring the boundary appears on a line by itself
            byte[] boundaryBytes =
                   Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            long length = postHeaderBytes.Length + file.Length +
                                                   boundaryBytes.Length;
            webrequest.ContentLength = length;

            Stream requestStream = webrequest.GetRequestStream();

            // Write out our post header
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            // Write out the file contents
            byte[] buffer = new Byte[checked((uint)Math.Min(4096,
                                     (int)file.Length))];
            int bytesRead = 0;
            while ((bytesRead = file.Read(buffer, 0, buffer.Length)) != 0)
                requestStream.Write(buffer, 0, bytesRead);

            // Write out the trailing boundary
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            WebResponse responce = webrequest.GetResponse();
            Stream s = responce.GetResponseStream();
            StreamReader sr = new StreamReader(s);

            return sr.ReadToEnd();
        }

        public static string ToJson(Type enumType)
        {
            Dictionary<int, string> listEnumField = new Dictionary<int, string>();
            Type type = enumType;
            foreach (var evalue in type.GetEnumValues())
            {
                var valueName = type.GetField(evalue.ToString());
                string displayLabel = "";
                DisplayAttribute displayAtt = valueName.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                if (displayAtt != null)
                    displayLabel = displayAtt.GetName();
                listEnumField.Add((int)evalue, displayLabel);
            }
            return JsonConvert.SerializeObject(listEnumField.Select(m => new { Key = m.Key, Value = m.Value }));
        }
        public static string ToJsonInt(Type enumType)
        {
            Dictionary<int, string> listEnumField = new Dictionary<int, string>();
            Type type = enumType;
            foreach (var evalue in type.GetEnumValues())
            {
                var valueName = type.GetField(evalue.ToString());
                string displayLabel = "";
                DisplayAttribute displayAtt = valueName.GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
                if (displayAtt != null)
                    displayLabel = displayAtt.GetName();
                listEnumField.Add((int)evalue, displayLabel);
            }
            return JsonConvert.SerializeObject(listEnumField.Select(m => new { Key = m.Key, Value = m.Value }));
        }
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
        public static DateTime GetFirstDayOfWeek(this DateTime date)
        {
            var firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;

            while (date.DayOfWeek != firstDayOfWeek)
            {
                date = date.AddDays(-1);
            }

            return date;
        }

    
        public static DateTime GetFirstDayOfMonth(this DateTime Date)
        {
            DateTime dtResult = new DateTime(Date.Year, Date.Month, 1);
            dtResult = dtResult.AddDays((-dtResult.Day) + 1);
            return dtResult;
        }
        public static DateTime GetLastDayOfMonth(this DateTime dtInput)
        {
            DateTime dtResult = dtInput;
            dtResult = dtResult.AddMonths(1);
            dtResult = dtResult.AddDays(-(dtResult.Day));
            return dtResult;
        }
        public static TimeSpan ToTimeSpan(int hour)
        {
            return new TimeSpan(hour, 0, 0);
        }
    }
}
