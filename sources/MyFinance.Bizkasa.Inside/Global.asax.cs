using MyFinance.Bizkasa.Inside.App_Start;
using MyFinance.Bizkasa.Inside.Infractstructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace MyFinance.Bizkasa.Inside
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Maps.Maps.Boot();
            UnityWebActivator.Start();
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["InsideCookie"];
            if (authCookie != null)
            {

                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                UserLoginViewModel serializeModel = JsonConvert.DeserializeObject<UserLoginViewModel>(authTicket.UserData);
                KasaPrincipal newUser = new KasaPrincipal(authTicket.Name);
               
                if (serializeModel != null)
                {
                    newUser.UserId = serializeModel.Id;
                    //newUser.FirstName = serializeModel.;
                    newUser.LastName = serializeModel.Email;
                    //newUser.roles = serializeModel.;

                    HttpContext.Current.User = newUser;
                }
               
            }
           
        }
    }
}
