using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public int HotelId { get; set; }
        public CategoryEnum CategoryType { get; set; }
        public string Description { get; set; }
    }
}
