
using MyFinance.Core;


using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
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
    public interface IInvoiceService
    {
        Response<int> InsertOrUpdateInvoice(InvoiceRowModel data);
        Response<DataPaging<List<InvoiceRowModel>>> GetInvoices(InvoiceFilterModel filter);
        Response<bool> UpdateStatusInvocie(int invoiceId, int status);
        Response<StaticReportModel> GetStaticReport(DateTime? FromDate, DateTime? ToDate);
        Response<List<RoomPopularReportModel>> GetRoomPopularReport();
        Response<List<ReceiptReportModel>> GetReceiptReport(DateTime? FromDate, DateTime? ToDate);
        Response<bool> DeleteInvoice(InvoiceFilterModel model);
        Response<bool> AddOrUpdateShift(ShiftDTO model);
        Response<SummaryInShift> SummaryInShift();
    }
    public partial class TikasaService
    {
        public Response<SummaryInShift> SummaryInShift()
        {
            SummaryInShift result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceProxyService>().SummaryInShift();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> AddOrUpdateShift(ShiftDTO model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceProxyService>().AddOrUpdateShift(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<bool> DeleteInvoice(InvoiceFilterModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceProxyService>().DeleteInvoice(model);//IoC.Get<IInvoiceBusiness>().UpdateStatusInvocie(invoiceId, status);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response< int> InsertOrUpdateInvoice(InvoiceRowModel data)
        {
            int result = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceProxyService>().InsertOrUpdateInvoice(data);//IoC.Get<IInvoiceBusiness>().InsertOrUpdateInvoice(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> UpdateStatusInvocie(int invoiceId, int status)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceProxyService>().UpdateStatusInvocie(invoiceId, status);//IoC.Get<IInvoiceBusiness>().UpdateStatusInvocie(invoiceId, status);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<DataPaging<List<InvoiceRowModel>>> GetInvoices(InvoiceFilterModel filter)
        {
            List<InvoiceRowModel> result = null;
            int total=0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceProxyService>().GetInvoices(filter, out total);//IoC.Get<IInvoiceBusiness>().GetInvoices(filter, out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result,total));
        }
       
        public Response< StaticReportModel> GetStaticReport(DateTime? FromDate, DateTime? ToDate)
        {
            StaticReportModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IReportProxyService>().GetStaticReport(FromDate, ToDate); //IoC.Get<IInvoiceBusiness>().GetStaticReport(FromDate, ToDate);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<RoomPopularReportModel>> GetRoomPopularReport()
        {
            List<RoomPopularReportModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IReportProxyService>().GetRoomPopularReport();//IoC.Get<IInvoiceBusiness>().GetRoomPopularReport();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<ReceiptReportModel>> GetReceiptReport(DateTime? FromDate, DateTime? ToDate)
        {
            List<ReceiptReportModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IReportProxyService>().GetReceiptReport(FromDate, ToDate);//IoC.Get<IInvoiceBusiness>().GetReceiptReport(FromDate, ToDate);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
