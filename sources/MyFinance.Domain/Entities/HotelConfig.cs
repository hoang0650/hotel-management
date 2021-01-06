using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
   public class HotelConfig
   {
       [Key]
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int StartOverNight { get; set; }
        public int EndOverNight { get; set; }
        public int TimeCheckOut { get; set; }
        public int TimeCheckIn { get; set; }
        public int TimeRound { get; set; }
        public bool HasCleaner { get; set; }
       [MaxLength(200)]
       public string RoomStatusColor { get; set; }
        public decimal OverCustomer { get; set; }  
     
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
    }
}
