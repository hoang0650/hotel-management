using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MyFinance.Domain.Enum
{
    public enum HotelEnum
    {
        [Description("Kích hoạt")]
        Active = 1,
        [Description("Chưa kích hoạt")]
        InActive = 2,

    }

    public enum HotelSource
    {
        [Display(Name = "người dùng đăng ký")]
        Register = 1,
        [Description("thu thập")]
        OutSource = 2,

    }

    public enum SearchRoomBy
    {
        [Description("Theo ngày")]
        ByDay = 1,
        [Description("Theo giờ")]
        ByHour = 2
    }

    public enum RoomStatus
    {

        [Display(Name = "Đang sử dụng")]
        Active = 1,
        [Display(Name = "Đang trống")]
        InActive = 2,
        [Display(Name = "Chưa dọn dẹp")]
        Refresh = 3,
        [Display(Name = "Đã đặt trước")]
        Booking = 4,
        [Display(Name = "Đang sửa chữa")]
        Repair = 5,
    }



    public enum OrderStatus
    {
        [Display(Name ="Chưa thanh toán")]
        NotPaid = 1,
        [Display(Name = "Công nợ")]
        Paid = 2,
        [Display(Name = "Chưa Checkin")]
        UnCheckIn = 3,
        [Display(Name = "Đã Checkin")]
        CheckIn = 4,
        [Display(Name = "Đã đặt trước")]
        Booking = 5,
        [Display(Name = "Hủy")]
        Cancel = 6,
        [Display(Name = "Đã thanh toán")]
        Completed = 7,
    }

    public enum CategoryEnum : int
    {
        [Description("Loại phòng")]
        Room = 1,
        [Description("Dịch vụ")]
        Services = 2
    }

    public enum CaculatorMode
    {
        [Display(Name = "Theo Ngày")]
        ByDay = 1,
        [Display(Name = "Qua đêm")]
        ByNight = 2,
        [Display(Name = "Theo giờ")]
        ByHour = 3
    }

    public enum CustomerType : int
    {
        [Description("Khách lẻ")]
        Customer = 1,
        [Description("Công ty")]
        Company = 2
    }

    public enum InvoiceType : int
    {
        [Description("Phiếu thu")]
        Receipt = 1,
        [Description("Phiếu chi")]
        Payment = 2
    }
    
    public enum CategoryInvoice : int
    {

        [Display(Name = "Tiền phòng")]
        Room = 1,
        [Display(Name = "Tiền dịch vụ")]
        Service = 2,
        [Display(Name = "Tiền điện")]
        Electronic = 3,
        [Display(Name = "Tiền nước")]
        Water = 4,
        [Display(Name = "Tiền internet")]
        Internet = 5,
        [Display(Name = "Tiền sửa chữa")]
        Repair = 6,
        [Display(Name = "Tiền tiếp khách")]
        Recept = 7,
        [Display(Name = "Tiền nhập kho")]
        Stored = 8,
        [Display(Name = "Tiền chi khác")]
        Orther = 9,
        [Display(Name = "Phụ thu")]
        Surcharge = 10,
        [Display(Name = "Giảm trừ")]
        Deductible = 11,
        [Display(Name = "Trả trước")]
        Prepaid = 12,
        [Display(Name = "Công An")]
        Police = 13,
        [Display(Name = "Đi chợ")]
        Market = 14,
        [Display(Name = "Tiền nhà")]
        House = 15,
        [Display(Name = "Tiền lương")]
        Salary = 16,
        [Display(Name = "Vật dụng")]
        VatDung = 17,
        [Display(Name = "Trái cây")]
        TraiCay = 18,


    }

    public enum CategoryInvoicePayment : int
    {

      
        [Display(Name = "Tiền điện")]
        Electronic = 3,
        [Display(Name = "Tiền nước")]
        Water = 4,      
        [Display(Name = "Tiền tiếp khách")]
        Recept = 7,      
        [Display(Name = "Tiền chi khác")]
        Orther = 9,      
        [Display(Name = "Công An")]
        Police = 13,
        [Display(Name = "Đi chợ")]
        Market = 14,
        [Display(Name = "Tiền nhà")]
        House = 15,
        [Display(Name = "Tiền lương")]
        Salary = 16,
        [Display(Name = "Vật dụng")]
        VatDung = 17,
        [Display(Name = "Trái cây")]
        TraiCay = 18,

    }

    public enum Additional
    {
        [Description("Phụ thu quá giờ Checkout(Theo ngày)")]
        CheckoutByDay = 1,
        [Description("Phụ thu quá giờ Checkout(Qua đêm)")]
        CheckoutBynight = 2,
        [Description("Phụ thu Checkin sớm(Theo ngày)")]
        CheckinByDay = 3,
        [Description("Phụ thu Checkin sớm(Qua đêm)")]
        CheckinBynight = 4,
        [Description("Phụ thu quá khách")]
        CustomerOver = 5,
        [Description("Giá bán theo giờ")]
        PriceByHour = 6,
        [Display(Name = "Cấu hình theo ngày")]
        PriceByDay = 7

    }
    public enum UtilitiesType
    {
        [Description("Tiện ích phòng")]
        Room = 1,
        [Description("Tiện ích khách sạn")]
        Hotel = 2,


    }
    public enum InvoiceStatus
    {
        [Display(Name = "Chưa thanh toán")]
        NotPaid = 1,
        [Display(Name = "Công nợ")]
        Paid = 2,
        [Display(Name = "Đã thanh toán")]
        Completed = 7,
    }
    public enum PaymentMethod :int
    {

        [Display(Name = "Tiền mặt")]
        COD = 0,
        [Display(Name = "Chuyển khoản")]
        POD = 1,
        [Display(Name = "Khác")]
        Other = 2,
        [Display(Name = "Thanh toán online")]
        Online = 3,
       
    }

    public enum ConfigType : int
    {

        [Display(Name = "Cấu hình theo thời điểm")]
        ByTime = 0,
        [Display(Name = "Cấu hình mẫu")]
        ByTemplate = 1,
        [Display(Name = "Cấu hình mặc định")]
        ByDefault = 2,

    }

    public enum UserType : int
    {

        [Display(Name = "Quản lý")]
        Admin = 1,
        [Display(Name = "Lễ tân")]
        Reception = 2,
        [Display(Name = "Buồng phòng")]
        Housekeeping = 3,
    }
}
