using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
   public class History
    {
       [Key]
       public int Id { get; set; }
       public int UserId { get; set; }
       public DateTime CreatedDate { get; set; }
       [MaxLength(300)]
       public string Content { get; set; }

       public int HotelId { get; set; }
       [ForeignKey("HotelId")]
       public virtual Hotel Hotel { get; set; }
    }
}
