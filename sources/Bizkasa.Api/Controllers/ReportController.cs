using Bizkasa.Api.Infractstructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.ApiService;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Bizkasa.Api.Controllers
{
    [AuthorizationRequired]
    [RoutePrefix("api/Report")]
    public class ReportController : ApiController
    {
          private readonly ITikasaService _Services;

           #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
          public ReportController(ITikasaService tokenServices)
        {
            this._Services = tokenServices;
        }
           #endregion



          [Route("GetStaticReport")]
          [HttpPost]
          public IHttpActionResult IGetStaticReport(ReportRequestModel request)
          {
              return Ok(GetStaticReport(request));
          }
          public Response GetStaticReport(ReportRequestModel request)
          {
              var result = _Services.GetStaticReport(request.FromDate,request.ToDate);
              return result;
          }


        [Route("Revenue")]
        [HttpPost]
        public IHttpActionResult IRevenue(InvoiceFilterModel request)
        {
            return Ok(Revenue(request));
        }
        public Response Revenue(InvoiceFilterModel request)
        {
            var result = _Services.Revenue(request);
            return result;
        }





        [Route("GetRoomPopularReport")]
          [HttpGet]
          public IHttpActionResult IGetRoomPopularReport()
          {
              return Ok(GetRoomPopularReport());
          }
          public Response GetRoomPopularReport()
          {
              var result = _Services.GetRoomPopularReport();
              return result;
          }



          [Route("GetReceiptReport")]
          [HttpPost]
          public IHttpActionResult IGetReceiptReport(ReportRequestModel request)
          {
              return Ok(GetReceiptReport(request));
          }
          public Response GetReceiptReport(ReportRequestModel request)
          {
              var result = _Services.GetReceiptReport(request.FromDate, request.ToDate);
              return result;
          }



          [Route("ReportRevenue")]
          [HttpPost]
          public IHttpActionResult IReportRevenue(InvoiceFilterModel request)
          {
              return Ok(ReportRevenue(request));
          }
          public Response ReportRevenue(InvoiceFilterModel request)
          {
              var result = _Services.ReportRevenue(request);
              return result;
          }




          [Route("ReportByRoom")]
          [HttpPost]
          public IHttpActionResult IReportByRoom(ReportRequestModel request)
          {
              return Ok(ReportByRoom(request));
          }
          public Response ReportByRoom(ReportRequestModel request)
          {
              var result = _Services.ReportByRoom(request.FromDate.Value, request.ToDate.Value, request.ByRoomType);
              return result;
          }





          [Route("ReportGoodsReceipt")]
          [HttpPost]
          public IHttpActionResult IReportGoodsReceipt(ReportRequestModel request)
          {
              return Ok(ReportGoodsReceipt(request));
          }
          public Response ReportGoodsReceipt(ReportRequestModel request)
          {
              var result = _Services.ReportGoodsReceipt(request.FromDate.Value, request.ToDate.Value);
              return result;
          }





          [Route("ReportByService")]
          [HttpPost]
          public IHttpActionResult IReportByService(ReportRequestModel request)
          {
              return Ok(ReportByService(request));
          }
          public Response ReportByService(ReportRequestModel request)
          {
              var result = _Services.ReportByService(request.FromDate.Value, request.ToDate.Value);
              return result;
          }






          [Route("ReportRoomHistory")]
          [HttpPost]
          public IHttpActionResult IReportRoomHistory(ReportRequestModel request)
          {
              return Ok(ReportRoomHistory(request));
          }
          public Response ReportRoomHistory(ReportRequestModel request)
          {
              var result = _Services.ReportRoomHistory(request.FromDate.Value, request.ToDate.Value, request.RoomId);
              return result;
          }

        [Route("ShiftHistory")]
        [HttpPost]
        public IHttpActionResult IShiftHistory(InvoiceFilterModel request)
        {
            return Ok(ShiftHistory(request));
        }
        public Response ShiftHistory(InvoiceFilterModel request)
        {
            var result = _Services.ShiftHistory(request);
            return result;
        }


    }
}
