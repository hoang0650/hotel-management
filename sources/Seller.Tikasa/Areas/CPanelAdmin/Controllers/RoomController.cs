using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Service;
using MyFinance.Utils;
using Seller.Tikasa.Areas.CPanelAdmin.Models.Room;
using Seller.Tikasa.Infractstructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Seller.Tikasa.Areas.CPanelAdmin.Controllers
{
    [KasaAuthorizeAttribute] 
    public class RoomController : Controller
    {
        private readonly ITikasaService _Service;


        public RoomController(ITikasaService userService)
        {
            this._Service = userService;

        }  
        //
        // GET: /CPanelAdmin/Room/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /CPanelAdmin/Room/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /CPanelAdmin/Room/Create
        public ActionResult Create()
        {
            return View();
        }
        [SessionFilterAction]
        public JsonResult AddRoomClass(RoomClassModel data)
        {
          var result = _Service.AddRoomClass(data);
           return new JsonResult() {Data=result };
        }

        [SessionFilterAction]
        public JsonResult RequestAddOrUpdateConfigPriceForOne(ConfigPriceViewModel data, int roomClassId)
        {
            var result = _Service.RequestAddOrUpdateConfigPriceForOne(data, roomClassId);
            return new JsonResult() { Data = result };
        }

        public JsonResult GeneralRoomAndFloor()
        {
            _Service.AutoGeneralFloorAndRoom(WorkContext.BizKasaContext.HotelId);
            return new JsonResult() { Data = true };
        }


        public JsonResult GetRoomClass()
        {
           
            var result = _Service.GetRoomClassBy(WorkContext.BizKasaContext.HotelId);
            if (result.HasError)
                return JsonCommonResult.CreateError(result.ToErrorMsg());
            return  new JsonResult() { Data = result };
        }


        [HttpPost]
        public JsonResult GetRoomClassForCheckinMutil()
        {
            // var model = AutoMapper.Mapper.Map<RoomClassModel>(data);
            var result = _Service.GetRoomForCheckinMutil();
            return new JsonResult() { Data = result };
        }

        [HttpPost,SessionFilterAction]
        public JsonResult AddOrder(OrderRowModel model)
        {
                
            model.HotelId = WorkContext.BizKasaContext.HotelId;// hotel
            // var model = AutoMapper.Mapper.Map<RoomClassModel>(data);
            var result = _Service.AddOrder(model);
            return new JsonResult() { Data = result };
        }

        [HttpPost, SessionFilterAction]
        public JsonResult BookingOrder(OrderRowModel model)
        {
            model.HotelId = WorkContext.BizKasaContext.HotelId;// hotel
            model.OrderStatus = (int)OrderStatus.Booking;
            var result = _Service.BookingOrder(model);
            return new JsonResult() { Data = result };
        }



        [HttpPost, SessionFilterAction]
        public JsonResult AddOrderMutil(OrderRowModel model)
        {
            if (model == null) return new JsonResult() { Data = false };
            model.HotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Service.AddOrderMutil(model);
            return new JsonResult() { Data = result };
        }

        [HttpPost]
        public JsonResult GetOrderForEdit( int orderId)
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;// hotel
            var result = _Service.GetOrderForEdit(hotelId, orderId);          
            return new JsonResult() { Data = result };
        }


        [HttpPost]
        public JsonResult GetOrderForCheckOut(int orderId,CaculatorMode mode)
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var result = _Service.GetOrderForCheckOut(hotelId, orderId, mode);

            return new JsonResult() { Data = result };
        }


        [HttpPost, SessionFilterAction]
        public JsonResult UpdateOrder(OrderRowModel model)
        {
            var result = _Service.UpdateOrder(model);

            return new JsonResult() { Data = result };
        }
        //
        [HttpPost]
        public JsonResult GetOrderByCompany()
        {
            var result = _Service.GetOrderByCompany(WorkContext.BizKasaContext.HotelId);

            return new JsonResult() { Data = result };
        }

        [HttpPost]
        public JsonResult CompanyDoCheckOut(List<int> OrderIds,CaculatorMode mode)
        {
            var result = _Service.CompanyCheckOut(OrderIds, WorkContext.BizKasaContext.HotelId, mode);

            return new JsonResult() { Data = result };
        }
          [HttpPost]
        public JsonResult GetRoomsStaticByTime(RoomSearchByTimeModel model)
        {
            var result = _Service.GetRoomsStaticByTime(WorkContext.BizKasaContext.HotelId, model);
            return new JsonResult() { Data = result };
        }

         
          [HttpPost]
          public JsonResult GetBookingOrders(InvoiceFilterModel filter)
          {
              var result = _Service.GetBookingOrders(filter);
              return new JsonResult() { Data = result };
          }

          [HttpPost, SessionFilterAction]
          public JsonResult TranferBookingToCheckIn(int orderid)
          {
              var result = _Service.TranferBookingToCheckIn(orderid);
              if (result.HasError)
                 return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result };
          }
          [HttpPost]
          public JsonResult ChangeRoomInOrder(int orderid, int roomid)
          {
              var result = _Service.ChangeRoomInOrder(orderid, roomid);
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result };
          }


          [HttpPost]
          public JsonResult GetRoomForEdit(int roomid)
          {
              var result = _Service.GetRoomForEdit(roomid);
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result };
          }

          public JsonResult GetListFloor()
          {
              var result = _Service.GetListFloor();
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
          }

          public JsonResult GetListRoomClass()
          {
              var result = _Service.GetListRoomClass();
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
          }

          [HttpPost]
          public JsonResult EditRoom(RoomForEditModel model)
          {
              var result = _Service.EditRoom(model);
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result };
          }
          [HttpPost]
          public JsonResult GetRoomClassById(int id)
          {
              var result = _Service.GetRoomClassById(id);
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result };
          }

          public JsonResult GetRoomUtilityBy(int roomtypeId)
          {
              var result = _Service.GetRoomUtilityBy(roomtypeId);
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result,JsonRequestBehavior=JsonRequestBehavior.AllowGet };
          }



          [HttpPost]
          public JsonResult GetRoomsByClass()
          {
              var result = _Service.GetRoomsByClass();
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result };
          }

          [HttpPost]
          public JsonResult GetRoomsByStatus(RoomStatus status)
          {
              var result = _Service.GetRoomsByStatus(status);
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result };
          }

          [HttpPost]
          public JsonResult DeleteRoom(int roomId)
          {
              var result = _Service.DeleteRoom(roomId);
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result };
          }

          public JsonResult GetStaticRoom()
          {
              var result = _Service.GetStaticRoom();
              return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
          }

          public JsonResult GetPaymentMethod()
          {
              var result =CommonUtil.ToJson(typeof(PaymentMethod));
              return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
          }

          [HttpPost]
          public JsonResult ChangeStatusRoom(int roomId, RoomStatus status)
          {
              var result = _Service.ChangeStatusRoom(roomId, status);
              if (result.HasError)
                  return JsonCommonResult.CreateError(result.ToErrorMsg());
              return new JsonResult() { Data = result };
          }

          public JsonResult InitConfigType()
          {
              var result = CommonUtil.ToJson(typeof(ConfigType));
              return new JsonResult() { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
          }
         
    }
}
