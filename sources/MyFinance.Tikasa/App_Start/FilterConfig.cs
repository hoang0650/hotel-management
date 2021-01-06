using MyFinance.Tikasa.Infractstructure;
using System.Web.Mvc;

namespace MyFinance.Tikasa
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
