
using MyFinance.Business;
using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ApiService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public partial class TikasaService:ITikasaService
    {
       public Response<bool> InsertHistory(HistoryModel model)
       {
           bool result = false;
           BusinessProcess.Current.Process(p =>
           {
               result = IoC.Get<IHistoryBusiness>().InsertHistory(model);
           });

           return BusinessProcess.Current.ToResponse(result);

       }

       public Response<DataPaging<List<HistoryModel>>> GetHistories(InvoiceFilterModel filter)
       {
           List<HistoryModel> result = null;
           int total = 0;
           BusinessProcess.Current.Process(p =>
           {
               result = IoC.Get<IHistoryBusiness>().GetHistories(filter, out total);
           });

           return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
       }

       public Response<DataPaging<List<HistoryModel>>> GetHistoriesByInside(InvoiceFilterModel filter)
       {
           List<HistoryModel> result = null;
           int total = 0;
           BusinessProcess.Current.Process(p =>
           {
               result = IoC.Get<IHistoryBusiness>().GetHistoriesByInside(filter, out total);
           });

           return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
       }
    }
}
