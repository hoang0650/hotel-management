using MyFinance.Domain.Entities;
using MyFinance.Domain.Enum;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{
    public class RoomClassModel
    {
        public RoomClassModel()
        {
            ConfigPrices = new List<ConfigPriceViewModel>();
            Images = new List<GalleryModel>();
        }
        public RoomClassRow RoomClass { get; set; }
        public List<ConfigPriceViewModel> ConfigPrices { get; set; }
        public List<int> RoomTypeFeatureIds { get; set; }
        public List<GalleryModel> Images { get; set; }
       
    }
    public class RoomClassRow {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumBed { get; set; }
        public int NumCustomer { get; set; }
        public int HotelId { get; set; }
        public int UserId { get; set; }


    }

    public class RoomClassViewModel
    {
        public RoomClassViewModel()
        {
            Images = new List<GalleryModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumBed { get; set; }
        public int NumCustomer { get; set; }
        public int HotelId { get; set; }
        public int UserId { get; set; }
        public List<ConfigPriceViewModel> ConfigPrices { get; set; }
        public List<int> RoomTypeFeatureIds { get; set; }
        public List<GalleryModel> Images { get; set; }

    }

    public class ConfigPriceViewModel
    {
        public ConfigPriceRowModel ConfigPriceRow { get; set; }
        public ConfigType ConfigType { get; set; }
        public bool IsDefault { get; set; }
       
       
    }

    public class ConfigPriceRowModel
    {
        public ConfigPriceRowModel()
        {
            CheckoutDayList = new List<RoomAttributeViewModel>();
            CheckoutNightList = new List<RoomAttributeViewModel>();
            CheckinDayList = new List<RoomAttributeViewModel>();
            CheckinNightList = new List<RoomAttributeViewModel>();
            PriceByDayList = new List<RoomAttributeViewModel>();
            AddtionCustomerList = new List<RoomAttributeViewModel>();
            ConfigTime = new List<RoomAttributeViewModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceByDay { get; set; }
        public decimal PriceByNight { get; set; }
        public decimal PriceByMonth { get; set; }
        public int RoomClassId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public List<RoomAttributeViewModel> CheckoutDayList { get; set; }
        public List<RoomAttributeViewModel> CheckoutNightList { get; set; }
        public List<RoomAttributeViewModel> CheckinDayList { get; set; }
        public List<RoomAttributeViewModel> CheckinNightList { get; set; }
        public List<RoomAttributeViewModel> PriceByDayList { get; set; }
        public List<RoomAttributeViewModel> AddtionCustomerList { get; set; }
        public List<RoomAttributeViewModel> ConfigTime { get; set; }



    }

    public class RoomAttributeViewModel
    {
        public int Id { get; set; }
        public int ConfigPriceId { get; set; }
        public int Key { get; set; }
        public decimal Value { get; set; }
        public Additional Additional { get; set; }
    }


    public class RoomsViewAvailableModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoomStatus RoomStatus { get; set; }
      
    }

    public class RoomsClassRowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int RoomAvailable { get; set; }
        public int RoomUsed { get; set; }
        public int RoomTotal { get; set; }
        public List<ConfigPriceRowModel> listConfig { get; set; }
        public List<RoomsViewAvailableModel> Rooms { get; set; }
        public decimal ConfigPriceSelected { get; set; }
       
    }


    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FloorId { get; set; }

        public RoomStatus RoomStatus { get; set; }
        public string ColorStatus { get; set; }
        public int RoomClassId { get; set; }
        public string RoomClassName { get; set; }
        public int HotelId { get; set; }
        public OrderRoomViewModel OrderRoom { get; set; }
    }
    public class FloorRowModel
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public int HotelId { get; set; }
        public List<RoomModel> Rooms { get; set; }
    }
    public class RoomStaticRowModel
    {
        public string id { get; set; }
        public string name { get; set; }
        
        public int value { get; set; }
        
    }
    public class RoomEventRowModel {
        public int id { get; set; }
        public string text { get; set; }
        public string start
        {
            get
            {
                return startDate.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
            }
        }
        public string end
        {
            get
            {
                return endDate.ToUniversalTime().ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff'Z'");
            }

        }
        public string resource { get; set; }
        public string bubbleHtml { get; set; }
        public string status { get; set; }
        public string paid { get; set; }
        public int group { get; set; }
        public string content { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public string className { get; set; }
    }

   

    public class RoomStaticModel
    {
        public List<RoomEventRowModel> events { get; set; }
        public List<RoomStaticRowModel> Rooms { get; set; }
    }
    public class OrderRoomViewModel
    {

        public int OrderId { get; set; }
        public int CustomerNum { get; set; }
        public decimal RoomPrice { get; set; }
        public string CustomerName { get; set; }
        public int? CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string TimeSpend { get; set; }
        public TimeSpan CheckInTime { get; set; }
        public DateTime CheckInDate { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public string TimeToCheckOut { get; set; }

        public CaculatorMode CaculatorMode { get; set; }
        public string CaculatorModeView { get {
                return (int) this.CaculatorMode>0? CommonUtil.GetDisplayName(CaculatorMode):string.Empty;
            } }
        public int OrderStatus { get; set; }
    }

    public class RoomForEditModel
    {
        public int RoomId { get; set; }
        public int RoomClassId { get; set; }
        public int FloorId { get; set; }
        public string RoomName { get; set; }
        
        
    }

    public class RoomTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }


    }
    public class ColorStatus
    {
        public string RoomStatus { get; set; }
        public string Color { get; set; }
        public string RoomStatusName { get; set; }


    }
}
