using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
 public  class Shift
    {
        [Key]
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public Nullable<DateTime> EndTime { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public decimal OpenAmount { get; set; }
        public decimal CloseAmount { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal DeliveryAmount { get; set; }
        public decimal DeliveryManagerAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public bool IsDeleted { get; set; }
        [MaxLength(300)]
        public string Notes { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
    }
}
