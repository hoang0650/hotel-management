using System;
using System.Collections.Generic;
using MyFinance.Utils;

namespace MyFinance.Domain.BusinessModel
{
    public class ReportRequestModel
    {
        public DateTime? FromDate{get;set;}
        public DateTime? ToDate { get; set; }
        public bool ByRoomType { get; set; }
        public int RoomId { get; set; }
    }
    public class GoodsReceiptModel
    {
        public string ServiceName { get; set; }
        public int NumReceipt { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public class ReportRoomModel
    {
        public ReportRoomModel()
        {
            Histories = new List<ReportRoomHistoryModel>();
        }
        public string RoomName { get; set; }
        public string RoomTypeName { get; set; }
        public string FloorName { get; set; }
        public List<ReportRoomHistoryModel> Histories { get; set; }


    }
    public class ReportRoomHistoryModel
    {
        public string CustomerName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string User { get; set; }
        public decimal TotalAmount { get; set; }
    }
   public class ReportByRoomModel
   {
       public string RoomName { get; set; }
       public string RoomTypeName { get; set; }
       public int RoomTypeId { get; set; }
       public int NumCheckIn { get; set; }
       public int NumCancel { get; set; }
       public decimal TotalAmount { get; set; }

   }

   public class ReportRevenueModel
   {
       public List<InvoiceRowModel> Data { get; set; }
       public decimal TotalAmount { get; set; }

   }
    public class RevenueItemModel
    {
        /// <summary>
        /// Luot phong su dung
        /// </summary>
        public int NumRoomUsed { get; set; }
        /// <summary>
        /// So khach
        /// </summary>
        public int NumCustomer { get; set; }
        /// <summary>
        /// Tien phong
        /// </summary>
        public decimal RoomAmount { get; set; }
        /// <summary>
        /// Tien dich vu
        /// </summary>
        public decimal ServiceAmount { get; set; }
        /// <summary>
        /// Tien mat
        /// </summary>
        public decimal Cashed { get; set; }
        /// <summary>
        /// phu thu
        /// </summary>
        public decimal SurchargeAmount { get; set; }
        /// <summary>
        /// giam tru
        /// </summary>
        public decimal DeductibleAmount { get; set; }
        /// <summary>
        /// Tien tra truoc
        /// </summary>
        public decimal PrepaidAmount { get; set; }
        /// <summary>
        /// Gom nhóm theo ngày
        /// </summary>
        public DateTime CreatedDate { get; set; }
        public string CreatedDateView { get {
                return CreatedDate.ToStringDateVN();
            } }
    }

    public class RevenueModel
    {
        /// <summary>
        /// Doanh thu ngay Hom nay
        /// </summary>
       public RevenueItemModel Today { get; set; }
        /// <summary>
        /// Doanh thu ngay hom qua
        /// </summary>
        public RevenueItemModel Yesterday { get; set; }
        /// <summary>
        /// Doanh thu tuan nay
        /// </summary>
        public RevenueItemModel ThisWeek { get; set; }
        /// <summary>
        /// Doanh thu thang nay
        /// </summary>
        public RevenueItemModel ThisMonth { get; set; }
        public List<RevenueItemModel> ByDate { get; set; }
        public RevenueItemModel Totals { get; set; }
    }
}
