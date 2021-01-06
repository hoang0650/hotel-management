using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Proxy
{
    public interface IRoomProxyServices : IBusinessBase
    {
        string GetStaticRoom();
        List<FloorRowModel> GetRoomsByFloor();
        List<FloorRowModel> GetRoomsByClass();
        List<FloorRowModel> GetRoomsByStatus(RoomStatus status);
        List<ConfigPriceRowModel> GetConfigPriceByRoom(int roomid);
        List<RoomsClassRowModel> GetRoomForCheckinMutil();
        RoomForEditModel GetRoomForEdit(int roomId);
        List<RoomClassRow> GetListRoomClass();
        bool EditRoom(RoomForEditModel model);
        bool ChangeStatusRoom(int roomId, RoomStatus status);
        bool DeleteRoom(int roomId);
        List<RoomsClassRowModel> GetRoomAvailable(DateTime? fromDate, DateTime? toDate);
        RoomStaticModel GetRoomsStaticByTime(RoomSearchByTimeModel model);
        List<RoomClassModel> GetRoomClassBy(int hotelId);
        List<RoomClassModel> AddRoomClass(RoomClassModel data);
        RoomClassModel GetRoomClassById(int roomclassId);
        List<RoomTypeViewModel> GetRoomUtilityBy(int roomTypeId);
        bool AddRoom(List<RoomForEditModel> model);
        List<RoomForEditModel> GetListRooms();
        List<ConfigPriceRowModel> GetConfigPriceBy(int roomid);
        bool RefreshRoom(int roomid);
        List<RoomClassModel> RequestAddOrUpdateConfigPriceForOne(ConfigPriceViewModel data, int roomClassId);
        bool DeleteRoomClass(List<int> Ids);
        bool DeleteConfigPrice(List<int> Ids);
        
    }

    public class RoomProxyServices : BaseProxyService, IRoomProxyServices
    {
        public bool DeleteConfigPrice(List<int> Ids)
        {
            string url = "api/Room/DeleteConfigPrice";
            return PostStructService<bool>(new { Ids = Ids }, url);
        }  
        public bool DeleteRoomClass(List<int> Ids)
        {
            string url = "api/Room/DeleteRoomClass";
            return PostStructService<bool>(new { Ids = Ids }, url);
        }  
        public List<RoomClassModel> RequestAddOrUpdateConfigPriceForOne(ConfigPriceViewModel data, int roomClassId)
        {
            string url = "api/Room/RequestAddOrUpdateConfigPriceForOne";
            return PostService<List<RoomClassModel>>(new { ConfigPrice = data, RoomClassId = roomClassId }, url);
        }
        public bool RefreshRoom(int roomid)
        {
            string url = "api/Room/RefreshRoom";
            return PostStructService<bool>(new { RoomId = roomid }, url);
        }
        public List<ConfigPriceRowModel> GetConfigPriceBy(int roomid)
        {
            string url = "api/Room/GetConfigPriceBy";
            return PostService<List<ConfigPriceRowModel>>(new { RoomId = roomid }, url);
        }
        public List<RoomForEditModel> GetListRooms()
        {
            string url = "api/Room/GetListRooms";
            return GetDataService<List<RoomForEditModel>>(url);
        }
        public bool AddRoom(List<RoomForEditModel> model)
        {
            try
            {
                string url = "api/Room/InsertRoom";
                return PostStructService<bool>(model,url);
            }
            catch (Exception)
            {
                this.AddError("Lỗi lấy dữ liệu! ");
                return false;
            }
        }
        public List<RoomTypeViewModel> GetRoomUtilityBy(int roomTypeId)
        {
            string url = "api/Room/GetRoomUtilityBy";
            return PostService<List<RoomTypeViewModel>>(new { RoomTypeId = roomTypeId }, url);
        }
        public RoomClassModel GetRoomClassById(int roomclassId)
        {
            string url = "api/Room/GetRoomClassById";
            return PostService<RoomClassModel>(new { RoomClassId = roomclassId }, url);

        }
        public List<RoomClassModel> AddRoomClass(RoomClassModel model)
        {
            string url = "api/Room/AddRoomClass";
            return PostService<List<RoomClassModel>>(model, url);
         
        }
        public List<RoomClassModel> GetRoomClassBy(int hotelId)
        {
            string url = "api/Room/GetRoomClass";
            return GetDataService<List<RoomClassModel>>( url);

        }
        public RoomStaticModel GetRoomsStaticByTime( RoomSearchByTimeModel model)
        {
            string url = "api/Room/GetRoomsStaticByTime";
            return PostService<RoomStaticModel>(model, url);

        }
        public List<RoomsClassRowModel> GetRoomAvailable(DateTime? fromDate, DateTime? toDate)
        {
            string url = "api/Room/GetRoomAvailable";
            return PostService<List<RoomsClassRowModel>>(new { Fromdate = fromDate.Value, ToDate = toDate.Value }, url);

        }
        public bool DeleteRoom(int roomId)
        {
            string url = "api/Room/DeleteRoom";
            return PostStructService<bool>(new { RoomId = roomId }, url);

        }
        public bool ChangeStatusRoom(int roomId, RoomStatus status)
        {
            string url = "api/Room/ChangeStatusRoom";
            return PostStructService<bool>(new { RoomId = roomId, status = status }, url);
        }
        public bool EditRoom(RoomForEditModel model)
        {
            string url = "api/Room/EditRoom";
            return PostStructService<bool>(model, url);

         
        }
        public List<RoomClassRow> GetListRoomClass()
        {
            string url = "api/Room/GetListRoomClass";
            return GetDataService<List<RoomClassRow>>(url);


            
        }
        public RoomForEditModel GetRoomForEdit(int roomId)
        {
            string url = "api/Room/GetRoomForEdit";
            return PostService<RoomForEditModel>(new { RoomId = roomId }, url);

        }
        public string GetStaticRoom()
        {
            string url = "api/Room/GetStaticRoom";
            return GetDataService<string>(url);

        }
        public List<FloorRowModel> GetRoomsByFloor()
        {
            string url = "api/Room/GetRoomsByFloor";
            return GetDataService<List<FloorRowModel>>(url);

           
        }

        public List<FloorRowModel> GetRoomsByClass()
        {
            string url = "api/Room/GetRoomsByClass";
            return GetDataService<List<FloorRowModel>>(url);

        }

        public List<FloorRowModel> GetRoomsByStatus(RoomStatus status)
        {
            string url = "api/Room/GetRoomsByStatus";
            return PostService<List<FloorRowModel>>(new { status = status }, url);



        }

        public List<ConfigPriceRowModel> GetConfigPriceByRoom(int roomid)
        {

            string url = "api/Room/GetConfigPriceByRoom";
            return PostService<List<ConfigPriceRowModel>>(new { RoomId = roomid }, url);
           

        }
        public List<RoomsClassRowModel> GetRoomForCheckinMutil()
        {
            string url = "api/Room/GetRoomClassForCheckinMutil";
            return GetDataService<List<RoomsClassRowModel>>(url);


        }
    }
}
