using MyFinance.Bizkasa.Infractstructure;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Bizkasa
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
