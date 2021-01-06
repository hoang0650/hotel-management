using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Bizkasa.Service
{
    public interface ITikasaService : IOrderService,
        IRoomService, IWidgetService, IUserService, IInvoiceService,
        ICustomerServices, IHotelservice, ISystemConfigService,IGalleryService
        ,IReportService
    {
        Response<StoreTokenModel> GetStoreToken(StoreTokenModel Model);
        Response<bool> AddStoreToken(StoreTokenModel Model);
        Response<bool> InsertHistory(HistoryModel model);
        Response<DataPaging<List<HistoryModel>>> GetHistories(InvoiceFilterModel filter);
        Response<string> CheckStoreToken(string StoreName);
        Response<bool> CheckValidToken(StoreTokenModel Model);

        #region Inside
        Response<DataPaging<List<HistoryModel>>> GetHistoriesByInside(InvoiceFilterModel filter);
        #endregion
    }
}
