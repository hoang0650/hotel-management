using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Service;
using MyFinance.Utils;
using Newtonsoft.Json;
using Seller.Tikasa.Areas.CPanelAdmin.Models;
using Seller.Tikasa.Infractstructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seller.Tikasa.Areas.CPanelAdmin.Controllers
{
    
    public class InvoiceController : Controller
    {
        private readonly ITikasaService _Service;

        public InvoiceController(ITikasaService InvoiceService)
        {
            this._Service = InvoiceService;

        }  

        //
        // GET: /CPanelAdmin/Invoice/
        public ActionResult Index()
        {
            return View();
        }

      
          
        [HttpPost]
        public JsonResult GetInvoices(InvoiceFilterModel filter)
        {
            var result = _Service.GetInvoices(filter);
            return new JsonResult() { Data =result  };
        }


        [HttpPost]
        public JsonResult UpdateStatusInvocie(int invoiceId, int status)
        {
            var data = _Service.UpdateStatusInvocie(invoiceId, status);

            return new JsonResult() { Data = data };
        }

        [HttpPost,SessionFilterAction]
        public JsonResult InsertOrUpdateInvoice(InvoiceRowModel data)
        {
            var result = _Service.InsertOrUpdateInvoice(data);
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }


        public JsonResult InitCategoryInvoice()
        {
            var result = CommonUtil.ToJson(typeof(CategoryInvoice));
            return new JsonResult() { Data = result,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
        }

        public JsonResult InitFilterModel()
        {
            var model = new InitFilterModel();
            model.InvoiceStatus = CommonUtil.ToJson(typeof(OrderStatus));
            model.PaymentMethod=CommonUtil.ToJson(typeof(PaymentMethod));
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
      

        public ActionResult Payment()
        {
         
            return View();
        }

       
    }
}
