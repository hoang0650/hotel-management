using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
    public class InvoiceDetail
    {

        [Key]
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public int CategoryInvoice { get; set; }
        [MaxLength(300)]
        public string Descriptions { get; set; }
        [MaxLength(50)]
        public string Unit { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubAmount { get; set; }
        [MaxLength(500)]
        public string Notes { get; set; }
        public int HotelId { get; set; }
        public Nullable<int> ServiceId { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }

        [MaxLength(100)]
        public string UserUpdate { get; set; }
        public int UserId { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }

        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
    }
}
