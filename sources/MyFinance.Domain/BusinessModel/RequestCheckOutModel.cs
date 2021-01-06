using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{
    public class RequestCheckOutModel
    {
        public int hotelId { get; set; }
        public int orderId { get; set; }
        public int? mode { get; set; }
        public bool isByNight { get; set; }

    }
}
