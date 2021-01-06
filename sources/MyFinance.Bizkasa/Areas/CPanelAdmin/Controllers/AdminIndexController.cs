using System;
using System.Web;
using System.Web.Mvc;
using MyFinance.Bizkasa.Infractstructure;
using MyFinance.ApiService;
using MyFinance.Utils;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    [KasaAuthorizeAttribute] 
    public class AdminIndexController : Controller
    {

       
        private readonly ITikasaService _Service;

        public AdminIndexController(ITikasaService roomService)
        {
            this._Service = roomService;
        }  

        //public ActionResult ListAccount()
        //{
        //    return View();
        //}

        //
        // GET: /CPanelAdmin/Home/
       // [Authorize(Roles = "Admin,Nhanvien")]
        public ActionResult Index()
        {
            
            return View();
           
        }   



   
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Logon(FormCollection f)
        {
            //var uid = f["user"];
            //var pwd = f["pass"];
            //var encodePass = String.Concat(Cryptography.EncodeMD5(pwd), Cryptography.EncryptData(pwd));
            //var list = _service.GetAll().Where(n => n.UserLogon == uid && n.PassLogon == encodePass && n.IsActive == true).ToList();
            //if (list.Count > 0)
            //{

            //    var userCookie = new HttpCookie("UID");
            //    var username = _service.GetAll().SingleOrDefault(m => m.UserLogon == uid);
            //    if (username != null) userCookie.Values["ID"] = username.AdminAccountID;
            //    userCookie.Expires = DateTime.Now.AddMinutes(20);
            //    FormsAuthentication.SetAuthCookie(uid, false);
            //    Response.Cookies.Add(userCookie);
            //    return RedirectToAction("Index");
            //}
            //ViewBag.Err = "Đăng nhập không thành công!";
            return View();
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {

            //var cookie = new HttpCookie("uid");
            //string[] cookies = Request.Cookies.AllKeys;
            var cookie = new HttpCookie("UID", "") { Expires = DateTime.Now };
            cookie.Expires = DateTime.Now.AddDays(-1d);
            //Response.Cookies.Add(cookie);
            // Thêm Cookie
            Response.Cookies.Add(cookie);
            return RedirectToAction("Logon");
        }
       
       [HttpPost]
        public JsonResult GetRoomsByFloor()
        {
            var result = _Service.GetRoomsByFloor(WorkContext.BizKasaContext.HotelId);
            return result.ToJsonResult(result.Data);
        }


        public JsonResult GetConfigPriceByRoom(int roomid)
        {
            var result = _Service.GetConfigPriceByRoom(roomid);
            return new JsonResult() { Data = result };
        }


        #region Phân quyền
        public ActionResult NotAuthorities()
        {
            if (Request.Cookies["UID"] != null)
            {
                return View();
            }

            return RedirectToRoute("LogOn");
        }
        #endregion

        

    }
}
