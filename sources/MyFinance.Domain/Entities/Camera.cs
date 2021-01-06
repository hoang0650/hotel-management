using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
   public class Camera
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Link { get; set; }
        [MaxLength(100)]
        public string Username { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        public int NumCamera{ get; set; }
        public int CameraDefault { get; set; }
        public bool IsDefault { get; set; }
        public int HotelId{ get; set; }
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }


    }
}
