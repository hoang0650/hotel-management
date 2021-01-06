using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{
   public class RoomSearchByTimeModel
    {
       public DateTime? FromDate { get; set; }
       public DateTime? ToDate { get; set; }
       public TimeSpan? FromTime { get; set; }
       public TimeSpan? ToTime { get; set; }
       public SearchRoomBy SearchBy { get; set; }
    }
}
