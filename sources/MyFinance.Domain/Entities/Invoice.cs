using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
   public class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new List<InvoiceDetail>();
        }
       [Key]
       public int Id { get; set; }
       public Nullable<int> OrderId { get; set; }
       [MaxLength(100)]
       public string CustomerName { get; set; }
       [MaxLength(100)]
       public string CompanyName { get; set; }
       [MaxLength(50)]
       public string RoomName { get; set; }
       [MaxLength(50)]
       public string RoomClassName { get; set; }
      

       [MaxLength(200)]
       public string Notes { get; set; }
       [MaxLength(200)]
       public string Address { get; set; }
       [MaxLength(50)]
       public string Mobile { get; set; }
       [MaxLength(50)]
       public string Email { get; set; }

       public decimal Surcharge { get; set; }
       public decimal Deductible { get; set; }
       public decimal Prepay { get; set; }
       public decimal Cashed { get; set; }
       public decimal TotalAmount { get; set; }
       public int InvoiceStatus { get; set; }
       public int InvoiceType { get; set; }
       public int PaymentMethod { get; set; }
       public Nullable<DateTime> CheckInDate { get; set; }
       public Nullable<DateTime> CheckOutDate { get; set; }
       public int HotelId { get; set; }
       public Nullable<DateTime> CreatedDate { get; set; }
       public Nullable<DateTime> UpdatedDate { get; set; }

       [MaxLength(100)]
       public string UserUpdate { get; set; }
       public int UserId { get; set; }

       [ForeignKey("HotelId")]
       public virtual Hotel Hotel { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
