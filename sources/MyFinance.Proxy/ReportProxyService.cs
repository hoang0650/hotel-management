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
    public interface IReportProxyService : IBusinessBase
    {
        StaticReportModel GetStaticReport(DateTime? FromDate, DateTime? ToDate);
        List<RoomPopularReportModel> GetRoomPopularReport();
        List<ReceiptReportModel> GetReceiptReport(DateTime? FromDate, DateTime? ToDate);
        ReportRevenueModel ReportRevenue(InvoiceFilterModel filter, out int total);
        List<ReportByRoomModel> ReportByRoom(DateTime fromDate, DateTime toDate, bool ByRoomType = false);
        List<GoodsReceiptModel> ReportGoodsReceipt(DateTime fromDate, DateTime toDate);
        List<ReportByRoomModel> ReportByService(DateTime fromDate, DateTime toDate);
        ReportRoomModel ReportRoomHistory(DateTime fromDate, DateTime toDate, int roomId);
        RevenueModel Revenue(InvoiceFilterModel filter);
        List<ShiftDTO> ShiftHistory(InvoiceFilterModel filter, out int total);
    }
    public class ReportProxyService : BaseProxyService, IReportProxyService
    {
        public ReportRoomModel ReportRoomHistory(DateTime fromDate, DateTime toDate, int roomId)
        {
            try
            {
                string url = "api/Report/ReportRoomHistory";
                var result = PostService<ReportRoomModel>(new { FromDate = fromDate, ToDate = toDate, RoomId = roomId }, url);               
                return result;
            }
            catch (Exception)
            {

                this.AddError("Lỗi khi lấy dữ liệu !");
                return null;
            }
        }
        public List<ShiftDTO> ShiftHistory(InvoiceFilterModel filter, out int total)
        {
            string url = "api/Report/ShiftHistory";
            return PostOutTotalService<List<ShiftDTO>>(filter, url, out total);

        }
        
        public RevenueModel Revenue(InvoiceFilterModel filter)
        {
            string url = "api/Report/Revenue";
            return PostService<RevenueModel>(filter, url);

        }
        public List<ReportByRoomModel> ReportByService(DateTime fromDate, DateTime toDate)
        {
            string url = "api/Report/ReportByService";
            return PostService<List<ReportByRoomModel>>(new { FromDate = fromDate, ToDate = toDate }, url);

        }
        public List<GoodsReceiptModel> ReportGoodsReceipt(DateTime fromDate, DateTime toDate)
        {
            string url = "api/Report/ReportGoodsReceipt";
            return PostService<List<GoodsReceiptModel>>(new { FromDate = fromDate, ToDate = toDate }, url);
        }
        public List<ReportByRoomModel> ReportByRoom(DateTime fromDate, DateTime toDate, bool ByRoomType = false)
        {
            string url = "api/Report/ReportByRoom";
            return PostService<List<ReportByRoomModel>>(new { FromDate = fromDate, ToDate = toDate, ByRoomType = ByRoomType }, url);
          
        }
        public ReportRevenueModel ReportRevenue(InvoiceFilterModel filter, out int total)
        {
            string url = "api/Report/ReportRevenue";
            return PostOutTotalService<ReportRevenueModel>(filter, url,out total);

          
        }
        public List<ReceiptReportModel> GetReceiptReport(DateTime? FromDate, DateTime? ToDate)
        {
            string url = "api/Report/GetReceiptReport";
            return PostService<List<ReceiptReportModel>>(new { FromDate = FromDate, ToDate = ToDate }, url);

        }
        public List<RoomPopularReportModel> GetRoomPopularReport()
        {
            string url = "api/Report/GetRoomPopularReport";
            return GetDataService<List<RoomPopularReportModel>>(url);

          
        }
        public StaticReportModel GetStaticReport(DateTime? FromDate, DateTime? ToDate)
        {

            string url = "api/Report/GetStaticReport";
            return PostService<StaticReportModel>(new { FromDate = FromDate, ToDate = ToDate }, url);

        }
    }
}
