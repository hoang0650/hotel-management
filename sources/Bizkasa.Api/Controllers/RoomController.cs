using Bizkasa.Api.Infractstructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
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
    [RoutePrefix("api/Room")]
    public class RoomController : ApiController
    {
         private readonly ITikasaService _tokenServices;

           #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
         public RoomController(ITikasaService tokenServices)
        {
           this._tokenServices = tokenServices;
        }
           #endregion

         [Route("GetStaticRoom")]
         [HttpGet]
         public IHttpActionResult IGetStaticRoom()
         {
             return Ok(GetStaticRoom());
         }
         public Response GetStaticRoom()
         {
             var result = _tokenServices.GetStaticRoom();
             return result;
         }
         [Route("GetRoomsByFloor")]
         [HttpGet]
         public IHttpActionResult IGetRoomsByFloor()
         {
             return Ok(GetRoomsByFloor());
         }
         public Response GetRoomsByFloor()
         {
             var result = _tokenServices.GetRoomsByFloor(WorkContext.BizKasaContext.HotelId);
             return result;
         }

         [Route("GetRoomsByClass")]
         [HttpGet]
         public IHttpActionResult IGetRoomsByClass()
         {
             return Ok(GetRoomsByClass());
         }
         public Response GetRoomsByClass()
         {
             var result = _tokenServices.GetRoomsByClass();
             return result;
         }



         [Route("GetRoomsByStatus")]
         [HttpPost]
         public IHttpActionResult IGetRoomsByStatus(RoomRequestModel status)
         {

             return Ok(GetRoomsByStatus(status));
         }
         public Response GetRoomsByStatus(RoomRequestModel status)
         {
             var stt = status;
             var result = _tokenServices.GetRoomsByStatus(status.status);
             return result;
         }

         [Route("GetConfigPriceByRoom")]
         [HttpPost]
         public IHttpActionResult IGetConfigPriceByRoom(RoomRequestModel request)
         {

             return Ok(GetConfigPriceByRoom(request));
         }
         public Response GetConfigPriceByRoom(RoomRequestModel request)
         {
             var result = _tokenServices.GetConfigPriceByRoom(request.RoomId);
             return result;
         }

         [Route("GetRoomClassForCheckinMutil")]
         [HttpGet]
         public IHttpActionResult IGetRoomClassForCheckinMutil()
         {

             return Ok(GetRoomClassForCheckinMutil());
         }
         public Response GetRoomClassForCheckinMutil()
         {
             var result = _tokenServices.GetRoomForCheckinMutil();
             return result;
         }



         [Route("GetRoomForEdit")]
         [HttpPost]
         public IHttpActionResult IGetRoomForEdit(RoomRequestModel request)
         {
             return Ok(GetRoomForEdit(request));
         }
         public Response GetRoomForEdit(RoomRequestModel request)
         {
             var result = _tokenServices.GetRoomForEdit(request.RoomId);
             return result;
         }




         [Route("GetListRoomClass")]
         [HttpGet]
         public IHttpActionResult IGetListRoomClass()
         {
             return Ok(GetListRoomClass());
         }
         public Response GetListRoomClass()
         {
             var result = _tokenServices.GetListRoomClass();
             return result;
         }



         [Route("EditRoom")]
         [HttpPost]
         public IHttpActionResult IEditRoom(RoomForEditModel request)
         {
             return Ok(EditRoom(request));
         }
         public Response EditRoom(RoomForEditModel request)
         {
             var result = _tokenServices.EditRoom(request);
             return result;
         }


         [Route("ChangeStatusRoom")]
         [HttpPost]
         public IHttpActionResult IChangeStatusRoom(RoomRequestModel request)
         {
             return Ok(ChangeStatusRoom(request));
         }
         public Response ChangeStatusRoom(RoomRequestModel request)
         {
             var result = _tokenServices.ChangeStatusRoom(request.RoomId,request.status);
             return result;
         }


         [Route("DeleteRoom")]
         [HttpPost]
         public IHttpActionResult IDeleteRoom(RoomRequestModel request)
         {
             return Ok(DeleteRoom(request));
         }
         public Response DeleteRoom(RoomRequestModel request)
         {
             var result = _tokenServices.DeleteRoom(request.RoomId);
             return result;
         }

         [Route("DeleteRoomClass")]
         [HttpPost]
         public IHttpActionResult IDeleteRoomClass(FloorRequestModel request)
         {
             return Ok(DeleteRoomClass(request));
         }
         public Response DeleteRoomClass(FloorRequestModel request)
         {
             var result = _tokenServices.DeleteRoomClass(request.Ids);
             return result;
         }


         [Route("DeleteConfigPrice")]
         [HttpPost]
         public IHttpActionResult IDeleteConfigPrice(FloorRequestModel request)
         {
             return Ok(DeleteConfigPrice(request));
         }
         public Response DeleteConfigPrice(FloorRequestModel request)
         {
             var result = _tokenServices.DeleteConfigPrice(request.Ids);
             return result;
         }

         [Route("GetRoomAvailable")]
         [HttpPost]
         public IHttpActionResult IGetRoomAvailable(RoomRequestModel request)
         {
             return Ok(GetRoomAvailable(request));
         }
         public Response GetRoomAvailable(RoomRequestModel request)
         {
             var result = _tokenServices.GetRoomAvailable(request.FromDate,request.ToDate);
             return result;
         }



         [Route("GetRoomsStaticByTime")]
         [HttpPost]
         public IHttpActionResult IGetRoomsStaticByTime(RoomSearchByTimeModel request)
         {
             return Ok(GetRoomsStaticByTime(request));
         }
         public Response GetRoomsStaticByTime(RoomSearchByTimeModel request)
         {
             var result = _tokenServices.GetRoomsStaticByTime(WorkContext.BizKasaContext.HotelId, request);
             return result;
         }



         [Route("GetRoomClass")]
         [HttpGet]
         public IHttpActionResult IGetRoomClass()
         {
             return Ok(GetRoomClass());
         }
         public Response GetRoomClass()
         {
             var result = _tokenServices.GetRoomClassBy(WorkContext.BizKasaContext.HotelId);
             return result;
         }



         [Route("AddRoomClass")]
         [HttpPost]
         public IHttpActionResult IAddRoomClass(RoomClassModel request)
         {
             return Ok(AddRoomClass(request));
         }
         public Response AddRoomClass(RoomClassModel request)
         {
             var result = _tokenServices.AddRoomClass( request);
             return result;
         }


         [Route("GetRoomClassById")]
         [HttpPost]
         public IHttpActionResult IGetRoomClassById(RoomRequestModel request)
         {
             return Ok(GetRoomClassById(request));
         }
         public Response GetRoomClassById(RoomRequestModel request)
         {
             var result = _tokenServices.GetRoomClassById(request.RoomClassId);
             return result;
         }



         [Route("GetRoomUtilityBy")]
         [HttpPost]
         public IHttpActionResult IGetRoomUtilityBy(RoomRequestModel request)
         {
             return Ok(GetRoomUtilityBy(request));
         }
         public Response GetRoomUtilityBy(RoomRequestModel request)
         {
             var result = _tokenServices.GetRoomUtilityBy(request.RoomId);
             return result;
         }

         [Route("InsertRoom")]
         [HttpPost]
         public IHttpActionResult IInsertRoom(List<RoomForEditModel> request)
         {
             return Ok(InsertRoom(request));
         }
         public Response InsertRoom(List<RoomForEditModel> request)
         {
             var result = _tokenServices.AddRoom(request);
             return result;
         }


         [Route("GetListRooms")]
         [HttpGet]
         public IHttpActionResult IGetListRooms()
         {
             return Ok(GetListRooms());
         }
         public Response GetListRooms()
         {
             var result = _tokenServices.GetListRooms();
             return result;
         }



         [Route("GetConfigPriceBy")]
         [HttpPost]
         public IHttpActionResult IGetConfigPriceBy(RoomRequestModel request)
         {
             return Ok(GetConfigPriceBy(request));
         }
         public Response GetConfigPriceBy(RoomRequestModel request)
         {
             var result = _tokenServices.GetConfigPriceBy(request.RoomId);
             return result;
         }

         [Route("RefreshRoom")]
         [HttpPost]
         public IHttpActionResult IRefreshRoom(RoomRequestModel request)
         {
             return Ok(RefreshRoom(request));
         }
         public Response RefreshRoom(RoomRequestModel request)
         {
             var result = _tokenServices.RefreshRoom(request.RoomId);
             return result;
         }

         [Route("RequestAddOrUpdateConfigPriceForOne")]
         [HttpPost]
         public IHttpActionResult IRequestAddOrUpdateConfigPriceForOne(RoomRequestModel request)
         {
             return Ok(RequestAddOrUpdateConfigPriceForOne(request));
         }
         public Response RequestAddOrUpdateConfigPriceForOne(RoomRequestModel request)
         {
             var result = _tokenServices.RequestAddOrUpdateConfigPriceForOne(request.ConfigPrice,request.RoomClassId);
             return result;
         }

        

    }
}
