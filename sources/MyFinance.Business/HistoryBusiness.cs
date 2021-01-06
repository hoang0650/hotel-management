using MyFinance.Core;
using MyFinance.Data;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFinance.Business
{
    public interface IHistoryBusiness:IBusinessBase
    {
        bool InsertHistory(HistoryModel model);
        List<HistoryModel> GetHistories(InvoiceFilterModel filter, out int total);
        #region Inside
        List<HistoryModel> GetHistoriesByInside(InvoiceFilterModel filter, out int total);
        #endregion
        
    }
    public class HistoryBusiness : BusinessBase, IHistoryBusiness
    {
        private readonly MyFinanceContext _context;

        private readonly IUnitOfWork unitOfWork;
        public HistoryBusiness(
             IUnitOfWork unitOfWork
             , MyFinanceContext context)
        {
            this.unitOfWork = unitOfWork;
            _context = context;
        }
        public bool InsertHistory(HistoryModel model)
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var row = new History() {
                Content=model.Content,
                CreatedDate=DateTime.Now,
                HotelId=hotelId,
                UserId=WorkContext.BizKasaContext.UserId
            };
            unitOfWork.Repository<History>().Add(row);
            unitOfWork.Commit();
            return !this.HasError;
        }

        public List<HistoryModel> GetHistories(InvoiceFilterModel filter, out int total)
        {
            filter.Page.currentPage--;
            List<HistoryModel> result = new List<HistoryModel>();
            int hoteid = WorkContext.BizKasaContext.HotelId;
            filter.FromDate = filter.FromDate.HasValue ? filter.FromDate.Value.ToMinDate() : filter.FromDate;
            filter.ToDate = filter.ToDate.HasValue ? filter.ToDate.Value.ToMaxDate() : filter.ToDate;

            var invoices = from a in _context.Histories
                           where a.HotelId == hoteid
                           select new HistoryModel
                           {
                              Content=a.Content,
                              CreatedDate=a.CreatedDate,
                              Id=a.Id,
                              UserId=a.UserId,
                              UserName=_context.Users.Where(b=>b.Id==a.UserId).Select(v=>v.Email).FirstOrDefault()
                           };

            if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                invoices = invoices.Where(a => a.CreatedDate >= filter.FromDate.Value && a.CreatedDate <= filter.ToDate.Value);
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
                invoices = invoices.Where(a => a.Content.Contains(filter.Keyword));
            total = invoices.Count();
            result = invoices.OrderByDescending(a => a.CreatedDate).Skip(filter.Page.currentPage * filter.Page.pageSize).Take(filter.Page.pageSize).ToList();



            return result;
        }

        #region Inside
        public List<HistoryModel> GetHistoriesByInside(InvoiceFilterModel filter, out int total)
        {
            filter.Page.currentPage--;
            List<HistoryModel> result = new List<HistoryModel>();
            filter.FromDate = filter.FromDate.HasValue ? filter.FromDate.Value.ToMinDate() : filter.FromDate;
            filter.ToDate = filter.ToDate.HasValue ? filter.ToDate.Value.ToMaxDate() : filter.ToDate;

            var invoices = from a in _context.Histories
                           select new HistoryModel
                           {
                               HotelName=_context.Hotels.Where(c=>c.Id==a.HotelId).FirstOrDefault().Name,
                               Content = a.Content,
                               CreatedDate = a.CreatedDate,
                               Id = a.Id,
                               UserId = a.UserId,
                               UserName = _context.Users.Where(b => b.Id == a.UserId).Select(v => v.Email).FirstOrDefault()
                           };

            if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                invoices = invoices.Where(a => a.CreatedDate >= filter.FromDate.Value && a.CreatedDate <= filter.ToDate.Value);
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
                invoices = invoices.Where(a => a.Content.Contains(filter.Keyword));
            total = invoices.Count();
            result = invoices.OrderByDescending(a => a.CreatedDate).Skip(filter.Page.currentPage * filter.Page.pageSize).Take(filter.Page.pageSize).ToList();



            return result;
        }
        #endregion
      
    }
}
