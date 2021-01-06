using Bizkasa.Api.Infractstructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.ApiService;
using MyFinance.ApiService.Inside;
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
    [RoutePrefix("api/Hotel")]
    public class HotelController : ApiController
    {
         private readonly ITikasaService _Services;
         private readonly IInsideService _InsideServices;

           #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
         public HotelController(ITikasaService tokenServices, IInsideService InsideServices)
        {
           this._Services = tokenServices;
           _InsideServices = InsideServices;
        }

        
        #endregion
         #region Bizkasa
         
       

         [Route("GetHotelInfo")]
       
        [HttpGet]
         public IHttpActionResult IGetHotelInfo()
         {
             return Ok(GetHotelInfo());
         }

         public Response GetHotelInfo()
         {
             var hotelId = WorkContext.BizKasaContext.HotelId;
             var result = _Services.GetHotelById(hotelId);
             return result;
         }


         [Route("GetHotelUtilityBy")]
        
        [HttpGet]
         public IHttpActionResult IGetHotelUtilityBy()
         {
             return Ok(GetHotelUtilityBy());
         }

         public Response GetHotelUtilityBy()
         {
             var result = _Services.GetHotelUtilityBy();
             return result;
         }


         [Route("AddHotel")]
         [HttpPost]
         public IHttpActionResult IAddHotel(HotelModel request)
         {
             return Ok(AddHotel(request));
         }

         public Response AddHotel(HotelModel request)
         {
             var result = _Services.AddHotel(request);
             return result;
         }




        [Route("ChangeViewHotel")]
        [HttpPost]
        public IHttpActionResult IChangeViewHotel(HotelModel request)
        {
            return Ok(ChangeViewHotel(request));
        }

        public Response ChangeViewHotel(HotelModel request)
        {
            var result = _Services.ChangeViewHotel(request.Id);
            return result;
        }



        [Route("GetConfig")]
       
        [HttpGet]
         public IHttpActionResult IGetConfig()
         {
             return Ok(GetConfig());
         }

         public Response GetConfig()
         {
             var result = _Services.GetConfig();
             return result;
         }

         [Route("AddOrUpdateConfig")]
         [HttpPost]
         public IHttpActionResult IAddOrUpdateConfig(SystemConfigModel request)
         {
             return Ok(AddOrUpdateConfig(request));
         }

         public Response AddOrUpdateConfig(SystemConfigModel request)
         {
             var result = _Services.AddOrUpdateConfig(request);
             return result;
         }


       


        

         #endregion

        #region Inside




         [Route("Inside/DisableHotel")]
         [HttpPost]
         public IHttpActionResult IDisableHotel(HotelRequestModel request)
         {
             return Ok(DisableHotel(request));
         }

         public Response DisableHotel(HotelRequestModel request)
         {
             var result = _InsideServices.DisableHotel(request.HotelId);
             return result;
         }


         [Route("GetHotels")]
         [HttpPost]
         public IHttpActionResult IGetHotels(InvoiceFilterModel request)
         {
             return Ok(GetHotels(request));
         }

         public Response GetHotels(InvoiceFilterModel request)
         {
             var result = _Services.GetHotels(request);
             return result;
         }
        #endregion

         #region Ticket


         [Route("AddTicket")]
         [HttpPost]
         public IHttpActionResult IAddTicket(TicketModel request)
         {
             return Ok(AddTicket(request));
         }

         public Response AddTicket(TicketModel request)
         {
             var result = _Services.AddTicket(request);
             return result;
         }



         [Route("ListTicket")]
         [HttpGet]
         public IHttpActionResult IListTicket()
         {
             return Ok(ListTicket());
         }

         public Response ListTicket()
         {
             var result = _Services.ListTicket();
             return result;
         }
         #endregion
        

    }
}
