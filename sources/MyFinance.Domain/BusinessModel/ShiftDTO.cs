using System;
using MyFinance.Utils;

namespace MyFinance.Domain.BusinessModel
{
    public class ShiftDTO
    {
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public string StartTimeView { get {
                return StartTime.ToStringVN();
            } }
        public DateTime? EndTime { get; set; }
        public string EndTimeView { get {
                return EndTime.HasValue ? EndTime.Value.ToStringVN() : string.Empty;
            } }
        public int UserId { get; set; }
       
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal OpenAmount { get; set; }
        public decimal CloseAmount { get; set; }
        public decimal ReceiptAmount { get; set; }
        public decimal DeliveryAmount { get; set; }
        public decimal DeliveryManagerAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Notes { get; set; }
    }

    public class ShiftTransferManagerDTO
    {
        public decimal TransferAmount { get; set; }
        public decimal MaxTransferAmount { get; set; }
        public int ManagerId { get; set; }
        public string ManagerPassword { get; set; }
        public int ShiftId { get; set; }
        public int HotelId { get; set; }
        public int UserId { get; set; }
    }

}
