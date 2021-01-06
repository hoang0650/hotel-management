using MyFinance.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Inside.Infractstructure
{
    public static class UrlHelper
    {
        public static string GetHomeUrl(this Controller controller)
        {

            var principal = controller.HttpContext.User;

            if (principal != null && principal.Identity.IsAuthenticated)
            {
                var wct = WorkContext.BizKasaContext;
                if ( wct!=null &&!string.IsNullOrWhiteSpace(wct.UserName))
                {
                    return controller.Url.Action("Index", "Dashboard", null, controller.Request.Url.Scheme);
                    
                }

              

            }

            return controller.Url.Action("Logon", "Home", null, controller.Request.Url.Scheme);
        }
    }
}