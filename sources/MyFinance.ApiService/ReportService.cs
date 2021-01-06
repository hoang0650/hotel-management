using MyFinance.Business;
using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ApiService
{
    public interface IReportService {
        Response<List<ReportByRoomModel>> ReportByRoom(DateTime fromDate, DateTime toDate, bool ByRoomType = false);
        Response<List<ReportByRoomModel>> ReportByService(DateTime fromDate, DateTime toDate);
        Response<List<GoodsReceiptModel>> ReportGoodsReceipt(DateTime fromDate, DateTime toDate);
        Response<ReportRoomModel> ReportRoomHistory(DateTime fromDate, DateTime toDate, int roomId);
        Response<DataPaging<ReportRevenueModel>> ReportRevenue(InvoiceFilterModel filter);
        Response<DataPaging<List<ShiftDTO>>> ShiftHistory(InvoiceFilterModel filter);
        Response<RevenueModel> Revenue(InvoiceFilterModel filter);
    }
    public partial class TikasaService
    {
        public Response<RevenueModel> Revenue(InvoiceFilterModel filter)
        {

            RevenueModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IReportBusiness>().Revenue(filter);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<DataPaging<List<ShiftDTO>>> ShiftHistory(InvoiceFilterModel filter)
        {
            List<ShiftDTO> result = null;
            int total = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IReportBusiness>().ShiftHistory(filter, out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
        }
        public Response<DataPaging<ReportRevenueModel>> ReportRevenue(InvoiceFilterModel filter)
        {
            ReportRevenueModel result = null;
            int total = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IReportBusiness>().ReportRevenue(filter, out total);
            });
            return BusinessProcess.Current.ToResponse( DataPaging.Create(result, total));
        }
       

        public Response<ReportRoomModel> ReportRoomHistory(DateTime fromDate, DateTime toDate, int roomId)
        {

            ReportRoomModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IReportBusiness>().ReportRoomHistory(fromDate, toDate,roomId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<GoodsReceiptModel>> ReportGoodsReceipt(DateTime fromDate, DateTime toDate)
        {

            List<GoodsReceiptModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IReportBusiness>().ReportGoodsReceipt(fromDate, toDate);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<ReportByRoomModel>> ReportByService(DateTime fromDate, DateTime toDate)
        {

            List<ReportByRoomModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IReportBusiness>().ReportByService(fromDate, toDate);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<ReportByRoomModel>> ReportByRoom(DateTime fromDate, DateTime toDate, bool ByRoomType=false)
        {

            List<ReportByRoomModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IReportBusiness>().ReportByRoom(fromDate, toDate, ByRoomType);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
