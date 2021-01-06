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
            return new JsonResult(){Data=result};
        }


        [HttpPost,SessionFilterAction]
        public JsonResult AddWidget(WidgetRowModel model)
        {

            var result = _Service.AddWidget(model);
            return new JsonResult() { Data = result };
        }

        [HttpPost]
        public JsonResult GetWidget()
        {

            var result = _Service.GetWidgetBy();
            return new JsonResult() { Data = result };
        }
          [HttpPost]
        public JsonResult GetGroupWidget(){

            var result=_Service.GetGroupWidgetBy();
            return new JsonResult(){Data=result};
        }

          [HttpPost]
          public JsonResult DeleteGroupWidget(int Id)
          {

              var result = _Service.DeleteGroupWidget(Id);
              return new JsonResult() { Data = result };
          }

          [HttpPost, SessionFilterAction]
          public JsonResult DeleteWidget(List<int> Ids)
          {
              var result = _Service.DeleteWidget(Ids);
              return new JsonResult() { Data = result };
          }

          [HttpPost]
          public JsonResult GetWidgetById(int Id)
          {
              var result = _Service.GetWidgetById(Id);
              if (result.HasError)
                  JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result };
          }


          public JsonResult GetWidgetForRecept()
          {
              var result = _Service.GetWidgetForRecept();
              if (result.HasError)
                  JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result,JsonRequestBehavior=JsonRequestBehavior.AllowGet};
          }
    }
}
