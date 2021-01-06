
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace Bizkasa.Api.Controllers
{
     [RoutePrefix("api/Upload")]  
    public class UpLoadController : ApiController
    {
        [Route("Gallery")]
        [HttpPost]
         public IHttpActionResult Single()
        {
            return Ok(IUploadImage());
        }
        public Response IUploadImage()
        {
            string url = string.Empty;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files["file"];

                if (httpPostedFile != null)
                {
                    string fileName = httpPostedFile.FileName.ToAscii();
                    // Validate the uploaded image(optional)
                    bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                    if (!folderExists)
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                    var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedDocuments"), fileName);
                    httpPostedFile.SaveAs(fileSavePath);

                    url = "/UploadedDocuments/" + fileName;
                }
            }
            var result = new Response<string> {
                Data = url
            };
            return result;
        }
    }
}
