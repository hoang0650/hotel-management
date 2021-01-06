using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.RequestModel
{
   public class RequestLoginModel
    {
       public string Email { get; set; }
       public string Password { get; set; }
        public string Token { get; set; }
        public int HotelId { get; set; }
    }

   public class RoomRequestModel
   {
       public RoomStatus status { get; set; }
       public int RoomId { get; set; }
       public int RoomTypeId { get; set; }
       public int RoomClassId { get; set; }
       public DateTime FromDate { get;set; }
       public DateTime ToDate { get; set; }
       public ConfigPriceViewModel ConfigPrice { get; set; }
   }

   public class OrderRequestModel
   {
       public int Mode { get; set; }
       public int OrderId { get; set; }
       public int RoomId { get; set; }
       public int ConfigPriceId { get; set; }
       public List<int> OrderIds { get; set; }
       public int CompanyId { get; set; }
       public int TypeCustomer { get; set; }

   }

   public class AccountRequestModel
   {
       public int UserId { get; set; }
       public string UserName { get; set; }
      
   }

    
}
