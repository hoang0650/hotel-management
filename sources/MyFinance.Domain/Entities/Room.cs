using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
   public class RoomClass
    {
       [Key]
       public int Id { get; set; }
        [MaxLength(100)] 
       public string Name { get; set; }
        
        public int HotelId { get; set; }
        public int NumBed { get; set; }
        public int NumCustomer { get; set; }
      
        public bool IsDeleted { get; set; }
       [ForeignKey("HotelId")] 
       public virtual Hotel Hotel { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<ConfigPrice> ConfigPrices { get; set; }


    }

   public class RoomAttribute
   {
       [Key]
       public int Id { get; set; }
       public int ConfigPriceId { get; set; }
       public int Key { get; set; }
       public decimal Value { get; set; }
       public Additional Additional { get; set; }
       [ForeignKey("ConfigPriceId")]
       public virtual ConfigPrice ConfigPrice { get; set; }

   }
   public class ConfigPrice
   {
       [Key]
       public int Id { get; set; }
       public int RoomClassId { get; set; }
        [MaxLength(50)]
       public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal PriceByDay { get; set; }
        public decimal PriceByNight { get; set; }
        public decimal PriceByMonth { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDefault { get; set; }
       public ConfigType ConfigType { get; set; }
       [ForeignKey("RoomClassId")]
       public virtual RoomClass RoomClass { get; set; }
   }
   public class Floor {
       [Key]
       public int Id { get; set; }
       [MaxLength(50)]
       public string Name { get; set; }
       public int HotelId { get; set; }
       public bool IsDeleted { get; set; }

       [ForeignKey("HotelId")]
       public virtual Hotel Hotel { get; set; }
   }

   public class Room
   {
       [Key]
       public int Id { get; set; }
       [MaxLength(50)]
       public string Name { get; set; }
       public RoomStatus RoomStatus { get; set; }
       public int FloorId { get; set; }
       public int RoomClassId { get; set; }
       public int HotelId { get; set; }
       public bool IsDeleted { get; set; }

       [ForeignKey("HotelId")]
       public virtual Hotel Hotel { get; set; }

       [ForeignKey("FloorId")]
       public virtual Floor Floor { get; set; }

       [ForeignKey("RoomClassId")]
       public virtual RoomClass RoomClass { get; set; }
   }

   public class Utility
   {
       [Key]
       public int Id { get; set; }
       public int GroupId { get; set; }
       [MaxLength(100)]
       public string Name { get; set; }
      
       public string InputType { get; set; }
       public int UtilityType { get; set; }//tien ich loai phong or tien ich khach san
       public bool IsDeleted { get; set; }
      
   }
   public class UtilityGroup
   {
       [Key]
       public int Id { get; set; }
       [MaxLength(50)]
       public string Name { get; set; }
      
       public bool IsDeleted { get; set; }
   }
   public class UtilityMapping
   {
       [Key]
       public int Id { get; set; }
       public int HotelId { get; set; }

       public int RoomTypeId { get; set; }
       public int UtilityId { get; set; }
     

       [ForeignKey("UtilityId")]
       public virtual Utility Utility { get; set; }

       [ForeignKey("HotelId")]
       public virtual Hotel Hotel { get; set; }
       public bool IsDeleted { get; set; }
   }


}
