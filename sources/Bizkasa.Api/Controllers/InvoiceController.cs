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
    [RoutePrefix("api/Invoice")]
    public class InvoiceController : ApiController
    {
        private readonly ITikasaService _tokenServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public InvoiceController(ITikasaService tokenServices)
        {
            this._tokenServices = tokenServices;
        }

        #endregion

        [Route("GetInvoices")]
        [HttpPost]
        public IHttpActionResult IGetInvoices(InvoiceFilterModel filter)
        {
            return Ok(GetInvoices(filter));
        }
        public Response GetInvoices(InvoiceFilterModel filter)
        {
            var result = _tokenServices.GetInvoices(filter);
            return result;
        }


        [Route("InsertOrUpdateInvoice")]
        [HttpPost]
        public IHttpActionResult IInsertOrUpdateInvoice(InvoiceRowModel filter)
        {
            return Ok(InsertOrUpdateInvoice(filter));
        }
        public Response InsertOrUpdateInvoice(InvoiceRowModel filter)
        {
            var result = _tokenServices.InsertOrUpdateInvoice(filter);
            return result;
        }

        [Route("UpdateStatusInvocie")]
        [HttpPost]
        public IHttpActionResult IUpdateStatusInvocie(InvoiceRowModel request)
        {
            return Ok(UpdateStatusInvocie(request));
        }
        public Response UpdateStatusInvocie(InvoiceRowModel request)
        {
            var result = _tokenServices.UpdateStatusInvocie(request.Id,request.InvoiceStatus);
            return result;
        }

        [Route("DeleteInvoice")]
        [HttpPost]
        public IHttpActionResult IDeleteInvoice(InvoiceFilterModel request)
        {
            return Ok(DeleteInvoice(request));
        }
        public Response DeleteInvoice(InvoiceFilterModel request)
        {
            var result = _tokenServices.DeleteInvoice(request.InvoiceIds);
            return result;
        }

        [Route("SummaryInShift")]
        [HttpGet]
        public IHttpActionResult ISummaryInShift()
        {
            return Ok(SummaryInShift());
        }
        public Response SummaryInShift()
        {
            var result = _tokenServices.SummaryInShift();
            return result;
        }

        [Route("AddOrUpdateShift")]
        [HttpPost]
        public IHttpActionResult IAddOrUpdateShift(ShiftDTO request)
        {
            return Ok(AddOrUpdateShift(request));
        }
        public Response AddOrUpdateShift(ShiftDTO request)
        {
            var result = _tokenServices.AddOrUpdateShift(request);
            return result;
        }

    }
      
}
