using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.Entities
{
   public class Token
    {
       [Key] 
       public int TokenId { get; set; }
        public int UserId { get; set; }
        public int HotelId { get; set; }
       [MaxLength(100)]   
       public string StoreName { get; set; }
         [MaxLength(100)]  
       public string AuthToken { get; set; }
        public System.DateTime IssuedOn { get; set; }
        public System.DateTime ExpiresOn { get; set; }
    }
}
