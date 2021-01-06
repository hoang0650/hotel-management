using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{
   public class CameraDTO
    {
      public CameraDTO()
        {
            LinkTargets = new List<string>();
        }
        public int Id { get; set; }
        
        public string Link { get; set; }
        public List<string> LinkTargets { get; set; }
        public string Username { get; set; }
       
        public string Password { get; set; }
        public int NumCamera { get; set; }
        public bool IsDefault { get; set; }
        public int CameraDefault { get; set; }
        public int CameraCurrentView { get; set; }
        public List<int> CameraNos { get; set; }
        public int HotelId { get; set; }
       
    }
}
