using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Inside.Infractstructure
{
    public class JsonCommonResult<T> : JsonCommonResult
    {
        public T Data { get; set; }
    }

    public class JsonCommonResult
    {
        public bool IsError { get; set; }
        public string Message { get; set; }

        public static JsonResult CreateError(string message)
        {
            JsonCommonResult result = new JsonCommonResult();
            if (message != null)
                result.IsError = true;
            else
                result.IsError = false;
            result.Message = message;
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.DenyGet };
        }




      
        //public static JsonResult ToJsonResult<T, G>(this Response<G> response, T withData)
        //{
        //    JsonCommonResult<T> result = new JsonCommonResult<T>();
        //    result.Data = withData;
        //    result.IsError = response.HasError;
        //    result.Message = response.ToErrorMsg();
        //    return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.DenyGet };
        //}
    }


    public static class Utils
    {
        public static JsonResult ToJsonResult<T>(this Response<T> response, T withData)
        {
            JsonCommonResult<T> result = new JsonCommonResult<T>();
            result.Data = withData;
            result.IsError = response.HasError;
            result.Message = response.ToErrorMsg();
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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

        public static string ToStringVN( this DateTime Date)
        {
            
            return string.Format("{0}", string.Format("{0}/{1}/{2}",
                                                    Date.Day < 10 ? "0" + Date.Day : Date.Day.ToString(),
                                                    Date.Month < 10 ? "0" + Date.Month : Date.Month.ToString(),
                                                    Date.Year));
        }
       
    }
}