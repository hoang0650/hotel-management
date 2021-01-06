using MyFinance.Domain.BusinessModel;
using MyFinance.Bizkasa.Infractstructure;
using System.Collections.Generic;
using System.Web.Mvc;
using MyFinance.ApiService;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
{

    public class WidgetController : Controller
    {
        private readonly ITikasaService _Service;


        public WidgetController(ITikasaService userService)
        {
            this._Service = userService;

        }  
        //
        // GET: /CPanelAdmin/Widget/
        public ActionResult Index()
        {
            return View();
        }

       

        [HttpPost]
        public JsonResult AddGroupWidget(WidgetGroupRowModel model){

            var result=_Service.AddGroupWidget(model);
            return result.ToJsonResult(result.Data);
        }


        [HttpPost,SessionFilterAction]
        public JsonResult AddWidget(WidgetRowModel model)
        {

            var result = _Service.AddWidget(model);
            return result.ToJsonResult(result.Data);
        }

        [HttpGet]
        public JsonResult GetWidget()
        {

            var result = _Service.GetWidgetBy();
            if(result.HasError)
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            return result.ToJsonResult(result.Data);
        }
          [HttpPost]
        public JsonResult GetGroupWidget(){

            var result=_Service.GetGroupWidgetBy();
            return result.ToJsonResult(result.Data);
        }

          [HttpPost]
          public JsonResult DeleteGroupWidget(int Id)
          {

              var result = _Service.DeleteGroupWidget(Id);
              return result.ToJsonResult(result.Data);
          }

          [HttpPost, SessionFilterAction]
          public JsonResult DeleteWidget(List<int> Ids)
          {
              var result = _Service.DeleteWidget(Ids);
              return result.ToJsonResult(result.Data);
          }

          [HttpPost]
          public JsonResult GetWidgetById(int Id)
          {
              var result = _Service.GetWidgetById(Id);
              if (result.HasError)
                  JsonCommonResult.CreateError(result.ToErrorMsg());
              return result.ToJsonResult(result.Data);
          }


          public JsonResult GetWidgetForRecept()
          {
              var result = _Service.GetWidgetForRecept();
              if (result.HasError)
                  JsonCommonResult.CreateError(result.ToErrorMsg());
              return result.ToJsonResult(result.Data);
          }
    }
}
