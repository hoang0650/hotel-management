using MyFinance.Domain.BusinessModel;
using MyFinance.Service;
using Seller.Tikasa.Infractstructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seller.Tikasa.Areas.CPanelAdmin.Controllers
{
    public class FloorController : Controller
    {
         private readonly ITikasaService _Service;


         public FloorController(ITikasaService userService)
        {
            this._Service = userService;

        }  
        //
        // GET: /CPanelAdmin/Floor/


         [HttpPost]
         public JsonResult InsertOrUpdateFloor(FloorModel model)
         {
             var result = _Service.InsertOrUpdateFloor(model);
             if (result.HasError)
                 return JsonCommonResult.CreateError(result.ToErrorMsg());
             return new JsonResult() { Data = result };
         }


         [HttpPost]
         public JsonResult InsertRoom(List<RoomForEditModel> model)
         {
             var result = _Service.AddRoom(model);
             if (result.HasError)
                 return JsonCommonResult.CreateError(result.ToErrorMsg());
             return new JsonResult() { Data = result };
         }


         [HttpPost]
         public JsonResult GetFloorBy(int floorId)
         {
             var result = _Service.GetFloorBy(floorId);
             if (result.HasError)
                 return JsonCommonResult.CreateError(result.ToErrorMsg());
             return new JsonResult() { Data = result };
         }
        public ActionResult Index()
        {
            return View();
        }
	}
}