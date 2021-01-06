using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace MyFinance.Utils
{
    public static class WorkContext
    {
        public const string SessionBizkasaKey = "Bizkasa";
        public const string CookieBizkasaKey = "BizkasaKey";
        public const string SessionBizkasaInsideKey = "Insidekasa";
        public static void SetInSession(object value)
        {
            HttpContext.Current.Session[SessionBizkasaKey] = value;
        }
        public static T GetSession<T>()
        {
            return (T)HttpContext.Current.Session[SessionBizkasaKey];
        }
   
        public static UserContext BizKasaContext
        {
            get
            {
                if (HttpContext.Current.Session != null)
                    return HttpContext.Current.Session[SessionBizkasaKey] as UserContext;
                else
                    return null;
            }
            set
            {
                if (HttpContext.Current.Session != null)
                    HttpContext.Current.Session[SessionBizkasaKey] = value;
            }
        }

        public static UserContext InsideContext
        {
            get
            {
                if (HttpContext.Current.Session != null)
                    return HttpContext.Current.Session[SessionBizkasaInsideKey] as UserContext;
                else
                    return null;
            }
            set
            {
                if (HttpContext.Current.Session != null)
                    HttpContext.Current.Session[SessionBizkasaInsideKey] = value;
            }
        }
    }
    public class UserContext
    {
        public int UserId { get; set; }

        public int HotelId { get; set; }
        public int ShiftId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string HotelName { get; set; }
        public string Logo { get; set; }
        public bool IsOwner { get; set; }
        public bool IsInShift { get; set; }
        public string TokenId { get; set; }
        public int UserType { get; set; }
        public bool IsLogined { get { return this.UserId > 0; } }
       public IList<OwnerHotelDTO> OwnerHotels { get; set; }
    }

    public class OwnerHotelDTO
    {
        public int HotelId { get; set; }
        public string HotelName { get; set; }
        public bool IsSelected { get; set; }
        public DateTime? DateExpired { get; set; }
    }
}
