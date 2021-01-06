using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
    public class Widget
    {

        [Key]
        [Required]
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int GroupId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public  bool IsRecept { get; set; }
        public decimal Price { get; set; }
        public decimal PricePaid { get; set; }
        public int NumImport { get; set; }
        public int NumExport { get; set; }
        public int Residual { get; set; }

        public bool IsDeteled { get; set; }
        [MaxLength(200)]
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }

        [ForeignKey("GroupId")]
        public virtual GroupWidget GroupWidget { get; set; }

    }

    public class GroupWidget
    {

        [Key]
        [Required]
        public int Id { get; set; }
        public int HotelId { get; set; }
        public bool IsDeteled { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Note { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }

    }
}
