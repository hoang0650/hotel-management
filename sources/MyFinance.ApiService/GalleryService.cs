﻿using MyFinance.Business;
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ApiService
{
    public interface IGalleryService
    {
        Response<string> UploadFile(Stream fileStream, string fileName, string code);
    }
    public partial class TikasaService
    {
        public Response<string> UploadFile(Stream fileStream, string fileName, string code)
        {

            string result = string.Empty;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IGalleryBusiness>().UploadFile(fileStream, fileName,code);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
