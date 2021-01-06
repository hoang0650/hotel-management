using MyFinance.Business;
using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Collections.Generic;

namespace MyFinance.ApiService
{
    public interface IInvoiceService
    {
        Response<int> InsertOrUpdateInvoice(InvoiceRowModel data);
        Response<InvoiceResponse> GetInvoices(InvoiceFilterModel filter);
        Response<bool> UpdateStatusInvocie(int invoiceId, int status);
        Response<StaticReportModel> GetStaticReport(DateTime? FromDate, DateTime? ToDate);
        Response<List<RoomPopularReportModel>> GetRoomPopularReport();
        Response<List<ReceiptReportModel>> GetReceiptReport(DateTime? FromDate, DateTime? ToDate);
        Response<bool> DeleteInvoice(List<int> invoiceIds);
        Response<SummaryInShift> SummaryInShift();
        Response<bool> AddOrUpdateShift(ShiftDTO dto);
        Response<DataPaging<List<InvoiceDetailRowModel>>> GetInvoiceByPayment(InvoiceFilterModel filter);
        Response<InvoiceResult> GetInvoicesExport(InvoiceFilterModel filter);
        Response<bool> TransferToManager(ShiftTransferManagerDTO data);
    }
    public partial class TikasaService
    {
        public Response<bool> TransferToManager(ShiftTransferManagerDTO data)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().TransferToManager(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> DeleteInvoice(List<int> invoiceIds)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().DeleteInvoice(invoiceIds);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response< int> InsertOrUpdateInvoice(InvoiceRowModel data)
        {
            int result = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().InsertOrUpdateInvoice(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> UpdateStatusInvocie(int invoiceId, int status)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().UpdateStatusInvocie(invoiceId, status);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<DataPaging<List<InvoiceDetailRowModel>>> GetInvoiceByPayment(InvoiceFilterModel filter)
        {
            List<InvoiceDetailRowModel> result = null;
            int total = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().GetInvoiceByPayment(filter, out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
        }
        public Response<InvoiceResponse> GetInvoices(InvoiceFilterModel filter)
        {
            InvoiceResponse result = new InvoiceResponse();
            InvoiceResult m_invoices = null;
            int total=0;
            BusinessProcess.Current.Process(p =>
            {
                m_invoices = IoC.Get<IInvoiceBusiness>().GetInvoices(filter, out total);
            });
            result.DataPaging = DataPaging.Create(m_invoices.Data, total);
            result.Summary = m_invoices.Summary;
            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<InvoiceResult> GetInvoicesExport(InvoiceFilterModel filter)
        {
            InvoiceResult result = new InvoiceResult();
            filter.IsExport = true;
            int total = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().GetInvoices(filter, out total);
            });
            return BusinessProcess.Current.ToResponse(result);
        }

        public Response< StaticReportModel> GetStaticReport(DateTime? FromDate, DateTime? ToDate)
        {
            StaticReportModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().GetStaticReport(FromDate, ToDate);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<RoomPopularReportModel>> GetRoomPopularReport()
        {
            List<RoomPopularReportModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().GetRoomPopularReport();
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<List<ReceiptReportModel>> GetReceiptReport(DateTime? FromDate, DateTime? ToDate)
        {
            List<ReceiptReportModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().GetReceiptReport(FromDate, ToDate);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<SummaryInShift>  SummaryInShift()
        {
            SummaryInShift result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().SummaryInShift();
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<bool> AddOrUpdateShift(ShiftDTO dto)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IInvoiceBusiness>().AddOrUpdateShift(dto);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
    }
}
