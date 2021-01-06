using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Proxy
{
    public interface IInvoiceProxyService 
    {
        List<InvoiceRowModel> GetInvoices(InvoiceFilterModel filter, out int total);
        int InsertOrUpdateInvoice(InvoiceRowModel data);
        bool UpdateStatusInvocie(int invoiceId, int status);
        bool DeleteInvoice(InvoiceFilterModel model);
        bool AddOrUpdateShift(ShiftDTO dto);
        SummaryInShift SummaryInShift();
    }
   public class InvoiceProxyService : BaseProxyService, IInvoiceProxyService
    {
       public bool DeleteInvoice(InvoiceFilterModel model)
       {
           string url = "api/Invoice/DeleteInvoice";
           return PostStructService<bool>(model, url);
       }

       public bool UpdateStatusInvocie(int invoiceId, int status)
       {
           string url = "api/Invoice/UpdateStatusInvocie";
           return PostStructService<bool>(new { Id = invoiceId, InvoiceStatus = status }, url);
       }
       public int InsertOrUpdateInvoice(InvoiceRowModel model)
       {
           string url = "api/Invoice/InsertOrUpdateInvoice";
           return PostStructService<int>(model, url);
       }
        public List<InvoiceRowModel> GetInvoices(InvoiceFilterModel filter, out int total)
        {
            string url = "api/Invoice/GetInvoices";
            return PostOutTotalService<List<InvoiceRowModel>>(filter, url, out total);
          
        }


        public bool AddOrUpdateShift(ShiftDTO dto)
        {
            string url = "api/Invoice/AddOrUpdateShift";
            return PostStructService<bool>(dto, url);
        }
        public SummaryInShift SummaryInShift()
        {
            string url = "api/Invoice/SummaryInShift";
            return GetDataService<SummaryInShift>( url);
        }
    }
}
