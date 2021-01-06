using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFinance.Utils;

namespace MyFinance.Domain.BusinessModel
{
    public class InvoiceResponse
    {
        public DataPaging<List<InvoiceRowModel>> DataPaging { get; set; }
        public InvoiceSummary Summary { get; set; }
    }
    public class InvoiceResult
    {
        public List<InvoiceRowModel> Data { get; set; }
        public InvoiceSummary Summary { get; set; }
    }
    public class InvoiceSummary
    {
        public decimal TotalAmount { get; set; }
        public decimal DeductibleAmount { get; set; }
    }
    public class InvoiceRowModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ShiftId { get; set; }
        public string CustomerName { get; set; }
        public string PassportId { get; set; }
        public DateTime? PassportCreated { get; set; }
        public string PassportCreatedView { get {
                return this.PassportCreated.HasValue ? this.PassportCreated.Value.ToStringDateVN() : string.Empty;
            } }
        public string CompanyName { get; set; }

        public string RoomName { get; set; }
        public string RoomClassName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public decimal Surcharge { get; set; }
        public decimal Deductible { get; set; }
        public decimal Prepaid { get; set; }
        public decimal Cashed { get; set; }
        public decimal TotalAmount { get; set; }
        public int InvoiceStatus { get; set; }
        public string InvoiceStatusView
        {
            get
            {
                string statusName = string.Empty;
                switch (InvoiceStatus)
                {
                    case 1:
                        statusName = "Chưa thanh toán";
                        break;
                    case 2:
                        statusName = "Công nợ";
                        break;
                    case 7:
                        statusName = "Đã thanh toán";
                        break;
                }
                return statusName;
            }
        }
        public int InvoiceType { get; set; }
        public int PaymentMethod { get; set; }
        public int HotelId { get; set; }
        public string UserUpdate { get; set; }
        public int UserId { get; set; }
        public Nullable<DateTime> CheckInDate { get; set; }
        public string CheckInDateView
        {
            get
            {
                return this.CheckInDate.HasValue ? this.CheckInDate.Value.ToStringDateVN() : string.Empty;

            }
        }
        public string CheckInDateTimeView
        {
            get
            {
                return this.CheckInDate.HasValue ? this.CheckInDate.Value.ToStringTimeVN() : string.Empty;

            }
        }

        public Nullable<DateTime> CheckOutDate { get; set; }
        public string CheckOutDateView
        {
            get
            {
                return this.CheckOutDate.HasValue ? this.CheckOutDate.Value.ToStringDateVN() : string.Empty;
            }
        }
        public string CheckOutTimeView
        {
            get
            {
                return this.CheckOutDate.HasValue ? this.CheckOutDate.Value.ToStringTimeVN() : string.Empty;
            }
        }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string CreatedDateView
        {
            get
            {
                return this.CreatedDate.HasValue ? this.CreatedDate.Value.ToStringVN() : string.Empty;
            }
        }
        public Nullable<DateTime> UpdatedDate { get; set; }
        public List<InvoiceDetailRowModel> InvoiceDetails { get; set; }
    }

    public class InvoiceDetailRowModel
    {

        public int Id { get; set; }
        public string Unit { get; set; }
        public int? ServiceId { get; set; }
        public CategoryInvoice CategoryInvoice { get; set; }
        public string CategoryInvoiceView { get {
                return (int)CategoryInvoice>0? CategoryInvoice.GetDisplayName():string.Empty;
            } }
        public int Quantity { get; set; }
        public string Descriptions { get; set; }
        public decimal Price { get; set; }
        public decimal SubAmount { get; set; }
        public string Notes { get; set; }
        public int HotelId { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public string CreatedDateView { get {
                return CreatedDate.HasValue ? CreatedDate.Value.ToStringVN() : string.Empty;
            } }
        public Nullable<DateTime> UpdatedDate { get; set; }

        public string UserUpdate { get; set; }
        public int UserId { get; set; }
    }

    public class InvoiceFilterModel
    {
        public InvoiceFilterModel()
        {
            this.Page = new PagingModel();
        }
        public int InvoiceType { get; set; }
        public int? Source { get; set; }
        public List<int> InvoiceIds { get; set; }
        public int? PaymentMethod { get; set; }
        public int? InvoiceStatus { get; set; }
        public int? CategoryInvoice { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Keyword { get; set; }
        public string Address { get; set; }
        public bool IsShowInDay { get; set; }
        public bool IsExport { get; set; }
        public PagingModel Page { get; set; }
    }

    public class StaticReportModel
    {
        public decimal Room { get; set; }
        public decimal Service { get; set; }
        public decimal Electronic { get; set; }
        public decimal Water { get; set; }
        public decimal Internet { get; set; }
        public decimal Repair { get; set; }
        public decimal Recept { get; set; }
        public decimal Stored { get; set; }
        public decimal Orther { get; set; }
        public decimal Prepay { get; set; }
        public decimal Deductible { get; set; }
        public decimal Surcharge { get; set; }
        public decimal TotalRecept
        {
            get
            {
                return (Room + Service + Surcharge) - Deductible;
            }
        }

        public decimal TotalPayment
        {
            get
            {
                return Electronic + Water + Internet + Repair + Recept + Stored + Orther;
            }
        }

        public decimal TotalAmount
        {
            get
            {
                return TotalRecept - TotalPayment;
            }
        }

    }

    public class RoomPopularReportModel
    {
        public int Count { get; set; }
        public string RoomName { get; set; }
        public int RoomId { get; set; }
    }

    public class ReceiptReportModel
    {
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateView
        {
            get
            {
                return CreatedDate.ToStringVN();
            }
        }
    }

    public class InitFilterModel {
        public string InvoiceStatus { get; set; }
        public string PaymentMethod { get; set; }
    }

    public class SummaryInShift
    {
        public SummaryInShift()
        {
            this.ShiftPrev = new ShiftDTO();
        }
        public decimal InvoiceAmount { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal DeliveryAmount { get; set; }
        public decimal ManagerAmount { get; set; }
        public decimal TotalAmount { get {
                return (InvoiceAmount + ReceiptAmount) - DeliveryAmount;
            } }
        public ShiftDTO ShiftPrev { get; set; }
    }

}
