using Bizkasa.Api.Infractstructure;
using System.Web;
using System.Web.Mvc;

namespace Bizkasa.Api
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionHandler());
        }
    }
}
