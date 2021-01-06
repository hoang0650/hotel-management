using MyFinance.Utils;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Infractstructure
{
    public static class UrlHelper
    {
        public static string GetHomeUrl(this Controller controller)
        {

            var principal = controller.HttpContext.User;

            if (principal != null && principal.Identity.IsAuthenticated)
            {
                var wct = WorkContext.BizKasaContext;
                if (wct==null)
                    return controller.Url.Action("Logon", "Home", null, controller.Request.Url.Scheme);
                var url = string.Empty;
                if (wct.HotelId>0)
                {
                    switch (wct.UserType)
                    {
                        case 3:
                            url = controller.Url.Action("Index", "HouseKeeping", null, controller.Request.Url.Scheme);
                            break;
                        case 1:
                            url = controller.Url.Action("Index", "Report", null, controller.Request.Url.Scheme);
                            break;
                        case 2:
                            url = controller.Url.Action("Index", "AdminIndex", null, controller.Request.Url.Scheme);
                            break;
                        default :
                            url = controller.Url.Action("Index", "AdminIndex", null, controller.Request.Url.Scheme);
                            break;
                    }
                    return url;
                    
                }

              

            }

            return controller.Url.Action("Logon", "AdminIndex", null, controller.Request.Url.Scheme);
        }
    }
}