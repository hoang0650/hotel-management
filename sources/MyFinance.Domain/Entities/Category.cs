using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MyFinance.Domain.Enum;
using MyFinance.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFinance.Domain
{
    public class Category 
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set;}
       [MaxLength(100)]
        public string Description { get; set; }
       public int HotelId { get; set; }


       public int CategoryType { get; set; }
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
    }
}