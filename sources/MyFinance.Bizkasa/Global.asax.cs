using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using Newtonsoft.Json;
using MyFinance.Bizkasa.App_Start;
using MyFinance.Bizkasa.Infractstructure;
using MyFinance.Bizkasa.Maps;
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

namespace MyFinance.Bizkasa
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
            //UnityConfig.Initialise();

        }
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            try
            {
                HttpCookie authCookie = Request.Cookies[WorkContext.CookieBizkasaKey];
                if (authCookie != null)
                {
                    string authTicket = EncryptDecryptUtility.Decrypt(authCookie.Value, true);
                    //FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                    UserLoginViewModel serializeModel = JsonConvert.DeserializeObject<UserLoginViewModel>(authTicket);
                    KasaPrincipal newUser = new KasaPrincipal(serializeModel.UserName);
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
            catch (Exception)
            {

              
            }
            
          
        }
    }
}
