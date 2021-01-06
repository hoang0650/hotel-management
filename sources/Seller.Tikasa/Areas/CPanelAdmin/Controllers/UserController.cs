using MyFinance.Domain.BusinessModel;
using MyFinance.Service;
using MyFinance.Utils;
using Seller.Tikasa.Infractstructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seller.Tikasa.Areas.CPanelAdmin.Controllers
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


        public JsonResult GetUserByHotel()
        {
            var result = _Service.GetUserByHotel(WorkContext.BizKasaContext.HotelId);
            return new JsonResult() { Data = result };
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
                data.Password = Utils.CreateMD5(data.Password);
            data.HotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Service.AddUser(data);
            return new JsonResult() { Data = result };
        }
    }
}
