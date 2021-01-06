using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Utils
{
   public class ConfigKey
    {
       public static readonly string APIUploadImage = ConfigurationManager.AppSettings["APIUploadImage"];
        public static readonly string URLAPI = ConfigurationManager.AppSettings["URLAPI"];

    }
}
