using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Utils;
using MyFinance.Bizkasa.Infractstructure;
using System.Web.Mvc;
using System.Web.Security;
using MyFinance.ApiService;
using LacViet.HPS.Common.Utilities;


namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Controllers
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
        public ActionResult Shift()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetInvoices(InvoiceFilterModel filter)
        {
            Session["InvoiceFilterModel"] = filter;
            var result = _Service.GetInvoices(filter);
            return result.ToJsonResult(result.Data);
        }

        [HttpPost]
        public JsonResult GetInvoiceByPayment(InvoiceFilterModel filter)
        {
            
            var result = _Service.GetInvoiceByPayment(filter);
            return result.ToJsonResult(result.Data);
        }
        public FileContentResult ExportInvoice()
        {
            var model = (InvoiceFilterModel)Session["InvoiceFilterModel"];
            ExcelUtility m_ExcelUtility = new ExcelUtility();
            m_ExcelUtility.TemplateFileData = System.IO.File.ReadAllBytes(Server.MapPath("~/DataFiles/RevenueTemplate.xlsx"));
            var result = _Service.GetInvoicesExport(model);
            m_ExcelUtility.ParameterData.Add("FromDate", model.FromDate.Value.ToStringDateVN());
            m_ExcelUtility.ParameterData.Add("ToDate", model.ToDate.Value.ToStringDateVN());
            m_ExcelUtility.ParameterData.Add("TotalAmount", result.Data.Summary.TotalAmount);

            var m_DataExported = m_ExcelUtility.Export(result.Data.Data);
            return File(m_DataExported, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Tonghopphieuthu.xlsx");

        }





        [HttpPost]
        public JsonResult DeleteInvoice(InvoiceFilterModel filter)
        {
            var result = _Service.DeleteInvoice(filter.InvoiceIds);
            return result.ToJsonResult(result.Data);
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
        public JsonResult InitCategoryInvoicePayment()
        {
            var result = CommonUtil.ToJson(typeof(CategoryInvoicePayment));
            return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult InitFilterModel()
        {
            var model = new InitFilterModel();
            model.InvoiceStatus = CommonUtil.ToJson(typeof(InvoiceStatus));
            model.PaymentMethod=CommonUtil.ToJson(typeof(PaymentMethod));
            return new JsonResult() { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
      

        public ActionResult Payment()
        {
         
            return View();
        }


        [HttpPost]
        public JsonResult AddOrUpdateShift(ShiftDTO dto)
        {
            var data = _Service.AddOrUpdateShift(dto);
            if (data.HasError)
                return JsonCommonResult.CreateError(data.ToErrorMsg());
            if (data.Data)
            {
                FormsAuthentication.SignOut();
            }
            return data.ToJsonResult(data.Data);
        }
        [HttpGet]
        public JsonResult SummaryInShift()
        {
            var result = _Service.SummaryInShift();
            if (result.HasError)
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            return result.ToJsonResult(result.Data);
           
        }
        [HttpPost]
        public JsonResult TransferToManager(ShiftTransferManagerDTO dto)
        {
            dto.ManagerPassword = CommonUtil.CreateMD5(dto.ManagerPassword);
            dto.ShiftId = WorkContext.BizKasaContext.ShiftId;
            dto.HotelId = WorkContext.BizKasaContext.HotelId;
            var data = _Service.TransferToManager(dto);
            return data.ToJsonResult(data.Data);

        }
    }
}
