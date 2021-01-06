using MyFinance.ApiService;

using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MyFinance.Extention;
using System.Security.Principal;
using System.Web.Security;
using System.Web.Http.Filters;

using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;

namespace Bizkasa.Api.Infractstructure
{
   


    public class ExceptionHandler : System.Web.Mvc.IExceptionFilter
    {
        //private static readonly ILog logger =
        //   LogManager.GetLogger(typeof(ExceptionHandler));
        public static bool IsAjaxRequest()
        {
            var request = HttpContext.Current.Request;
            return ((request["X-Requested-With"] == "XMLHttpRequest") || ((request.Headers != null) && (request.Headers["X-Requested-With"] == "XMLHttpRequest")));
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception != null)
            {

                //logger.Error(filterContext.Exception);
                //if (filterContext.HttpContext.Request.IsAuthenticated)
                //{
                //    //Log out this session
                //    string email = filterContext.HttpContext.User.Identity.Name;
                //    var result=MyFinance.Extention.IoC.Get<ITikasaService>().Relogin(email);
                //    if (!result.Data)
                //    {
                //        FormsAuthentication.SignOut();
                //    }
                   
                //    //if (!IsAjaxRequest())
                //    //{
                //    //    HttpContext.Current.Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri);
                //    //    return;
                //    //}
                //}

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

                //HttpContext.Current.Response.Redirect("~/CPanelAdmin/Home/Logon");
                //HttpContext.Current.Response.Redirect("~/CPanelAdmin/Home/Error?code=" + HttpContext.Current.Response.StatusCode.ToString());
            }
        }
    }


   
}