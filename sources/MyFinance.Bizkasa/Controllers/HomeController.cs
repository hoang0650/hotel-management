using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using MyFinance.Bizkasa.Infractstructure;
using System.Web.Mvc;
using MyFinance.ApiService;

namespace MyFinance.Bizkasa.Controllers
{
    public class HomeController : Controller
    {

         private readonly ITikasaService _Service;


         public HomeController(ITikasaService userService)
         {
             this._Service = userService;

         }  
        public ActionResult Index()
        {

            //if (WorkContext.BizKasaContext == null)
            //    return View();
            //else
                return RedirectToAction("Index", "CPanelAdmin/AdminIndex");

           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Login(string returnUrl = null)
        {
            if (WorkContext.BizKasaContext == null)
                return View();
            if(WorkContext.BizKasaContext.IsLogined)
            {
                if (returnUrl != null)
                    Redirect(returnUrl);
             return Redirect(this.Url.Action("Index", "Hotel"));
            }

            return View();
        }
        [HttpPost]
        public JsonResult RegisterHotel(HotelRegisterModel data)
        {
            var result = _Service.RegisterHotel(data);
            if(result.HasError)
            {
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            }
            return new JsonResult { Data = result }; 

        }

        [HttpPost]
        public JsonResult CheckUserExist(string username)
        {
            var result = _Service.CheckUserExist(username);
            if (result.HasError)
            {
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            }
            return new JsonResult { Data = result };

        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult _AccountArea()
        {
            //string userid = User.Identity.GetUserId();
            //var user = db.Users.Find(userid);
            return PartialView("_AccountArea");
        }
    }
}