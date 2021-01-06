
using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Extention;
using MyFinance.Proxy;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Bizkasa.Service
{
   public partial class TikasaService:ITikasaService
    {
       public Response<StoreTokenModel> GetStoreToken(StoreTokenModel Model)
       {
           StoreTokenModel result = null;
           BusinessProcess.Current.Process(p =>
           {
               result = IoC.Get<ITokenProxyService>().GetStoreToken(Model);//IoC.Get<IHistoryBusiness>().InsertHistory(model);
           });

           return BusinessProcess.Current.ToResponse(result);

       }
       public Response<bool> AddStoreToken(StoreTokenModel Model)
       {
           bool result = false;
           BusinessProcess.Current.Process(p =>
           {
               result = IoC.Get<ITokenProxyService>().AddStoreToken(Model);//IoC.Get<IHistoryBusiness>().InsertHistory(model);
           });

           return BusinessProcess.Current.ToResponse(result);

       }

       public Response<string> CheckStoreToken(string StoreName)
       {
           string result = string.Empty;
           BusinessProcess.Current.Process(p =>
           {
               result = IoC.Get<ITokenProxyService>().CheckStoreToken(StoreName);//IoC.Get<IHistoryBusiness>().InsertHistory(model);
           });

           return BusinessProcess.Current.ToResponse(result);

       }

       public Response<bool> CheckValidToken(StoreTokenModel Model)
       {
           bool result = false;
           BusinessProcess.Current.Process(p =>
           {
               result = IoC.Get<ITokenProxyService>().CheckValidToken(Model);//IoC.Get<IHistoryBusiness>().InsertHistory(model);
           });

           return BusinessProcess.Current.ToResponse(result);

       }
       public Response<bool> InsertHistory(HistoryModel model)
       {
           bool result = false;
           BusinessProcess.Current.Process(p =>
           {
               result = IoC.Get<IHistoryProxyService>().InsertHistory(model);//IoC.Get<IHistoryBusiness>().InsertHistory(model);
           });

           return BusinessProcess.Current.ToResponse(result);

       }

       public Response<DataPaging<List<HistoryModel>>> GetHistories(InvoiceFilterModel filter)
       {
           List<HistoryModel> result = null;
           int total = 0;
           BusinessProcess.Current.Process(p =>
           {
               result = IoC.Get<IHistoryProxyService>().GetHistories(filter, out total);//IoC.Get<IHistoryBusiness>().GetHistories(filter, out total);
           });

           return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
       }
       #region Inside
       public Response<DataPaging<List<HistoryModel>>> GetHistoriesByInside(InvoiceFilterModel filter)
       {
           List<HistoryModel> result = null;
           int total = 0;
           BusinessProcess.Current.Process(p =>
           {
               result = IoC.Get<IHistoryProxyService>().GetHistoriesByInside(filter, out total);
           });

           return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
       }
       #endregion
       
    }
}
