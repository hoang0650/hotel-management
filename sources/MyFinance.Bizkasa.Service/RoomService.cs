using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Domain.Entities;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Proxy;

namespace MyFinance.Bizkasa.Service
{
    public interface IRoomService
    {
        Response<List<RoomClassModel>> AddRoomClass(RoomClassModel data);
        Response<List<RoomClassModel>> GetRoomClassBy(int hotelId);
        Response<List<FloorRowModel>> GetRoomsByFloor();
        Response<List<ConfigPriceRowModel>> GetConfigPriceByRoom(int roomid);
        Response<List<RoomsClassRowModel>> GetRoomForCheckinMutil();

        Response<RoomStaticModel> GetRoomsStaticByTime( RoomSearchByTimeModel model);
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
        // void SaveCategory();
        Response<List<RoomsClassRowModel>> GetRoomAvailable(DateTime? fromDate, DateTime? toDate);
        Response<List<ConfigPriceRowModel>> GetConfigPriceBy(int roomid);
        Response<bool> RefreshRoom(int roomid);
        Response<bool> DeleteFloor(List<int> Ids);
        Response<bool> DeleteRoomClass(List<int> Ids);
        Response<bool> DeleteConfigPrice(List<int> Ids);
    }

    public partial class TikasaService {
        public Response<bool> DeleteConfigPrice(List<int> Ids)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().DeleteConfigPrice(Ids);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        
        public Response<bool> DeleteRoomClass(List<int> Ids)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().DeleteRoomClass(Ids);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<bool> DeleteFloor(List<int> Ids)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IFloorProxyService>().DeleteFloor(Ids);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> RefreshRoom(int roomid)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().RefreshRoom(roomid);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<ConfigPriceRowModel>> GetConfigPriceBy(int roomid)
        {
            List<ConfigPriceRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetConfigPriceBy(roomid);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<List<RoomsClassRowModel>> GetRoomAvailable(DateTime? fromDate, DateTime? toDate)
        {
            List<RoomsClassRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetRoomAvailable(fromDate, toDate);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<RoomClassModel>> RequestAddOrUpdateConfigPriceForOne(ConfigPriceViewModel data, int roomClassId)
        {
            List<RoomClassModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().RequestAddOrUpdateConfigPriceForOne(data, roomClassId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> ChangeStatusRoom(int roomId, RoomStatus status)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().ChangeStatusRoom(roomId, status);//IoC.Get<IRoomBusiness>().ChangeStatusRoom(roomId,status);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<FloorRowModel>> GetRoomsByStatus(RoomStatus status)
        {
            List<FloorRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetRoomsByStatus(status);//IoC.Get<IRoomBusiness>().GetRoomsByStatus(status);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<string> GetStaticRoom()
        {
            string result = string.Empty;
            BusinessProcess.Current.Process(p =>
            {
                //result = IoC.Get<IRoomBusiness>().GetStaticRoom();
                result = IoC.Get<IRoomProxyServices>().GetStaticRoom();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
      
        public Response< bool> AddRoom(List<RoomForEditModel> data)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().AddRoom(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response< List<RoomForEditModel>> GetListRooms()
        {
            List<RoomForEditModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetListRooms();//IoC.Get<IRoomBusiness>().GetListRooms();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response< List<FloorRowModel>> GetRoomsByClass()
        {
            List<FloorRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetRoomsByClass();//IoC.Get<IRoomBusiness>().GetRoomsByClass();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<RoomTypeViewModel>> GetRoomUtilityBy(int roomTypeId)
        {
            List<RoomTypeViewModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetRoomUtilityBy(roomTypeId);//IoC.Get<IRoomBusiness>().GetRoomUtilityBy(roomTypeId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<RoomClassModel> GetRoomClassById(int roomtypeId)
        {
            RoomClassModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetRoomClassById(roomtypeId);//IoC.Get<IRoomBusiness>().GetRoomClassById(roomtypeId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<FloorModel> GetFloorBy(int floorId)
        {
            FloorModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IFloorProxyService>().GetFloorBy(floorId);//IoC.Get<IRoomBusiness>().GetFloorBy(floorId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> InsertOrUpdateFloor(FloorModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IFloorProxyService>().InsertOrUpdateFloor(model);// IoC.Get<IRoomBusiness>().InsertOrUpdateFloor(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> DeleteRoom(int roomid)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().DeleteRoom(roomid);//IoC.Get<IRoomBusiness>().DeleteRoom(roomid);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> EditRoom(RoomForEditModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().EditRoom(model);//IoC.Get<IRoomBusiness>().EditRoom(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<RoomClassRow>> GetListRoomClass()
        {
            List<RoomClassRow> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetListRoomClass();//IoC.Get<IRoomBusiness>().GetListRoomClass();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<FloorModel>> GetListFloor()
        {
            List<FloorModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IFloorProxyService>().GetListFloor();//IoC.Get<IRoomBusiness>().GetListFloor();
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<RoomForEditModel> GetRoomForEdit(int roomId)
        {
            RoomForEditModel  result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetRoomForEdit(roomId);//IoC.Get<IRoomBusiness>().GetRoomForEdit(roomId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<RoomClassModel>> AddRoomClass(RoomClassModel data)
        {
            List<RoomClassModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().AddRoomClass(data);//IoC.Get<IRoomBusiness>().AddRoomClass(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<List<RoomClassModel>> GetRoomClassBy(int hotelId)
        {
            List<RoomClassModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetRoomClassBy(hotelId);//IoC.Get<IRoomBusiness>().GetRoomClassBy(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<FloorRowModel>> GetRoomsByFloor()
        {
            List<FloorRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetRoomsByFloor();// IoC.Get<IRoomBusiness>().GetRoomsByFloor(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }





        public Response<RoomStaticModel> GetRoomsStaticByTime( RoomSearchByTimeModel model)
        {
            RoomStaticModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetRoomsStaticByTime(model);//IoC.Get<IRoomBusiness>().GetRoomsStaticByTime(hotelId, model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }



        public Response<List<RoomsClassRowModel>> GetRoomForCheckinMutil()
        {
            List<RoomsClassRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetRoomForCheckinMutil();//IoC.Get<IRoomBusiness>().GetRoomForCheckinMutil();
            });

            return BusinessProcess.Current.ToResponse(result);
        }

      

        public Response<List<ConfigPriceRowModel>> GetConfigPriceByRoom(int roomid)
        {
            List<ConfigPriceRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IRoomProxyServices>().GetConfigPriceByRoom(roomid);// IoC.Get<IRoomBusiness>().GetConfigPriceByRoom(roomid);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        



    }
}
