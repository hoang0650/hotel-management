using MyFinance.Core;
using MyFinance.Data;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Domain.Enum;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Business
{
    public interface IReportBusiness : IBusinessBase
    {
        List<ReportByRoomModel> ReportByRoom(DateTime fromDate, DateTime toDate, bool ByRoomType = false);
        List<ReportByRoomModel> ReportByService(DateTime fromDate, DateTime toDate);
        List<GoodsReceiptModel> ReportGoodsReceipt(DateTime fromDate, DateTime toDate);
        ReportRoomModel ReportRoomHistory(DateTime fromDate, DateTime toDate, int roomId);
        ReportRevenueModel ReportRevenue(InvoiceFilterModel filter, out int total);
        List<ShiftDTO> ShiftHistory(InvoiceFilterModel model, out int total);
        RevenueModel Revenue(InvoiceFilterModel filter);
    }
    public class ReportBusiness : BusinessBase, IReportBusiness
    {
        private readonly MyFinanceContext _context;
        private readonly IUnitOfWork unitOfWork;
        public ReportBusiness(MyFinanceContext context, IUnitOfWork UnitOfWork)
        {
            this._context = context;
            this.unitOfWork = UnitOfWork;
        }


        public ReportRevenueModel ReportRevenue(InvoiceFilterModel filter, out int total)
        {
            filter.Page.currentPage--;
            var result = new ReportRevenueModel();
            List<InvoiceRowModel> data = new List<InvoiceRowModel>();
            int hoteid = WorkContext.BizKasaContext.HotelId;
            filter.FromDate = filter.FromDate.HasValue ? filter.FromDate.Value.ToMinDate() : DateTime.Now.AddMonths(-1);
            filter.ToDate = filter.ToDate.HasValue ? filter.ToDate.Value.ToMaxDate() : DateTime.Now;

            var invoices = from a in _context.Invoices
                           where a.HotelId == hoteid && a.InvoiceType == filter.InvoiceType
                           select new InvoiceRowModel
                           {
                               Address = a.Address,
                               TotalAmount = a.TotalAmount,
                               CompanyName = a.CompanyName,
                               CustomerName = a.CustomerName,
                               RoomClassName = a.RoomClassName,
                               RoomName = a.RoomName,
                               Id = a.Id,
                               Email = a.Email,
                               InvoiceStatus = a.InvoiceStatus,
                               Mobile = a.Mobile,
                               Notes = a.Notes,
                               UserId = a.UserId,
                               UserUpdate = a.UserUpdate,
                               CheckInDate = a.CheckInDate,
                               CheckOutDate = a.CheckOutDate,
                               CreatedDate = a.CreatedDate,
                               PaymentMethod = a.PaymentMethod,
                               InvoiceDetails = (from b in _context.InvoiceDetail
                                                 where b.InvoiceId == a.Id
                                                 select new InvoiceDetailRowModel
                                                 {
                                                     Descriptions = b.Descriptions,
                                                     Quantity = b.Quantity,
                                                     SubAmount = b.SubAmount,
                                                     CreatedDate = b.CreatedDate,
                                                     Notes = b.Notes,
                                                     Price = b.Price,
                                                     Id = b.Id,
                                                     UserUpdate = b.UserUpdate

                                                 }).ToList()
                           };

            if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                invoices = invoices.Where(a => a.CreatedDate >= filter.FromDate.Value && a.CreatedDate <= filter.ToDate.Value);

            if (filter.InvoiceStatus.HasValue)
                invoices = invoices.Where(a => a.InvoiceStatus == filter.InvoiceStatus.Value);

            if (filter.PaymentMethod.HasValue)
                invoices = invoices.Where(a => a.PaymentMethod == filter.PaymentMethod.Value);

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
                invoices = invoices.Where(a => a.CompanyName.Contains(filter.Keyword) || a.CustomerName.Contains(filter.Keyword) || a.Mobile.Contains(filter.Keyword) || a.Address.Contains(filter.Keyword));
            total = invoices.Count();
            result.TotalAmount = total > 0 ? result.TotalAmount = invoices.Sum(a => a.TotalAmount) : 0;
            data = invoices.OrderByDescending(a => a.CreatedDate).Skip(filter.Page.currentPage * filter.Page.pageSize).Take(filter.Page.pageSize).ToList();

            result.Data = data;


            return result;
        }


        public RevenueModel Revenue(InvoiceFilterModel filter)
        {
            var result = new RevenueModel();
            int hoteid = WorkContext.BizKasaContext.HotelId;
            var m_invoiceRepository = unitOfWork.Repository<Invoice>().GetQueryable();
            var m_invoiceDetailRepository = unitOfWork.Repository<InvoiceDetail>().GetQueryable();

            // get data today
            var m_FromDate = DateTime.Now.ToWorkingDate();
            var m_ToDate = DateTime.Now.AddDays(1).ToWorkingDate();
            result.Today = RevenueItem(m_FromDate, m_ToDate);

            // get data yesterday

            m_ToDate = m_FromDate;
            m_FromDate = DateTime.Now.AddDays(-1).ToWorkingDate();
            result.Yesterday = RevenueItem(m_FromDate, m_ToDate);

            // get data this week
            m_FromDate = DateTime.Now.GetFirstDayOfWeek().ToWorkingDate();
            m_ToDate = m_FromDate.AddDays(8).ToWorkingDate();
            result.ThisWeek = RevenueItem(m_FromDate, m_ToDate);
            // get data this month

            m_FromDate = DateTime.Now.GetFirstDayOfMonth().ToWorkingDate();
            m_ToDate = DateTime.Now.GetLastDayOfMonth().AddDays(1).ToWorkingDate();
            result.ThisMonth = RevenueItem(m_FromDate, m_ToDate);

            // get data by date
            if (filter.FromDate.HasValue && filter.ToDate.HasValue)
            {
                m_FromDate = filter.FromDate.Value.ToWorkingDate();
                m_ToDate = filter.ToDate.Value.AddDays(1).ToWorkingDate();
                result.ByDate = RevenueByDate(m_FromDate, m_ToDate);
                if (result.ByDate.Any())
                {
                    var m_total = new RevenueItemModel()
                    {
                        DeductibleAmount = result.ByDate.Sum(a => a.DeductibleAmount),
                        Cashed = result.ByDate.Sum(a => a.Cashed),
                        NumCustomer = result.ByDate.Sum(a => a.NumCustomer),
                        NumRoomUsed = result.ByDate.Sum(a => a.NumRoomUsed),
                        PrepaidAmount = result.ByDate.Sum(a => a.PrepaidAmount),
                        RoomAmount = result.ByDate.Sum(a => a.RoomAmount),
                        ServiceAmount = result.ByDate.Sum(a => a.ServiceAmount),
                        SurchargeAmount = result.ByDate.Sum(a => a.SurchargeAmount)
                    };
                    result.Totals = m_total;
                }
            }

            return result;
        }
        private RevenueItemModel RevenueItem(DateTime fromDate, DateTime todate)
        {
            int hoteid = WorkContext.BizKasaContext.HotelId;
            var m_invoiceRepository = unitOfWork.Repository<Invoice>().GetQueryable();
            var m_invoiceDetailRepository = unitOfWork.Repository<InvoiceDetail>().GetQueryable();

            if (DateTime.Now <= DateTime.Now.ToWorkingDate())
            {
                fromDate = fromDate.AddDays(-1).ToWorkingDate();
                todate = DateTime.Now.ToWorkingDate();
            }
            var m_invoices = m_invoiceRepository.Where(a => a.HotelId == hoteid
                                    && a.CreatedDate >= fromDate
                                    && a.CreatedDate <= todate
                                    && a.InvoiceType == (int)InvoiceType.Receipt
                                    ).ToList();
            List<int> m_invoiceIds = m_invoices.Select(a => a.Id).ToList();

            var m_invoiceDetail = m_invoiceDetailRepository
                .Where(a => m_invoiceIds.Contains(a.InvoiceId)).ToList();

            var m_revenue = new RevenueItemModel()
            {
                NumRoomUsed = m_invoiceIds.Count(),
                RoomAmount = m_invoiceDetail.Where(a => a.ServiceId == 0 || a.ServiceId == null).Sum(a => a.SubAmount),
                ServiceAmount = m_invoiceDetail.Where(a => a.ServiceId > 0).Sum(a => a.SubAmount),
                Cashed = m_invoices.Sum(a => a.Cashed + a.Prepay),
                PrepaidAmount = m_invoices.Sum(a => a.Prepay),
                SurchargeAmount = m_invoices.Sum(a => a.Surcharge),
                DeductibleAmount = m_invoices.Sum(a => a.Deductible),
                NumCustomer = m_invoiceIds.Count()
            };
            return m_revenue;
        }

        private List<RevenueItemModel> RevenueByDate(DateTime fromDate, DateTime todate)
        {
            int hoteid = WorkContext.BizKasaContext.HotelId;
            var m_invoiceRepository = unitOfWork.Repository<Invoice>().GetQueryable();
            var m_invoiceDetailRepository = unitOfWork.Repository<InvoiceDetail>().GetQueryable();
            if (DateTime.Now <= DateTime.Now.ToWorkingDate())
            {
                fromDate = fromDate.AddDays(-1).ToWorkingDate();
                todate = DateTime.Now.ToWorkingDate();
            }
            List<RevenueItemModel> result = new List<RevenueItemModel>();
            do
            {
                DateTime m_todate = fromDate.AddDays(1).ToWorkingDate();
                var m_invoices = m_invoiceRepository.Where(a => a.HotelId == hoteid
                                                       && a.CreatedDate >= fromDate
                                                       && a.CreatedDate <= m_todate
                                                       && a.InvoiceType == (int)InvoiceType.Receipt)
                                                       .ToList();
                if (m_invoices.Any())
                {
                    List<int> m_invoiceIds = m_invoices.Select(a => a.Id).ToList();
                    var m_invoiceDetail = m_invoiceDetailRepository
                    .Where(a => m_invoiceIds.Contains(a.InvoiceId)).ToList();

                    var m_revenue = new RevenueItemModel()
                    {
                        NumRoomUsed = m_invoiceIds.Count(),
                        RoomAmount = m_invoiceDetail.Where(a => a.ServiceId == 0 || a.ServiceId == null).Sum(a => a.SubAmount),
                        ServiceAmount = m_invoiceDetail.Where(a => a.ServiceId > 0).Sum(a => a.SubAmount),
                        Cashed = m_invoices.Sum(a => a.Cashed + a.Prepay),
                        PrepaidAmount = m_invoices.Sum(a => a.Prepay),
                        SurchargeAmount = m_invoices.Sum(a => a.Surcharge),
                        DeductibleAmount = m_invoices.Sum(a => a.Deductible),
                        NumCustomer = m_invoiceIds.Count(),
                        CreatedDate = fromDate
                    };
                    result.Add(m_revenue);
                }
                fromDate = fromDate.AddDays(1);
            } while (fromDate < todate);

            return result;
        }

        public List<ReportByRoomModel> ReportByRoom(DateTime fromDate, DateTime toDate, bool ByRoomType = false)
        {
            fromDate = fromDate.ToMinDate();
            toDate = toDate.ToMaxDate();
            var result = new List<ReportByRoomModel>();

            var rooms = unitOfWork.Repository<Room>().GetMany(a => a.HotelId == WorkContext.BizKasaContext.HotelId && !a.IsDeleted).ToList();
            var roomType = rooms.Select(a => a.RoomClassId).Distinct().ToList();
            if (rooms.Any())
            {
                foreach (var item in rooms)
                {
                    var row = new ReportByRoomModel();
                    var orders = unitOfWork.Repository<Order>().GetMany(a => a.RoomId == item.Id && a.CreatedDate >= fromDate && a.CreatedDate <= toDate).ToList();
                    row.NumCheckIn = orders.Count(a => a.OrderStatus == (int)OrderStatus.Paid);
                    row.TotalAmount = orders.Where(a => a.OrderStatus == (int)OrderStatus.Paid).Sum(a => a.TotalAmount);
                    row.NumCancel = orders.Count(a => a.OrderStatus == (int)OrderStatus.Cancel);
                    row.RoomName = item.Name;
                    row.RoomTypeName = unitOfWork.Repository<RoomClass>().GetById(item.RoomClassId).Name;
                    row.RoomTypeId = item.RoomClassId;
                    result.Add(row);
                }

            }

            if (ByRoomType)
            {
                var data = new List<ReportByRoomModel>();
                foreach (var item in roomType)
                {
                    var DataRow = result.Where(a => a.RoomTypeId == item).ToList();
                    var row = new ReportByRoomModel();
                    row.TotalAmount = DataRow.Sum(a => a.TotalAmount);
                    row.NumCancel = DataRow.Sum(a => a.NumCancel);
                    row.NumCheckIn = DataRow.Sum(a => a.NumCheckIn);
                    row.RoomTypeName = DataRow.FirstOrDefault().RoomTypeName;
                    data.Add(row);

                }
                return data;

            }

            return result;
        }


        public List<ReportByRoomModel> ReportByService(DateTime fromDate, DateTime toDate)
        {
            fromDate = fromDate.ToMinDate();
            toDate = toDate.ToMaxDate();
            var result = new List<ReportByRoomModel>();

            var services = unitOfWork.Repository<Widget>().GetMany(a => a.HotelId == WorkContext.BizKasaContext.HotelId && !a.IsDeteled).ToList();

            if (services.Any())
            {
                // get order from Invoice
                var m_orderIds = unitOfWork.Repository<Invoice>().GetQueryable()
                    .Where(a => a.HotelId == WorkContext.BizKasaContext.HotelId
                                && a.InvoiceStatus == (int)InvoiceStatus.Completed
                                && a.CreatedDate >= fromDate && a.CreatedDate <= toDate)
                    .Select(a => a.OrderId.Value)
                    .ToList();

                // get service from order
                var m_services = unitOfWork.Repository<OrderService>().GetQueryable()
                    .Where(a => m_orderIds.Contains(a.OrderId))
                    .ToList();


                //var query = _context.Orders.Join(_context.OrderService,
                //                                           order => order.Id,
                //                                           service => service.OrderId,
                //                                           (order, service) => new
                //                                           {
                //                                               TotalAmount = service.Price*service.Quantity,
                //                                               OrderStatus = order.OrderStatus,
                //                                               HotelId = order.HotelId,
                //                                               CreatedDate = order.CreatedDate,
                //                                               ServiceId = service.ServiceId,

                //                                           });
                foreach (var item in services)
                {
                    var row = new ReportByRoomModel();

                    //var data = query.Where(a => a.HotelId == WorkContext.BizKasaContext.HotelId && a.CreatedDate >= fromDate && a.CreatedDate <= toDate && a.ServiceId == item.Id);

                    row.TotalAmount = m_services.Where(a => a.ServiceId == item.Id).Sum(a => a.Quantity * a.Price);// data.Where(a => a.OrderStatus == (int)OrderStatus.Paid).Sum(l => (decimal?)l.TotalAmount) ?? 0;
                    row.RoomName = item.Name;
                    row.NumCheckIn = m_services.Where(a => a.ServiceId == item.Id).Count();// data.Count(a => a.OrderStatus == (int)OrderStatus.Paid);
                                                                                           // row.NumCancel = data.Count(a => a.OrderStatus == (int)OrderStatus.Cancel);                   

                    result.Add(row);
                }

            }


            return result;
        }


        public List<GoodsReceiptModel> ReportGoodsReceipt(DateTime fromDate, DateTime toDate)
        {
            fromDate = fromDate.ToMinDate();
            toDate = toDate.ToMaxDate();
            var result = new List<GoodsReceiptModel>();

            var services = unitOfWork.Repository<Widget>().GetMany(a => a.HotelId == WorkContext.BizKasaContext.HotelId && !a.IsDeteled).ToList();

            if (services.Any())
            {
                var query = _context.InvoiceDetail.Join(_context.Widgets,
                                                           order => order.ServiceId,
                                                           service => service.Id,
                                                           (order, service) => new
                                                           {
                                                               TotalAmount = order.Price * order.Quantity,
                                                               HotelId = order.HotelId,
                                                               CreatedDate = order.CreatedDate,
                                                               ServiceId = order.ServiceId,
                                                               Quantity = order.Quantity

                                                           });
                foreach (var item in services)
                {
                    var row = new GoodsReceiptModel();
                    var data = query.Where(a => a.HotelId == WorkContext.BizKasaContext.HotelId && a.CreatedDate >= fromDate && a.CreatedDate <= toDate && a.ServiceId == item.Id);
                    row.TotalAmount = data.Sum(l => (decimal?)l.TotalAmount) ?? 0;
                    row.ServiceName = item.Name;
                    row.NumReceipt = data.Count();
                    row.Quantity = data.Sum(l => (int?)l.Quantity) ?? 0;
                    result.Add(row);
                }

            }


            return result;
        }


        public ReportRoomModel ReportRoomHistory(DateTime fromDate, DateTime toDate, int roomId)
        {
            fromDate = fromDate.ToMinDate();
            toDate = toDate.ToMaxDate();
            var result = new ReportRoomModel();

            var room = unitOfWork.Repository<Room>().GetById(roomId);

            if (room != null)
            {
                result.RoomName = room.Name;
                result.RoomTypeName = unitOfWork.Repository<Floor>().GetById(room.FloorId).Name;
                result.RoomTypeName = unitOfWork.Repository<RoomClass>().GetById(room.RoomClassId).Name;
                var histories = unitOfWork.Repository<Order>().GetMany(a => a.HotelId == WorkContext.BizKasaContext.HotelId && a.CreatedDate >= fromDate && a.CreatedDate <= toDate && a.RoomId == roomId).Select(b => new ReportRoomHistoryModel
                {
                    CustomerName = b.CustomerName,
                    CheckInDate = b.CheckInDate.Value,
                    CheckOutDate = b.CheckOutDate.Value,
                    TotalAmount = b.TotalAmount
                }).ToList();
                result.Histories = histories;


            }


            return result;
        }


        public List<ShiftDTO> ShiftHistory(InvoiceFilterModel model, out int total)
        {
            try
            {
                model.Page.currentPage--;
                model.FromDate = model.FromDate.HasValue ? model.FromDate.Value.ToMinDate() : DateTime.Now.AddDays(-15);
                model.ToDate = model.ToDate.HasValue ? model.ToDate.Value.ToMaxDate() : DateTime.Now;
                var result = new List<ShiftDTO>();

                var m_shiftReposiotry = unitOfWork.Repository<Shift>().GetQueryable();
                //a.CreatedDate >= model.FromDate&& a.CreatedDate <= model.ToDate
                m_shiftReposiotry = m_shiftReposiotry.Where(a => a.HotelId == WorkContext.BizKasaContext.HotelId);

                result = m_shiftReposiotry
                   .OrderByDescending(a => a.CreatedDate)
                   .Skip(model.Page.currentPage * model.Page.pageSize)
                   .Take(model.Page.pageSize)
                   .Select(a => new ShiftDTO()
                   {
                       CloseAmount = a.CloseAmount,
                       CreatedDate = a.CreatedDate,
                       DeliveryAmount = a.DeliveryAmount,
                       DeliveryManagerAmount = a.DeliveryManagerAmount,
                       ReceiptAmount = a.ReceiptAmount,
                       OpenAmount = a.OpenAmount,
                       EndTime = a.EndTime,
                       StartTime = a.StartTime,
                       Notes = a.Notes,
                       Id = a.Id,
                       UserId = a.UserId,
                       Email = a.User.Email

                   })
                   .ToList();
                total = m_shiftReposiotry.Count();

                return result;
            }
            catch (Exception ex)
            {
                total = 0;
                base.AddError(ex.Message);
                return null;

            }

        }
       
    }
}
