using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Utils;

namespace MyFinance.Domain.BusinessModel
{
    public class HotelModel
    {
        public HotelModel()
        {
            Images = new List<GalleryModel>();
            HotelUtilityIds = new List<int>();
        }
        public int Id { get; set; }
        public int NumFloors { get; set; }
        public int NumRooms { get; set; }
        public int Source { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Logo { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public string Description { get; set; }
        public string Policy { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
        public List<int> HotelUtilityIds { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<GalleryModel> Images { get; set; }
        public decimal FisrtHour { get; set; }
        public DateTime? DateExpired { get; set; }
        public string DateExpiredString { get {
                return this.DateExpired.HasValue ? DateExpired.Value.ToStringVN() : string.Empty;
            } }
        public string CreateDateString
        {
            get
            {
                return this.CreateDate.HasValue ? CreateDate.Value.ToStringVN() : string.Empty;
            }
        }
        public decimal NextHour { get; set; }
        public decimal OverNight { get; set; }
        public double Distance { get; set; }
    }

    public class HotelRegisterModel
    {
        public int Id { get; set; }
        public int NumFloors { get; set; }
        public int NumRooms { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public DateTime? DateExpired { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public bool IsOwner { get; set; }
        public UserViewModel User { get; set; }
    }

    public class HotelRequestModel
    {
        public HotelRequestModel()
        {
            this.Page = new PagingModel();
        }
        public int HotelId { get; set; }

        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public PagingModel Page { get; set; }
    }

   
}
