using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Utils;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{
   public class TokenModel
    {
        public int TokenId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
        public string AuthToken { get; set; }
        public System.DateTime IssuedOn { get; set; }
        public System.DateTime ExpiresOn { get; set; }
    }

  

    public class StoreTokenModel
    {
        public string StoreName { get; set; }
        public string Code { get; set; }
        public string access_token { get; set; }
        public System.DateTime IssuedOn { get; set; }
        public System.DateTime ExpiresOn { get; set; }
    }

    public class TicketModel
   {
       public int Id { get; set; }
       public string TenKhachHang { get; set; }
      
       public string TenKhachDat { get; set; }
      
       public string CodeVe { get; set; }
      
       public string SoVe { get; set; }
       
       public string ChieuDi { get; set; }
       
       public string ChieuVe { get; set; }
       public decimal TongTien { get; set; }
     
       public string HangBay { get; set; }

       public DateTime NgayBay { get; set; }
       public string NgayBayView { get{
           return NgayBay.ToShortDateString();
       } }
       public DateTime NgayDat { get; set; }

       public string NgayDatView { get {
           return NgayDat.ToShortDateString();
       } }
      
       public decimal TongPhaiTra { get; set; }
       public decimal TongThuKhach { get; set; }
       public decimal ChietKhauCTV { get; set; }
       public decimal PhiDaiLy { get; set; }
       public decimal PhiGioiThieu { get; set; }
       public decimal ChietKhauF1 { get; set; }
   }
}
