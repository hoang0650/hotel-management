using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Utils;
using MyFinance.Bizkasa.Infractstructure;
using System.Web.Mvc;
using MyFinance.ApiService;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    public class SystemConfigController : Controller
    {
        private readonly ITikasaService _Service;
        public SystemConfigController(ITikasaService userService)
        {
            this._Service = userService;
        }  
        //
        // GET: /CPanelAdmin/SystemConfig/
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetRoomStatusEnum()
        {
            var result= CommonUtil.ToJson(typeof(RoomStatus));
            return new JsonResult() { Data = result,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult GetConfig()
        {
            
            var result = _Service.GetConfig();

            return new JsonResult() { Data = result };
        }

        [HttpPost, SessionFilterAction]
        public JsonResult AddConfig(SystemConfigModel data)
        {
           
            var result = _Service.AddOrUpdateConfig(data);

            return new JsonResult() { Data = result };
        }
	}
}