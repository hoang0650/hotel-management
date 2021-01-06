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
    [RoutePrefix("api/History")]
    public class HistoryController : ApiController
    {
         private readonly ITikasaService _Services;

           #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
         public HistoryController(ITikasaService tokenServices)
        {
            this._Services = tokenServices;
        }
           #endregion


         [Route("GetHistories")]
         [HttpPost]
         public IHttpActionResult IGetHistoriest(InvoiceFilterModel request)
         {
             return Ok(GetHistories(request));
         }
         public Response GetHistories(InvoiceFilterModel request)
         {
             var result = _Services.GetHistories(request);
             return result;
         }


         [Route("GetHistoriesByInside")]
         [HttpPost]
         public IHttpActionResult IGetHistoriesByInside(InvoiceFilterModel request)
         {
             return Ok(GetHistoriesByInside(request));
         }
         public Response GetHistoriesByInside(InvoiceFilterModel request)
         {
             var result = _Services.GetHistoriesByInside(request);
             return result;
         }


         [Route("InsertHistory")]
         [HttpPost]
         public IHttpActionResult IInsertHistory(HistoryModel request)
         {
             return Ok(InsertHistory(request));
         }
         public Response InsertHistory(HistoryModel request)
         {
             var result = _Services.InsertHistory(request);
             return result;
         }
    }
}
