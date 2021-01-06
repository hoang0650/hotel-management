using MyFinance.Bizkasa.Inside.Infractstructure;
using MyFinance.Bizkasa.Service.Inside;
using MyFinance.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyFinance.Bizkasa.Inside.Controllers
{
    public class HomeController : Controller
    {
        private readonly IInsideService _Service;

        public HomeController(IInsideService roomService)
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
        [HttpPost]
        public JsonResult Login(string email, string password)
        {

            if (HttpContext.Request.IsAjaxRequest() && (!User.Identity.IsAuthenticated))
            {
                Response.AddHeader("SYSTEM", "TIMEOUT");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

            password =CommonUtil.CreateMD5(password);
            
            var result = _Service.Login(email, password);
            if (result.HasError)
            {
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            }
            if (result.Data != null)
            {
                string userData = JsonConvert.SerializeObject(result.Data);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(0, result.Data.UserName, DateTime.Now, DateTime.Now.AddDays(1), false, userData, FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie("InsideCookie", encTicket);
                Response.Cookies.Add(faCookie);

             
                var context = new UserContext()
                {
                    HotelId = result.Data.HotelId,
                    UserId = result.Data.Id,
                    UserName = result.Data.UserName,
                    HotelName = result.Data.HotelName,
                    IsOwner = result.Data.IsOwner,
                    TokenId=result.Data.Token.AuthToken

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
        public ActionResult Logon()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

    }
}