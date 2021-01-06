using MyFinance.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Proxy
{
    public interface IGalleryProxyService : IBusinessBase
    {
        string UploadFile(Stream fileStream, string fileName, string code);
    }
    public class GalleryProxyService : BaseProxyService, IGalleryProxyService
    {
        public string UploadFile(Stream fileStream, string fileName, string code)
        {
            string url = "api/Gallery/UploadFile";
            return PostService<string>(new { FileStream = fileStream, FileName = fileName ,Code=code}, url);
        }
    }
}
