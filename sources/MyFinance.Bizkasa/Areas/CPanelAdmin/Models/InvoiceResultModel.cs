using MyFinance.Domain.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFinance.Bizkasa.Areas.CPanelAdmin.Models
{
    public class InvoiceResultModel
    {
        public List<InvoiceRowModel> Data { get; set; }
        public int Total { get; set; }
    }
}