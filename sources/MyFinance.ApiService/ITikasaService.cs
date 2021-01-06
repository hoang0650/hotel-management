using MyFinance.Domain.BusinessModel;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ApiService
{
    [ServiceContract]
    public interface ITikasaService : IOrderService,
        IRoomService, IWidgetService, IUserService, IInvoiceService,
        ICustomerServices, IHotelservice, ISystemConfigService,IGalleryService
        ,IReportService,ITokenService
    {
        [OperationContract]
        Response<bool> InsertHistory(HistoryModel model);
        Response<DataPaging<List<HistoryModel>>> GetHistories(InvoiceFilterModel filter);
        Response<DataPaging<List<HistoryModel>>> GetHistoriesByInside(InvoiceFilterModel filter);
    }
}
