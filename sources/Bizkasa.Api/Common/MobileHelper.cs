using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Bizkasa.Api.Common
{
    public static class MobileHelper
    {
        public static DateTime? ToUTC(this DateTime? date)
        {
            if (date.HasValue == false) return date;

            return date.Value.ToUTC();
        }

        public static DateTime ToUTC(this DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return DateTime.MinValue;
            }

            var dateOffset = new DateTimeOffset(date, TimeZoneInfo.Local.GetUtcOffset(date));
            return dateOffset.UtcDateTime;
        }

        public static DateTime ToDateTime(this double unixTimeStamp)
        {
            if (unixTimeStamp == 0.0) return DateTime.MinValue;

            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return dtDateTime;
        }

        public static double ToUnixTime(this DateTime dateTime)
        {
            return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        }

        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }
    }
}