using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MyFinance.Domain.Entities
{
    public class Hotel
    {
        public int Id { get; set; }
        public int NumFloors { get; set; }

        public int NumRooms { get; set; }
        public int Views { get; set; }
        public int Source { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [MaxLength(100)]
        public string Logo { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Phone { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Website { get; set; }
        [MaxLength(200)]
        public string Note { get; set; }
        [MaxLength(300)]
        public string Address { get; set; }

        public string Description { get; set; }
        public string Policy { get; set; }
        [MaxLength(100)]
        public string Keywords { get; set; }
        [MaxLength(300)]
        public string ImagesUrl { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<DateTime> DateExpired { get; set; }
        public Nullable<DateTime> LastUpdate { get; set; }
        [MaxLength(100)]
        public string UpdateByUser { get; set; }

        public bool IsActive { get; set; }

        public decimal FirstHour { get; set; }
        public decimal NextHour { get; set; }
        public decimal OverNight { get; set; }
        public bool IsDeleted { get; set; }

    }


}
