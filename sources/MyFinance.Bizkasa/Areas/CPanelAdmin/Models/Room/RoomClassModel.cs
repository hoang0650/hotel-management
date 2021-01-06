using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Models.Room
{
    public class RoomViewClassModel
    {
        public string Name { get; set; }
        public int NumBed { get; set; }
        public int NumCustomer { get; set; }
        public decimal PriceByDay { get; set; }
        public decimal PriceByNight { get; set; }
        public decimal PriceByMonth { get; set; }
        public int HotelId { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }

        public List<RoomAttribute> CheckoutDayList { get; set; }
        public List<RoomAttribute> CheckoutNightList { get; set; }
        public List<RoomAttribute> CheckinDayList { get; set; }
        public List<RoomAttribute> CheckinNightList { get; set; }
        public List<RoomAttribute> PriceByDayList { get; set; }
        public List<RoomAttribute> AddtionCustomerList { get; set; }
     

    }
    public class RoomAttribute
    {
        public int Key { get; set; }
        public decimal Value { get; set; }
     

    }
}