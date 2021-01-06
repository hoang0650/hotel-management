using System;
using System.Collections.Generic;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Business;
using System.ServiceModel;

namespace MyFinance.ApiService
{
    [ServiceContract]
    public interface IRoomService
    {
        Response<List<RoomClassModel>> AddRoomClass(RoomClassModel data);
        Response<List<RoomClassModel>> GetRoomClassBy(int hotelId);
        Response AutoGeneralFloorAndRoom(int hoteid);
        [OperationContract]
        Response<List<FloorRowModel>> GetRoomsByFloor(int hotelId);
        Response<List<ConfigPriceRowModel>> GetConfigPriceByRoom(int roomid);
        Response<List<RoomsClassRowModel>> GetRoomForCheckinMutil();

        Response<RoomStaticModel> GetRoomsStaticByTime(int hotelId, RoomSearchByTimeModel model);
        Response<RoomForEditModel> GetRoomForEdit(int roomId);
        Response<List<FloorModel>> GetListFloor();
        Response<List<RoomClassRow>> GetListRoomClass();
        Response<bool> EditRoom(RoomForEditModel model);
        Response<bool> DeleteRoom(int roomid);
        Response<bool> InsertOrUpdateFloor(FloorModel model);
        Response<FloorModel> GetFloorBy(int floorId);
        Response<RoomClassModel> GetRoomClassById(int roomtypeId);
        Response<List<RoomTypeViewModel>> GetRoomUtilityBy(int roomTypeId);
        Response<List<FloorRowModel>> GetRoomsByClass();
        Response<List<RoomForEditModel>> GetListRooms();
        Response<bool> AddRoom(List<RoomForEditModel> data);
        Response<string> GetStaticRoom();
        Response<List<FloorRowModel>> GetRoomsByStatus(RoomStatus status);
        Response<bool> ChangeStatusRoom(int roomId, RoomStatus status);
        Response<List<RoomClassModel>> RequestAddOrUpdateConfigPriceForOne(ConfigPriceViewModel data,int roomClassId);
        Response<List<RoomsClassRowModel>> GetRoomAvailable(DateTime? fromDate, DateTime? toDate);
        Response<List<ConfigPriceRowModel>> GetConfigPriceBy(int roomid);
        Response<bool> RefreshRoom(int roomid);
        Response<bool> DeleteFloor(List<int> ids);
        Response<bool> DeleteRoomClass(List<int> ids);
        Response<bool> DeleteConfigPrice(List<int> ids);
        Response<List<RoomModel>> GetRoomsByShort();
        // void SaveCategory();
    }

    public partial class TikasaService {
        public Response<bool> DeleteConfigPrice(List<int> ids)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().DeleteConfigPrice(ids);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> DeleteRoomClass(List<int> ids)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().DeleteRoomClass(ids);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> DeleteFloor(List<int> ids)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().DeleteFloor(ids);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> RefreshRoom(int roomid)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().RefreshRoom(roomid);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<ConfigPriceRowModel>> GetConfigPriceBy(int roomid)
        {
            List<ConfigPriceRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetConfigPriceBy(roomid);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<RoomsClassRowModel>> GetRoomAvailable(DateTime? fromDate, DateTime? toDate)
        {
            List<RoomsClassRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomAvailable(fromDate, toDate);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        
        public Response<List<RoomClassModel>> RequestAddOrUpdateConfigPriceForOne(ConfigPriceViewModel data, int roomClassId)
        {
            List<RoomClassModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().RequestAddOrUpdateConfigPriceForOne(data, roomClassId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> ChangeStatusRoom(int roomId, RoomStatus status)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().ChangeStatusRoom(roomId,status);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<FloorRowModel>> GetRoomsByStatus(RoomStatus status)
        {
            List<FloorRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomsByStatus(status);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<string> GetStaticRoom()
        {
            string result = string.Empty;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetStaticRoom();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
      
        public Response< bool> AddRoom(List<RoomForEditModel> data)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().AddRoom(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response< List<RoomForEditModel>> GetListRooms()
        {
            List<RoomForEditModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetListRooms();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response< List<FloorRowModel>> GetRoomsByClass()
        {
            List<FloorRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomsByClass();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<RoomTypeViewModel>> GetRoomUtilityBy(int roomTypeId)
        {
            List<RoomTypeViewModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomUtilityBy(roomTypeId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<RoomClassModel> GetRoomClassById(int roomtypeId)
        {
            RoomClassModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomClassById(roomtypeId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<FloorModel> GetFloorBy(int floorId)
        {
            FloorModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetFloorBy(floorId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> InsertOrUpdateFloor(FloorModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().InsertOrUpdateFloor(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> DeleteRoom(int roomid)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().DeleteRoom(roomid);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> EditRoom(RoomForEditModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().EditRoom(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<RoomClassRow>> GetListRoomClass()
        {
            List<RoomClassRow> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetListRoomClass();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<FloorModel>> GetListFloor()
        {
            List<FloorModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetListFloor();
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<RoomForEditModel> GetRoomForEdit(int roomId)
        {
            RoomForEditModel  result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomForEdit(roomId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<RoomClassModel>> AddRoomClass(RoomClassModel data)
        {
            List<RoomClassModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().AddRoomClass(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<List<RoomClassModel>> GetRoomClassBy(int hotelId)
        {
            List<RoomClassModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomClassBy(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<List<RoomModel>> GetRoomsByShort()
        {
            List<RoomModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomsByShort();
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<FloorRowModel>> GetRoomsByFloor(int hotelId)
        {
            List<FloorRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomsByFloor(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }





        public Response<RoomStaticModel> GetRoomsStaticByTime(int hotelId, RoomSearchByTimeModel model)
        {
            RoomStaticModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomsStaticByTime(hotelId, model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }



        public Response<List<RoomsClassRowModel>> GetRoomForCheckinMutil()
        {
            List<RoomsClassRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetRoomForCheckinMutil();
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response AutoGeneralFloorAndRoom(int hoteid)
        {
            BusinessProcess.Current.Process(p =>
            {
                IoC.Get<IRoomBusiness>().AutoGeneralFloorAndRoom(hoteid);
            });

            return BusinessProcess.Current.ToResponse();
        }

        public Response<List<ConfigPriceRowModel>> GetConfigPriceByRoom(int roomid)
        {
            List<ConfigPriceRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomBusiness>().GetConfigPriceByRoom(roomid);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        



    }
}
