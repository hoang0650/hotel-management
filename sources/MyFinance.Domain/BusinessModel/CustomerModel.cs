using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFinance.Utils;

namespace MyFinance.Domain.BusinessModel
{
   public class CustomerSearchModel {
       public PagingModel Page { get; set; }
       public int CustomerType { get; set; }
       public string Keyword { get; set; }
       public List<int> OrderIds { get; set; }
       public string PassportId { get; set; }
       public int Id { get; set; }
   }
   public class CustomerRowViewModel
   {
        public int Index { get; set; }
        public int Id { get; set; }
       public int HotelId { get; set; }
       public string PassportId { get; set; }
       public int NumCheckIn { get; set; }
       public decimal TotalAmount { get; set; }
       public string Name { get; set; }
       public string Address { get; set; }
       public string Notes { get; set; }
       public string Mobile { get; set; }
       public string Email { get; set; }
       public List<int> OrderIds { get; set; }
       public DateTime? CreatedDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthDateView
        {
            get
            {
                return this.BirthDate.HasValue ? this.BirthDate.Value.ToStringDateVN() : string.Empty;
            }
        }
        public string CreatedDateView
        {
            get
            {
                return this.CreatedDate.HasValue ? this.CreatedDate.Value.ToStringDateVN() : string.Empty;
            }
        }
        public DateTime? PassportCreatedDate { get; set; }
       public string PassportAgency { get; set; }
       public CustomerType CustomerType { get; set; }
       public string National { get; set; }
   }

   public class CustomerCheckinRowModel
   {
       public int Id { get; set; }
       public string RoomName { get; set; }
       public string PassportId { get; set; }
       public string RoomClassName { get; set; }       
       public string Name { get; set; }
       public string Address { get; set; }
       public string Notes { get; set; }
       public string Mobile { get; set; }
       public string Email { get; set; }
       public List<int> OrderIds { get; set; }
       public DateTime? CheckInDate { get; set; }
        public string CheckInDateView { get {
                return this.CheckInDate.HasValue ? this.CheckInDate.Value.ToStringVN() : string.Empty;
            } }
        public DateTime? CheckOutDate { get; set; }
        public string CheckOutDateView
        {
            get
            {
                return this.CheckOutDate.HasValue ? this.CheckOutDate.Value.ToStringDateVN() : string.Empty;
            }
        }
        public DateTime? BirthDate { get; set; }
        public string BirthDateView
        {
            get
            {
                return this.BirthDate.HasValue ? this.BirthDate.Value.ToStringDateVN() : string.Empty;
            }
        }
        public DateTime? PassportCreatedDate { get; set; }
       public string PassportAgency { get; set; }
       public CustomerType CustomerType { get; set; }
       public string National { get; set; }
   }
}
