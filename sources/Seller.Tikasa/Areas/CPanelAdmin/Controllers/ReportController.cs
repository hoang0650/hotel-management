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
    public class ReportController : Controller
    {
         private readonly ITikasaService _Service;

         public ReportController(ITikasaService InvoiceService)
        {
            this._Service = InvoiceService;

        }  

        //
        // GET: /CPanelAdmin/Report/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReportByRoom()
        {
            return View();
        }

        public ActionResult ReportByService()
        {
            return View();
        }

        public ActionResult ReportGoodsReceipt()
        {
            return View();
        }

        public ActionResult ReportRoomHistory()
        {
            return View();
        }

        public ActionResult ReportRevenue()
        {
            return View();
        }
        public JsonResult GetListRooms()
        {
           
            var result = _Service.GetListRooms();
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result ,JsonRequestBehavior=JsonRequestBehavior.AllowGet};
        }

        [HttpPost]
        public JsonResult ReportRoomHistory(DateTime? FromDate, DateTime? ToDate, int roomId)
        {
            if (!FromDate.HasValue)
            {
                FromDate = DateTime.Now.AddDays(-30);
                ToDate = DateTime.Now;
            }
            var result = _Service.ReportRoomHistory(FromDate.Value, ToDate.Value, roomId);
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }


        [HttpPost]
        public JsonResult ReportGoodsReceipt(DateTime? FromDate, DateTime? ToDate)
        {
            if (!FromDate.HasValue)
            {
                FromDate = DateTime.Now.AddDays(-30);
                ToDate = DateTime.Now;
            }
            var result = _Service.ReportGoodsReceipt(FromDate.Value, ToDate.Value);
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }



         [HttpPost]
        public JsonResult ReportByRoom(DateTime? FromDate, DateTime? ToDate, bool ByRoomType)
        {
             if(!FromDate.HasValue)
             {
                 FromDate = DateTime.Now.AddDays(-30);
                 ToDate = DateTime.Now;
             }
             var result = _Service.ReportByRoom(FromDate.Value, ToDate.Value, ByRoomType);
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }

         [HttpPost]
         public JsonResult ReportByService(DateTime? FromDate, DateTime? ToDate)
         {
             if (!FromDate.HasValue)
             {
                 FromDate = DateTime.Now.AddDays(-30);
                 ToDate = DateTime.Now;
             }
             var result = _Service.ReportByService(FromDate.Value, ToDate.Value);
             if (result.HasError)
                 JsonCommonResult.CreateError(result.ToErrorMsg());
             return new JsonResult() { Data = result };
         }


        [HttpPost]
        public JsonResult GetStaticReport(DateTime? FromDate, DateTime? ToDate)
        {
            if (!FromDate.HasValue)
            {
                FromDate = DateTime.Now.AddDays(-30);
                ToDate = DateTime.Now;
            }
            var result = _Service.GetStaticReport(FromDate, ToDate);
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }


        public JsonResult GetRoomPopularReport()
        {
           
            var result = _Service.GetRoomPopularReport();
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult GetReceiptReport(DateTime? FromDate, DateTime? ToDate)
        {
            if (!FromDate.HasValue)
            {
                FromDate = DateTime.Now.AddDays(-30);
                ToDate = DateTime.Now;
            }
            var result = _Service.GetReceiptReport(FromDate, ToDate);
            if (result.HasError)
                JsonCommonResult.CreateError(result.ToErrorMsg());
            return new JsonResult() { Data = result };
        }


        [HttpPost]
        public JsonResult ReportRevenue(InvoiceFilterModel filter)
        {
            var result = _Service.ReportRevenue(filter);
            return new JsonResult() { Data = result };
        }
	}
}