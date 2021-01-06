using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{
   public class UserLoginViewModel
    {

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string HotelName { get; set; }
        public string Logo { get; set; }
        public int UserType { get; set; }
        public int Id { get; set; }
        public int ShiftId { get; set; }
        public int HotelId { get; set; }
        public IList<OwnerHotelDTO> OwnerHotels { get; set; }
        public bool IsOwner { get; set; }

        public bool IsInShift { get; set; }
        public string Email { get; set; }

        public TokenModel Token { get; set; }

        public bool IsApproved { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime? DateLastLogin { get; set; }

        public DateTime? DateLastActivity { get; set; }

        public DateTime DateLastPasswordChange { get; set; }
    }


    public class RequestLogin
    {
public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsShift { get; set; }
    }
}
