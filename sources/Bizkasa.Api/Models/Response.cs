using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bizkasa.Api.Models
{
    public class Response
    {
        public Response()
        {
            TimeStamp = Common.MobileHelper.ToUnixTime(DateTime.Now);
        }

        public bool Flag { get; set; }
        public double TimeStamp { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
    public class BaseRequest
    {
        public string TokenId { get; set; }
        public string HashKey { get; set; }
        public double TimeStamp { get; set; }
    }
    public class UploadRequest
    {
        public HttpPostedFileBase File { get; set; }
       
    }
}