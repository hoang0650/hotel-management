using MyFinance.Data;
using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using Newtonsoft.Json;
using Seller.Tikasa.App_Start;
using Seller.Tikasa.Infractstructure;
using Seller.Tikasa.Maps;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace Seller.Tikasa
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
            // UnityConfig.Initialise();

        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
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
