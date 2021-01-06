using MyFinance.Domain.Enum;
using System;
using MyFinance.Utils;

namespace MyFinance.Domain.BusinessModel
{


    public class UserViewModel
   {
       public string FullName { get; set; }
       public string Phone { get; set; }
       public DateTime CreatedDate { get; set; }
        public string CreatedDateView
        {
            get
            {
                return this.CreatedDate.ToStringVN();
            }
        }
        public string Password { get; set; }
       public string Status { get; set; }
       public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
        public bool IsInShift { get; set; }
        public string Email { get; set; }
       public bool IsOwner { get; set; }
       public UserType UserType { get; set; }
       public int HotelId { get; set; }
       public int Id { get; set; }
       public DateTime? LastLogin { get; set; }
        public string LastLoginView { get {
                return this.LastLogin.HasValue ? this.LastLogin.Value.ToStringVN() : string.Empty;
            } }
    }
}
