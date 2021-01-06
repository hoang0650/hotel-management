using System;
using System.Web;
using System.Web.Mvc;
using MyFinance.Bizkasa.Infractstructure;
using System.Web.Security;
using MyFinance.Utils;
using System.Net;
using Newtonsoft.Json;
using log4net;
using MyFinance.Domain.BusinessModel;
using MyFinance.ApiService;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
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
       
        public ActionResult TopNavigation()
        {
            if (MyFinance.Utils.WorkContext.BizKasaContext == null)
            {
                HttpCookie myCookie = new HttpCookie(WorkContext.CookieBizkasaKey);
                myCookie = Request.Cookies[WorkContext.CookieBizkasaKey];
                if (myCookie != null)
                {
                    string m_cookie = EncryptDecryptUtility.Decrypt(myCookie.Value, true);
                    WorkContext.BizKasaContext = JsonConvert.DeserializeObject<UserContext>(m_cookie);
                }


            }
            var model = new UserLoginViewModel()
            {
                UserName =WorkContext.BizKasaContext.UserName,
                HotelName = WorkContext.BizKasaContext.HotelName,
                OwnerHotels = WorkContext.BizKasaContext.OwnerHotels,
                Logo= WorkContext.BizKasaContext.Logo,
            };
            
            return PartialView("_TopNavigation", model);
        }

        public ActionResult ChangeViewHotel(int hotelId)
        {
         var result=   _Service.ChangeViewHotel(hotelId);
            HttpCookie myCookie = new HttpCookie(WorkContext.CookieBizkasaKey);
            myCookie = Request.Cookies[WorkContext.CookieBizkasaKey];
            if (myCookie != null)
            {
                var m_context = new UserContext() {
                    HotelId= result.Data.HotelId,
                    HotelName= result.Data.HotelName,
                    IsOwner= result.Data.IsOwner,
                    UserId= result.Data.Id,
                    ShiftId=result.Data.ShiftId,
                    Logo= result.Data.Logo,
                    OwnerHotels= result.Data.OwnerHotels,
                    UserName= result.Data.Email,
                    IsInShift=result.Data.IsInShift,
                    TokenId= result.Data.Token.AuthToken,
                    UserType= result.Data.UserType
                };
               
                WorkContext.BizKasaContext = m_context;
              var  m_cookie = JsonConvert.SerializeObject(m_context);
                // expire cookie old
                myCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(myCookie);

                // create new cookie
                m_cookie = EncryptDecryptUtility.Encrypt(m_cookie, true);
                HttpCookie newCookie = new HttpCookie(WorkContext.CookieBizkasaKey, m_cookie);
                newCookie.Expires = DateTime.Now.AddDays(30);
                newCookie.Path = "/";
                Response.Cookies.Add(newCookie);
            }

            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Index()
        {

            var principal = HttpContext.User;
            if (principal.Identity.IsAuthenticated)
            {
                if (WorkContext.BizKasaContext == null)
                {
                    HttpCookie myCookie = Request.Cookies[WorkContext.CookieBizkasaKey];
                    if (myCookie != null)
                    {
                        string m_cookie = EncryptDecryptUtility.Decrypt(myCookie.Value, true);
                        WorkContext.BizKasaContext = JsonConvert.DeserializeObject<UserContext>(m_cookie);
                        if (!WorkContext.BizKasaContext.IsInShift && WorkContext.BizKasaContext.UserType==(int)Domain.Enum.UserType.Reception)
                        {
                            return View("Logout");
                        }
                    }
                        
                    else
                    {
                        string email = principal.Identity.Name;
                        var result = MyFinance.Extention.IoC.Get<ITikasaService>().Relogin(email);
                        if (result.Data == null)
                        {
                            FormsAuthentication.SignOut();
                        }
                        else
                        {
                            UserContext ctx = new UserContext()
                            {
                                HotelId = result.Data.HotelId,
                                TokenId = result.Data.Token.AuthToken,
                                UserName = result.Data.Email,
                                Logo = result.Data.Logo,
                                UserId = result.Data.Id,
                                ShiftId = result.Data.ShiftId,
                                FullName = result.Data.FullName,
                                HotelName = result.Data.HotelName,
                                IsOwner = result.Data.IsOwner,
                                UserType = result.Data.UserType,
                                IsInShift=result.Data.IsInShift,
                                OwnerHotels = result.Data.OwnerHotels
                            };
                            WorkContext.BizKasaContext = ctx;
                            string ctxtr = JsonConvert.SerializeObject(ctx);
                            ctxtr = EncryptDecryptUtility.Encrypt(ctxtr, true);
                            HttpCookie ContextCookie = new HttpCookie(WorkContext.CookieBizkasaKey, ctxtr);
                            ContextCookie.Expires = DateTime.Now.AddDays(30);
                            ContextCookie.Path = "/";
                            Response.Cookies.Add(ContextCookie);


                        }
                    }
                  
                   
                  
                }


                var url = this.GetHomeUrl();
                if (url != Url.Action("Index", "Home", null, Request.Url.Scheme))
                {
                    return Redirect(url);
                }
            }
            return View("Logon");

        }


        [HttpPost, SessionFilterAction]
        public JsonResult Login(RequestLogin model)
        {

            if (HttpContext.Request.IsAjaxRequest() && (!User.Identity.IsAuthenticated))
            {
                Response.AddHeader("SYSTEM", "TIMEOUT");
                Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }

            model.Password= CommonUtil.CreateMD5(model.Password);


            var result = _Service.Login(model);
            if (result.HasError)
            {
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            }
            if (result.Data != null)
            {
               
                var context = new UserContext()
                {
                    HotelId = result.Data.HotelId,
                    UserId = result.Data.Id,
                    UserName = result.Data.Email,
                    FullName = result.Data.FullName,
                    UserType = result.Data.UserType,
                    HotelName = result.Data.HotelName,
                    ShiftId = result.Data.ShiftId,
                    IsOwner = result.Data.IsOwner,
                    Logo = result.Data.Logo,
                    IsInShift=result.Data.IsInShift,
                    TokenId = result.Data.Token.AuthToken,
                    OwnerHotels = result.Data.OwnerHotels
                };
                string ctxtr = JsonConvert.SerializeObject(context);
                ctxtr = EncryptDecryptUtility.Encrypt(ctxtr, true);
                HttpCookie ContextCookie = new HttpCookie(WorkContext.CookieBizkasaKey, ctxtr);
                ContextCookie.Expires = DateTime.Now.AddDays(30);
                ContextCookie.Path = "/";
                Response.Cookies.Add(ContextCookie);

                //Session["Bizkasa"] = result.Data;
               
                WorkContext.BizKasaContext = context;
            }

            return new JsonResult { Data = result };

        }


        public ActionResult Logout(bool clientLogout = false, string returnUrl = null)
        {
            // mở khóa cho các user khác
            _Service.Logout();

            HttpContext.Session.Clear();
            HttpCookie myCookie = new HttpCookie(WorkContext.CookieBizkasaKey);
            myCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(myCookie);
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
        [HttpGet]
        public ActionResult LoginAsHotel(int id)
        {
            var result = _Service.LoginAsHotel(id);
            if (result.HasError)
            {
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            }
            if (result.Data != null)
            {
                string userData = JsonConvert.SerializeObject(result.Data);
                FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(0, result.Data.Email, DateTime.Now, DateTime.Now.AddDays(1), true, userData, FormsAuthentication.FormsCookiePath);
                string encTicket = FormsAuthentication.Encrypt(authTicket);
                HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                faCookie.Expires = DateTime.Now.AddDays(30);
                faCookie.Path = "/";
                Response.Cookies.Add(faCookie);

                //Session["Bizkasa"] = result.Data;
                var context = new UserContext()
                {
                    HotelId = result.Data.HotelId,
                    UserId = result.Data.Id,
                    UserName = result.Data.Email,
                    FullName = result.Data.FullName,
                    UserType = result.Data.UserType,
                    HotelName = result.Data.HotelName,
                    IsOwner = result.Data.IsOwner,
                    IsInShift=result.Data.IsInShift,
                    Logo = result.Data.Logo,
                    TokenId = result.Data.Token.AuthToken
                };
                WorkContext.BizKasaContext = context;
            }

            return RedirectToAction("Index", "Home");

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
