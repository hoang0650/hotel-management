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
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
         private readonly ITikasaService _tokenServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
         public CustomerController(ITikasaService tokenServices)
        {
            this._tokenServices = tokenServices;
        }

        #endregion


         [Route("GetListCustomer")]
         [HttpPost]
         public IHttpActionResult IGetListCustomer(CustomerSearchModel filter)
         {
             return Ok(GetListCustomer(filter));
         }
         public Response GetListCustomer(CustomerSearchModel filter)
         {
             var result = _tokenServices.GetListCustomer(filter);
             return result;
         }


         [Route("GetListCustomerCheckIn")]
         [HttpPost]
         public IHttpActionResult IGetListCustomerCheckIn(CustomerSearchModel filter)
         {
             return Ok(GetListCustomerCheckIn(filter));
         }
         public Response GetListCustomerCheckIn(CustomerSearchModel filter)
         {
             var result = _tokenServices.GetListCustomerCheckIn(filter);
             return result;
         }


         [Route("GetCustomerPassportId")]
         [HttpPost]
         public IHttpActionResult IGetCustomerPassportId(CustomerSearchModel filter)
         {
             return Ok(GetCustomerPassportId(filter));
         }
         public Response GetCustomerPassportId(CustomerSearchModel filter)
         {
             var result = _tokenServices.GetCustomerPassportId(filter.PassportId);
             return result;
         }


       




        [Route("GetInvoicesByCustomer")]
         [HttpPost]
         public IHttpActionResult IGetInvoicesByCustomer(CustomerSearchModel filter)
         {
             return Ok(GetInvoicesByCustomer(filter));
         }
         public Response GetInvoicesByCustomer(CustomerSearchModel filter)
         {
             var result = _tokenServices.GetInvoicesByCustomer(filter.OrderIds);
             return result;
         }



         [Route("GetCustomerByName")]
         [HttpPost]
         public IHttpActionResult IGetCustomerByName(CustomerSearchModel filter)
         {
             return Ok(GetCustomerByName(filter));
         }
         public Response GetCustomerByName(CustomerSearchModel filter)
         {
             var result = _tokenServices.GetCustomerByName(filter);
             return result;
         }


         [Route("GetCustomerById")]
         [HttpPost]
         public IHttpActionResult IGetCustomerById(CustomerSearchModel filter)
         {
             return Ok(GetCustomerById(filter));
         }
         public Response GetCustomerById(CustomerSearchModel filter)
         {
             var result = _tokenServices.GetCustomerById(filter.Id);
             return result;
         }



         [Route("InsertOrUpdateCustomer")]
         [HttpPost]
         public IHttpActionResult IInsertOrUpdateCustomer(CustomerRowModel request)
         {
             return Ok(InsertOrUpdateCustomer(request));
         }
         public Response InsertOrUpdateCustomer(CustomerRowModel request)
         {
             var result = _tokenServices.InsertOrUpdateCustomer(request);
             return result;
         }
       
    }
}
