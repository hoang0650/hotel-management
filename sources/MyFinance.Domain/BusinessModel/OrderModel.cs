using MyFinance.Domain.Entities;
using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFinance.Utils;


namespace MyFinance.Domain.BusinessModel
{

    public class OrderRowModel
    {
        public OrderRowModel()
        {
            Customers = new List<CustomerRowModel>();
            TimeUseds = new List<TimeUsed>();
        }
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int RoomId { get; set; }
        public int? CustomerId { get; set; }
        public int ConfigPriceId { get; set; }
       
        public string CustomerName { get; set; }
        public string PassportId { get; set; }
        public string RoomName { get; set; }
       
        public string Card { get; set; }
        public string NumberVehicle { get; set; }
        public string Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Price { get; set; }
        public decimal Surcharge { get; set; }
        public decimal Deductible { get; set; }
        public decimal Prepaid { get; set; }
        public decimal Cashed { get; set; }
        public int OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int PaymentId { get; set; }
        public CaculatorMode CaculatorMode { get; set; }
        public DateTime? CheckInDate { get; set; }
        public DateTime? CheckOutDate { get; set; }

        public Nullable<TimeSpan> CheckOutTime { get; set; }
        public Nullable<TimeSpan> CheckInTime { get; set; }
        public List<CustomerRowModel> Customers { get; set; }
        public CustomerRowModel Company { get; set; }
        public List<ServiceRowModel> Services { get; set; }
        public List<ConfigPriceSelected> RoomIds { get; set; }
        public List<OrderRowAttachment> OrderAttachment { get; set; }
        public List<TimeUsed> TimeUseds { get; set; }
       
    }
    public class MergeOrderModel
    {
        /// <summary>
        /// from order
        /// </summary>
        public int SourceId { get; set; }
        /// <summary>
        /// from room name
        /// </summary>
        public string SourceRoomName { get; set; }
        /// <summary>
        /// To order
        /// </summary>
        public int DestinationId { get; set; }
    }
    public class ConfigPriceSelected {
        public int RoomId { get; set; }
        public int ConfigPriceId { get; set; }
    }

    public class OrderRowForCheckoutModel
    {
        
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int ShiftId { get; set; }
        public int RoomId { get; set; }
        public int ConfigPriceId { get; set; }
        public int? CustomerId { get; set; }
        public string PassportId { get; set; }
        public string CustomerName { get; set; }
        public string RoomClassName { get; set; }
        public string RoomName { get; set; }
        public string CompanyName { get; set; }
        public string Card { get; set; }
        public string NumberVehicle { get; set; }
        public string Notes { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal Price { get; set; }
        public decimal PriceByDay { get; set; }
        public decimal PriceOverNight { get; set; }
        public decimal PriceByHour { get; set; }
        public decimal Surcharge { get; set; }
        public decimal Deductible { get; set; }
        public decimal Prepaid { get; set; }
        public decimal Cashed { get; set; }
        public int OrderStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public CaculatorMode CaculatorMode { get; set; }

        public TimeSpan CheckInTime { get; set; }
        public TimeSpan CheckOutTime { get; set; }
        public DateTime CheckInDate { get; set; }
        public string CheckInDateView { get{
            return CheckInDate.ToStringDateVN();
        }} 
        public string CheckOutDateView {
            get
            {
                return DateTime.Now.ToStringDateVN();
            }
        }
        public DateTime? CheckOutDate { get; set; }
        public List<ServiceRowModel> Services { get; set; }
        public List<CustomerRowModel> Customers { get; set; }
        public List<OrderRowAttachment> OrderAttachments { get; set; }
        public List<TimeUsed> TimeUseds { get; set; }
        public bool isByNight { get; set; }
        public decimal SubAmount { get; set; }
        public decimal AttachmentAmount { get; set; }
        public decimal SurchargeAmount { get; set; }
        public decimal DeductibleAmount { get; set; }
        public decimal PrepaidAmount { get; set; }
        public decimal MiniBarAmount { get; set; }
        public decimal ServiceAmount { get; set; }
        public decimal RoomAmount { get; set; }
        public List<OrderDetailDTO> OrderDetails { get; set; }
        public List<OrderDetailDTO> MiniBars { get; set; }
        public List<OrderDetailDTO> Surcharges { get; set; }
        public List<OrderDetailDTO> Deductibles { get; set; }
        public List<OrderDetailDTO> Prepaids { get; set; }

    }
    public class OrderDetailDTO
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int DetailTypeId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal SubAmount { get; set; }
        public string Title { get; set; }
        public string Note { get; set; }
        public int ShiftId { get; set; }
        public int? RelatedId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime? UpdatedDate { get; set; }
    }
    public class TimeUsed {
        public int UnitUsed { get; set; }
        public string Description { get; set; }
        public decimal SumAmount { get; set; }

    }

    public class CustomerRowModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public bool IsPrimary { get; set; }
        public string PassportId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthDateView { get
            {
                return this.BirthDate.HasValue ? this.BirthDate.Value.ToStringDateVN() : string.Empty;
            } }
        public DateTime? PassportCreatedDate { get; set; }
        public string PassportAgency { get; set; }
        public CustomerType CustomerType { get; set; }
        public string National { get; set; }

     
    }
    public class ServiceRowModel
    {

        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedDate { get; set; }

    }

    public class OrderRowCompany
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public List<OrderRowForCheckoutModel> Orders { get; set; }
    }

    public class OrderRowForCompanyCheckOut {
        public OrderRowForCheckoutModel OrderKey { get; set; }
        public List<OrderRowAttachment> OrderAttach { get; set; }

    }
    public class OrderRowAttachment {
        public int OrderId { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string Note { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class OrderBookingRowModel
    {
        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public DateTime? CheckInDate { get; set; }
        public TimeSpan? CheckInTime { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public TimeSpan? CheckOutTime { get; set; }
        public DateTime CreatedDate { get; set; }
        public string RoomName { get; set; }
        public string CustomerName { get; set; }
    }


    public class Folio
    {
        public Folio()
        {
            Customers = new List<CustomerRowModel>();
            FolioItems = new List<FolioItem>();
        }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public List<CustomerRowModel> Customers { get; set; }
        public List<FolioItem> FolioItems { get; set; }

    }
    public class FolioItem
    {
        public string RoomName { get; set; }
        public string RoomClassName { get; set; }
        public decimal Price { get; set; }
    }


    public class OrderFilterModel {
        public int OrderStatus { get; set; }
        public PagingModel Page { get; set; }
    }


    public class ChangeRoomInOrderModel
    {
        public int orderId { get; set; }
        public int hotelId { get; set; }
        public int roomId { get; set; }
        public int configPriceId { get; set; }
        public bool isOnlyChangeRoom { get; set; }
    }
}
