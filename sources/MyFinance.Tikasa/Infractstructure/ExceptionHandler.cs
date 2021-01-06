
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Principal;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Utils;
using log4net;

namespace MyFinance.Tikasa.Infractstructure
{
   


    public class ExceptionHandler : System.Web.Mvc.IExceptionFilter
    {
        private static readonly ILog logger =
           LogManager.GetLogger(typeof(ExceptionHandler));
        public static bool IsAjaxRequest()
        {
            var request = HttpContext.Current.Request;
            return ((request["X-Requested-With"] == "XMLHttpRequest") || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest")));
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {

                logger.Error(filterContext.Exception);
                if (filterContext.HttpContext.Request.IsAuthenticated)
                {
                    if (WorkContext.BizKasaContext == null)
                    {
                       
                            HttpContext.Current.Response.Redirect("~/CPanelAdmin/Home");
                            return;
                       
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
                        return;
                    }
                   
                }

                if (IsAjaxRequest())
                {
                    HttpContext.Current.Response.Redirect("~/Home/Error?code=999");
                    return;
                }

                if (!HttpContext.Current.IsDebuggingEnabled)
                {
                    if (filterContext.Exception is System.Web.HttpRequestValidationException)
                    {
                        HttpContext.Current.Response.Redirect("~/Home/Error?code=9000");
                        return;
                    }
                }

                HttpContext.Current.Response.Redirect("~/CPanelAdmin/Home/Logon");
                
            }
        }
    }

    
}