using MyFinance.Domain.BusinessModel;
using MyFinance.Bizkasa.Infractstructure;
using System.Collections.Generic;
using System.Web.Mvc;
using MyFinance.ApiService;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
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
             return result.ToJsonResult(result.Data);
         }


         [HttpPost]
         public JsonResult GetFloorBy(int floorId)
         {
             var result = _Service.GetFloorBy(floorId);
             if (result.HasError)
                 return JsonCommonResult.CreateError(result.ToErrorMsg());
             return new JsonResult() { Data = result };
         }


         [HttpPost]
         public JsonResult DeleteFloor(List<int> Ids)
         {
             var result = _Service.DeleteFloor(Ids);
             if (result.HasError)
                 return JsonCommonResult.CreateError(result.ToErrorMsg());
             return result.ToJsonResult(result.Data);
         }
        public ActionResult Index()
        {
            return View();
        }
	}
}