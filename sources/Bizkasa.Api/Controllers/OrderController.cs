using Bizkasa.Api.Infractstructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.RequestModel;

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
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    { 
        private readonly ITikasaService _Services;

           #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public OrderController(ITikasaService tokenServices)
        {
            this._Services = tokenServices;
        }
           #endregion

        [Route("GetOrderForCheckOut")]
        [HttpPost]
        public IHttpActionResult IGetOrderForCheckOut(OrderRequestModel request)
        {

            return Ok(GetOrderForCheckOut(request));
        }
        public Response GetOrderForCheckOut(OrderRequestModel request)
        {
            RequestCheckOutModel model = new RequestCheckOutModel()
            {
                hotelId = WorkContext.BizKasaContext.HotelId,
                isByNight=false,
                mode=request.Mode,
                orderId=request.OrderId
            };
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Services.GetOrderForCheckOut(model);
            return result;
        }



        [Route("GetCountries")]
        [HttpGet]
        public IHttpActionResult IGetCountries()
        {

            return Ok(GetCountries());
        }
        public Response GetCountries()
        {
            var result = _Services.GetCountries();
            return result;
        }


        [Route("UpdateOrder")]
        [HttpPost]
        public IHttpActionResult IUpdateOrder(OrderRowModel request)
        {
            return Ok(UpdateOrder(request));
        }
        public Response UpdateOrder(OrderRowModel request)
        {
            var result = _Services.UpdateOrder(request);
            return result;
        }


        [Route("AddOrder")]
        [HttpPost]
        public IHttpActionResult IAddOrder(OrderRowModel request)
        {
            return Ok(AddOrder(request));
        }
        public Response AddOrder(OrderRowModel request)
        {
            request.HotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Services.AddOrder(request);
            return result;
        }


        [Route("GetOrderForEdit")]
        [HttpPost]
        public IHttpActionResult IGetOrderForEdit(OrderRequestModel request)
        {
            return Ok(GetOrderForEdit(request));
        }
        public Response GetOrderForEdit(OrderRequestModel request)
        {
            var result = _Services.GetOrderForEdit(request.OrderId);
            return result;
        }



        [Route("GetBookingOrders")]
        [HttpPost]
        public IHttpActionResult IGetBookingOrders(InvoiceFilterModel request)
        {
            return Ok(GetBookingOrders(request));
        }
        public Response GetBookingOrders(InvoiceFilterModel request)
        {
            var result = _Services.GetBookingOrders(request);
            return result;
        }


        [Route("GetOrderBookingByCompany")]
        [HttpPost]
        public IHttpActionResult IGetOrderBookingByCompany(OrderFilterModel request)
        {
            return Ok(GetOrderBookingByCompany(request));
        }
        public Response GetOrderBookingByCompany(OrderFilterModel request)
        {
            var result = _Services.GetOrderBookingByCompany(request);
            return result;
        }


        [Route("GetOrderByCompany")]
        [HttpGet]
        public IHttpActionResult IGetOrderByCompany()
        {
            return Ok(GetOrderByCompany());
        }
        public Response GetOrderByCompany()
        {
            var result = _Services.GetOrderByCompany(WorkContext.BizKasaContext.HotelId);
            return result;
        }


        [Route("GetFolioCustomer")]
        [HttpGet]
        public IHttpActionResult IGetFolioCustomer(OrderRequestModel request)
        {
            return Ok(GetFolioCustomer(request));
        }
        public Response GetFolioCustomer(OrderRequestModel request)
        {
            var result = _Services.GetFolioCustomer(request.CompanyId,request.TypeCustomer);
            return result;
        }



        [Route("AddOrderMutil")]
        [HttpPost]
        public IHttpActionResult IAddOrderMutil(OrderRowModel request)
        {
            return Ok(AddOrderMutil(request));
        }
        public Response AddOrderMutil(OrderRowModel request)
        {
            request.HotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Services.AddOrderMutil(request);
            return result;
        }


        [Route("CompanyDoCheckOut")]
        [HttpPost]
        public IHttpActionResult ICompanyDoCheckOut(OrderRequestModel request)
        {
            return Ok(CompanyDoCheckOut(request));
        }
        public Response CompanyDoCheckOut(OrderRequestModel request)
        {
            var result = _Services.CompanyCheckOut(request.OrderIds, request.Mode);
            return result;
        }


        [Route("TranferBookingToCheckIn")]
        [HttpPost]
        public IHttpActionResult ITranferBookingToCheckIn(OrderRequestModel request)
        {
            return Ok(TranferBookingToCheckIn(request));
        }
        public Response TranferBookingToCheckIn(OrderRequestModel request)
        {
            var result = _Services.TranferBookingToCheckIn(request.OrderId);
            return result;
        }



        [Route("ChangeRoomInOrder")]
        [HttpPost]
        public IHttpActionResult IChangeRoomInOrder(OrderRequestModel request)
        {
            return Ok(ChangeRoomInOrder(request));
        }
        public Response ChangeRoomInOrder(OrderRequestModel request)
        {
            var result = _Services.ChangeRoomInOrder(request.OrderId,request.RoomId,request.ConfigPriceId);
            return result;
        }
    }
}
