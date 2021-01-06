using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFinance.Utils;

namespace MyFinance.Domain.BusinessModel
{
   public class SystemConfigModel
    {

       public int HotelId { get; set; }
       public int TimeCheckOut { get; set; }
       public int TimeCheckIn { get; set; }
       public int StartOverNight { get; set; }
       public int EndOverNight { get; set; }
       public decimal OverCustomer { get; set; }
       public string RoomStatusColor { get; set; }
       public bool HasCleaner { get; set; }
       public int TimeRound { get; set; }    
    }

   public class HistoryModel
   {

       public int Id { get; set; }
       public int HotelId { get; set; }
       public string HotelName { get; set; }
       public int UserId { get; set; }
       public string UserName { get; set; }
       public DateTime CreatedDate { get; set; }
       public string CreatedDateView { get {
           return CreatedDate.ToStringVN();
       }}
       public string Content { get; set; }

   }
}
