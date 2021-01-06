using MyFinance.Domain.BusinessModel;
using MyFinance.Bizkasa.Infractstructure;
using System.Collections.Generic;
using System.Web.Mvc;
using MyFinance.ApiService;
using LacViet.HPS.Common.Utilities;
using MyFinance.Utils;
using System;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
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


        [HttpGet]
        public JsonResult GetCustomerByName(CustomerSearchModel model)
        {
            var result = _Service.GetCustomerByName(model);
            return result.ToJsonResult(result.Data);
        }


        [HttpGet]
        public JsonResult GetCustomerPassportId(string passportId)
        {
            var result = _Service.GetCustomerPassportId(passportId);
            return new JsonResult() { Data = result ,JsonRequestBehavior=JsonRequestBehavior.AllowGet};
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
            Session["CustomerSearchModel"] = filter;
            var result = _Service.GetListCustomer(filter);
            return new JsonResult() { Data = result };
        }


        [HttpPost]
        public JsonResult GetInvoicesByCustomer(List<int> OrderIds)
        {
            var result = _Service.GetInvoicesByCustomer(OrderIds);
            return result.ToJsonResult(result.Data);
        }

        public FileContentResult ExportCustomer()
        {
            var model = (CustomerSearchModel)Session["CustomerSearchModel"];
            ExcelUtility m_ExcelUtility = new ExcelUtility();
            m_ExcelUtility.TemplateFileData = System.IO.File.ReadAllBytes(Server.MapPath("~/DataFiles/CustomerTemplate.xlsx"));
            model.Page.currentPage = 1;
            var result = _Service.GetListCustomer(model);
            int i = 0;
            foreach (var item in result.Data.Data)
            {
                i++;
                item.Index = i;
            }
            //m_ExcelUtility.ParameterData.Add("FromDate", model.FromDate.Value.ToStringDateVN());
            //m_ExcelUtility.ParameterData.Add("ToDate", model.ToDate.Value.ToStringDateVN());
            //m_ExcelUtility.ParameterData.Add("TotalAmount", result.Data.Summary.TotalAmount);
            string filename= "Danhsachkhachhang_"+DateTime.Now.ToString("ddmmyyhhmm")+".xlsx";
            var m_DataExported = m_ExcelUtility.Export(result.Data.Data);
            return File(m_DataExported, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);

        }

        [HttpPost]
        public JsonResult GetCustomerById(int CustomerId)
        {
            var result = _Service.GetCustomerById(CustomerId);
            return result.ToJsonResult(result.Data);
        }

        [HttpPost]
        public JsonResult InsertOrUpdateCustomer(CustomerRowModel model)
        {
            var result = _Service.InsertOrUpdateCustomer(model);            
            return result.ToJsonResult(result.Data);
        }


        [HttpPost]
        public JsonResult GetListCustomerCheckIn(CustomerSearchModel model)
        {
            var result = _Service.GetListCustomerCheckIn(model);
            if (result.HasError) return JsonCommonResult.CreateError(result.ToErrorMsg());
            return result.ToJsonResult(result.Data);
        }
    }
}
