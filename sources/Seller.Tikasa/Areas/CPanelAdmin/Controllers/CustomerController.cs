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
   
    public class CustomerController : Controller
    {
        //
        // GET: /CPanelAdmin/Customer/
        private readonly ITikasaService _Service;

        public CustomerController(ITikasaService InvoiceService)
        {
            this._Service = InvoiceService;

        }


        [HttpPost]
        public JsonResult GetCustomerByName(string customerName)
        {
            var result = _Service.GetCustomerByName(customerName);
            return new JsonResult() { Data = result };
        }


        [HttpPost]
        public JsonResult GetCustomerPassportId(string passportId)
        {
            var result = _Service.GetCustomerPassportId(passportId);
            return new JsonResult() { Data = result };
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckIn()
        {
            return View();
        }



        [HttpPost]
        public JsonResult GetListCustomer(CustomerSearchModel filter)
        {
            var result = _Service.GetListCustomer(filter);
            return new JsonResult() { Data = result };
        }


        [HttpPost]
        public JsonResult GetInvoicesByCustomer(List<int> OrderIds)
        {
            var result = _Service.GetInvoicesByCustomer(OrderIds);
            return new JsonResult() { Data = result };
        }

        [HttpPost]
        public JsonResult GetCustomerById(int CustomerId)
        {
            var result = _Service.GetCustomerById(CustomerId);
            if (result.HasError) return JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }

        [HttpPost]
        public JsonResult InsertOrUpdateCustomer(CustomerRowModel model)
        {
            var result = _Service.InsertOrUpdateCustomer(model);
            if (result.HasError) return JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }


        [HttpPost]
        public JsonResult GetListCustomerCheckIn(CustomerSearchModel model)
        {
            var result = _Service.GetListCustomerCheckIn(model);
            if (result.HasError) return JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }
    }
}
