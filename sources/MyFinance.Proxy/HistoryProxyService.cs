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
    public interface IHistoryProxyService
    {
        List<HistoryModel> GetHistories(InvoiceFilterModel filter, out int total);
        bool InsertHistory(HistoryModel model);
        List<HistoryModel> GetHistoriesByInside(InvoiceFilterModel filter, out int total);
    }
    public class HistoryProxyService : BaseProxyService, IHistoryProxyService
    {
        public bool InsertHistory(HistoryModel model)
        {
            string url = "api/History/InsertHistory";
            return PostStructService<bool>(model, url);
        }
        public List<HistoryModel> GetHistories(InvoiceFilterModel filter, out int total)
        {
            string url = "api/History/GetHistories";
            return PostOutTotalService<List<HistoryModel>>(filter, url,out total);
          
        }

        public List<HistoryModel> GetHistoriesByInside(InvoiceFilterModel filter, out int total)
        {
            string url = "api/History/GetHistoriesByInside";
            return PostOutTotalService<List<HistoryModel>>(filter, url, out total);

        }
    }
}
