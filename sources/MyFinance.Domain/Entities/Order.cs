using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Nullable<int> ShiftId { get; set; }
        public Nullable< int> ParentId { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public int ConfigPriceId { get; set; }
        public Nullable<int> CompanyId { get; set; }
        [MaxLength(100)]
        public string CustomerName { get; set; }
        [MaxLength(100)]
        public string PassportId { get; set; }
        [MaxLength(100)]
        public string CompanyName { get; set; }
        [MaxLength(50)]
        public string Card { get; set; }
        [MaxLength(50)]
        public string NumberVehicle { get; set; }

        [MaxLength(200)]
        public string Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Price { get; set; }
        public decimal Surcharge { get; set; }
        public decimal Deductible { get; set; }
        public decimal Prepaid { get; set; }
        public decimal Cashed { get; set; }

        public int OrderStatus { get; set; }

        public int PaymentMethod { get; set; }
        public CaculatorMode CaculatorMode { get; set; }


        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public Nullable<DateTime> CheckInDate { get; set; }
        public Nullable<TimeSpan> CheckInTime { get; set; }
        public Nullable<DateTime> CheckOutDate { get; set; }
        public Nullable<TimeSpan> CheckOutTime { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("RoomId")]
        public virtual Room Room { get; set; }
        public virtual ICollection<OrderService> OrderServices { get; set; }
        public virtual ICollection<OrderCustomer> OrderCustomers { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
    public  class OrderDetail
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DetailTypeId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubAmount { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public int ShiftId { get; set; }
        public Nullable<int> RelatedId { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [ForeignKey("ShiftId")]
        public virtual Shift Shift { get; set; }
    }
    public class OrderService
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ServiceId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        [ForeignKey("ServiceId")]
        public Widget Service { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

    }

    public class OrderCustomer
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int CustomerId { get; set; }


        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }

    


}
