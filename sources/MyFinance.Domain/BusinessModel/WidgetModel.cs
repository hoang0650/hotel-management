using MyFinance.Domain.Entities;
using MyFinance.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{

    public class WidgetRequestModel
    {
        public int Id { get; set; }
        public List<int> Ids { get; set; }
    }
    public class WidgetRowModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }

        public string Name { get; set; }
        public bool IsRecept { get; set; }
        public decimal Price { get; set; }
        public decimal PricePaid { get; set; }

        public int NumImport { get; set; }
        public int NumExport { get; set; }
        public int Residual { get; set; }
        public string Note { get; set; }
    
       
    }
    public class WidgetRowResultModel
    {
        public int HotelId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public List<WidgetRowModel> Widgets { get; set; }
       


    }

    public class WidgetGroupRowModel
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
      

    }

    
}
