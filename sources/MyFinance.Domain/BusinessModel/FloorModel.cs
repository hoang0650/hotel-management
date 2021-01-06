using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{
   public class FloorModel
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public int NumRooms { get; set; }
       public int RoomClassId { get; set; }
    }
   public class FloorRequestModel
   {
       public List<int> Ids { get; set; }
   }
}
