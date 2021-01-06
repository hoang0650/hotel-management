using MyFinance.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seller.Tikasa.Infractstructure
{
    public static class UrlHelper
    {
        public static string GetHomeUrl(this Controller controller)
        {

            var principal = controller.HttpContext.User;

            if (principal != null && principal.Identity.IsAuthenticated)
            {
                var wct = WorkContext.BizKasaContext;
                if (wct.HotelId>0)
                {
                    return controller.Url.Action("Index", "AdminIndex", null, controller.Request.Url.Scheme);
                    
                }

              

            }

            return controller.Url.Action("Logon", "AdminIndex", null, controller.Request.Url.Scheme);
        }
    }
}