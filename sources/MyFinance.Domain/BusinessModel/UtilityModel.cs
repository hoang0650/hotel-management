using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{
   public class UtilityModel
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        
        public string Name { get; set; }

        public string InputType { get; set; }
        public int UtilityType { get; set; }
        public bool IsDeleted { get; set; }
    }
   public class UtilityGroupModel
   {
       public int Id { get; set; }
       public string Name { get; set; }
       public bool IsDeleted { get; set; }
       public List<UtilityModel> Utilities { get; set; }
       public UtilityGroupModel()
       {
           Utilities = new List<UtilityModel>();
       }
   }
}
