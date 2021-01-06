using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
   public class Gallery
    {
       [Key]
       public int Id { get; set; }
       public int HotelId { get; set; }
       public int UserId { get; set; }
       public Nullable<int> RoomTypeId { get; set; }
        [MaxLength(200)]
       public string Name { get; set; }
       [MaxLength(500)]
       public string Url { get; set; }
       public System.DateTime CreatedDate { get; set; }

       [ForeignKey("HotelId")]
       public virtual Hotel Hotel { get; set; }
       //[ForeignKey("RoomTypeId")]
       //public virtual RoomClass RoomType { get; set; }
    }
}
