using MyFinance.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Seller.Tikasa.Infractstructure;
using System.Web.Security;
using MyFinance.Utils;
using System.Net;
using Newtonsoft.Json;
using log4net;
using MyFinance.Service.Inside;

namespace Seller.Tikasa.Areas.CPanelAdmin.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog logger =
          LogManager.GetLogger(typeof(HomeController));
        //
        // GET: /CPanelAdmin/Home/
        private readonly ITikasaService _Service;

        public HomeController(ITikasaService roomService)
        {
            this._Service = roomService;
        }


        public ActionResult Index()
        {

            var principal = HttpContext.User;
            if (principal.Identity.IsAuthenticated)
            {
                var url = this.GetHomeUrl();
                if (url != Url.Action("Index", "Home", null, Request.Url.Scheme))
                {
                    return Redirect(url);
                }
            }
            return View("Logon");

        }


        [HttpPost,SessionFilterAction]       
        public JsonResult Login(string email, string password,bool IsRemember=false)
        {
            
            if (HttpContext.Request.IsAjaxRequest() && (!User.Identity.IsAuthenticated))
            {
                Response.AddHeader("SYSTEM", "TIMEOUT");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

            password = Utils.CreateMD5(password);
            var result = _Service.Login(email, password);
            if (result.HasError)
            {
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            }
            if (result.Data != null)
            {
                string userData = JsonConvert.SerializeObject(result.Data);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(0, result.Data.Email, DateTime.Now, DateTime.Now.AddDays(1), IsRemember, userData,FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                faCookie.Expires = DateTime.Now.AddDays(7);
                faCookie.Path = "/";
                Response.Cookies.Add(faCookie);            

              
                //Session["Bizkasa"] = result.Data;
                var context = new UserContext() { 
                    HotelId = result.Data.HotelId,
                    UserId = result.Data.Id,
                    UserName = result.Data.Email,
                    HotelName=result.Data.HotelName,
                    IsOwner=result.Data.IsOwner,
                    Logo = result.Data.Logo

                };
                WorkContext.BizKasaContext = context;
            }






            return new JsonResult { Data = result };

        }


        public ActionResult Logout(bool clientLogout = false, string returnUrl = null)
        {
            HttpContext.Session.Clear();
            //SendoSession.Current.Dispose();
            FormsAuthentication.SignOut();
            WorkContext.BizKasaContext = null;
            string absUrl = returnUrl;

            if (absUrl == null)
                absUrl = Url.Action("Index", "Home", null, Request.Url.Scheme);
            return Redirect(absUrl);

        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Logon()
        {
            //if (Request.Cookies["UID"] != null)
            //{
            //    var userCookie = new HttpCookie("UID", "") { Expires = DateTime.Now };
            //    Response.Cookies.Add(userCookie);
            //}
            return View();
        }

        [AllowAnonymous]
        public ActionResult Error()
        {
            //if (Request.Cookies["UID"] != null)
            //{
            //    var userCookie = new HttpCookie("UID", "") { Expires = DateTime.Now };
            //    Response.Cookies.Add(userCookie);
            //}
            return View();
        }
    }
    
}
