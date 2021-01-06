using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
    public class Customer
    {

        [Key]
        [Required]
        public int Id { get; set; }
        public int HotelId { get; set; }

        public string PassportId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(200)]
        public string Notes { get; set; }
        [MaxLength(20)]
        public string Mobile { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }
        public Nullable<DateTime> PassportCreatedDate { get; set; }

        public string PassportAgency { get; set; }

        public bool IsHasCheckin { get; set; }

        public CustomerType CustomerType { get; set; }

        [MaxLength(100)]
        public string National { get; set; }
        public Nullable<DateTime> BirthDate { get; set; }

        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }      

        public ICollection<OrderCustomer> OrderCustomers { get; set; }

    }

   
}
