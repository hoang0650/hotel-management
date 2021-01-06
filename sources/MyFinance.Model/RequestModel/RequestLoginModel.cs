using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Model.RequestModel
{
   public class RequestLoginModel:RequestBase
    {
       public string Email { get; set; }
       public string Password { get; set; }
    }

   public class RoomRequestModel
   {
       public RoomStatus status { get; set; }
       public int RoomId { get; set; }
       
   }

   public class OrderRequestModel
   {
       public CaculatorMode Mode { get; set; }
       public int OrderId { get; set; }
     

   }
    
}
