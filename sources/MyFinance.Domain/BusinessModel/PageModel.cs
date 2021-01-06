using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Domain.BusinessModel
{
    public class PagingModel
    {
        public PagingModel()
        {
            this.currentPage = 1;
            this.pageSize = 10;
        }
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public int total { get; set; }
    }
}
