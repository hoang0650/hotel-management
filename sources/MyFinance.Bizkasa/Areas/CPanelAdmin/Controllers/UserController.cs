using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Utils;
using MyFinance.Bizkasa.Infractstructure;
using System.Collections.Generic;
using System.Web.Mvc;
using MyFinance.ApiService;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{
    [KasaAuthorize]
    public class UserController : Controller
    {
       private readonly ITikasaService _Service;


       public UserController(ITikasaService userService)
        {
            this._Service = userService;

        }  
        //
        // GET: /CPanelAdmin/User/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Detail()
        {
            return View();
        }
        //
        // GET: /CPanelAdmin/User/Details/5

        [HttpGet]
        public JsonResult GetUserByHotel()
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Service.GetUserByHotel(hotelId);
            return result.ToJsonResult(result.Data);
        }
        [HttpGet]
        public JsonResult GetAdminByHotel()
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Service.GetAdminByHotel(hotelId);
            return result.ToJsonResult(result.Data);
        }

        public JsonResult GetUserForEdit(int userId)
        {
            var result = _Service.GetUserForEdit(userId);
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }
        public JsonResult GetUserForEditCurentAccount()
        {
            int userId = WorkContext.BizKasaContext.UserId;
            var result = _Service.GetUserForEdit(userId);
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
        }

         [HttpPost]
        public JsonResult DeleteUser(List<int> Ids)
        {
            var result = _Service.DeleteUsers(Ids);
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }


        public JsonResult AddUser(UserViewModel data)
        {
            if (!string.IsNullOrWhiteSpace(data.Password))
                data.Password = CommonUtil.CreateMD5(data.Password);
            data.HotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Service.AddUser(data);
            return result.ToJsonResult(result.Data);
        }

        public JsonResult InitPermission()
        {
            var result = CommonUtil.ToJsonInt(typeof(UserType));
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
