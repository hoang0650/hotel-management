using MyFinance.Bizkasa.Service;
using MyFinance.Bizkasa.Service.Inside;
using MyFinance.Domain.BusinessModel;
using System.Web.Mvc;

namespace MyFinance.Bizkasa.Inside.Controllers
{
    public class UtilityController : Controller
    {
        private readonly ITikasaService _Service;

            private readonly IInsideService _InsideService;
            public UtilityController(ITikasaService hotelService, IInsideService insideService)
            {
                this._Service = hotelService;
                _InsideService = insideService;
            }
            //
           
            public ActionResult Index()
            {

                return View();
            }

            [HttpPost]
            public JsonResult GetUtilities()
            {
                var result = _InsideService.GetUtilities();
                return new JsonResult() { Data = result };
            }

            [HttpPost]
            public JsonResult GetUtilityGroups()
            {
                var result = _InsideService.GetUtilityGroups();
                return new JsonResult() { Data = result };
            }

         [HttpPost]
            public JsonResult AddOrUpdateUtility(UtilityModel model)
            {
                var result = _InsideService.AddOrUpdateUtility(model);
                return new JsonResult() { Data = result };
            }

         [HttpPost]
         public JsonResult AddOrUpdateUtilityGroup(UtilityGroupModel model)
         {
             var result = _InsideService.AddOrUpdateUtilityGroup(model);
             return new JsonResult() { Data = result };
         }

         [HttpPost]
         public JsonResult GetUtilityForEdit(int Id)
         {
             var result = _InsideService.GetUtilityForEdit(Id);
             return new JsonResult() { Data = result,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
         }
      
	}
}