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
using MyFinance.Domain;

namespace MyFinance.Business
{
    public interface IInvoiceBusiness : IBusinessBase
    {
        int InsertOrUpdateInvoice(InvoiceRowModel data);
        InvoiceResult GetInvoices(InvoiceFilterModel filter, out int total);

        bool UpdateStatusInvocie(int invoiceId, int status);
        StaticReportModel GetStaticReport(DateTime? FromDate, DateTime? ToDate);
        List<RoomPopularReportModel> GetRoomPopularReport();
        List<ReceiptReportModel> GetReceiptReport(DateTime? FromDate, DateTime? ToDate);
        bool DeleteInvoice(List<int> invoiceIds);
        SummaryInShift SummaryInShift();
        bool AddOrUpdateShift(ShiftDTO dto);
        List<InvoiceDetailRowModel> GetInvoiceByPayment(InvoiceFilterModel filter, out int total);
        bool TransferToManager(ShiftTransferManagerDTO data);
    }
    public class InvoiceBusiness : BusinessBase, IInvoiceBusiness
    {
        private readonly MyFinanceContext _context;

        private readonly IUnitOfWork unitOfWork;
        public InvoiceBusiness(
             IUnitOfWork unitOfWork
             , MyFinanceContext context)
        {
            this.unitOfWork = unitOfWork;
            _context = context;
        }
        /// <summary>
        /// xoa danh sach hoa don theo id
        /// </summary>
        /// <param name="invoiceIds">danh sach id</param>
        /// <returns>true or false</returns>
        public bool DeleteInvoice(List<int> invoiceIds)
        {
            if (!invoiceIds.Any())
            {
                this.AddError("Chưa chọn hóa đơn cần xóa !");
                return false;
            }
            var repodetail = unitOfWork.Repository<InvoiceDetail>();
            foreach (int item in invoiceIds)
            {
                var row = repodetail.GetById(item);
                repodetail.Delete(row);

            }
            unitOfWork.Commit();
            return !this.HasError;
        }
        public int InsertOrUpdateInvoice(InvoiceRowModel data)
        {
            try
            {
                data.UserId = WorkContext.BizKasaContext.UserId;
                data.UserUpdate = WorkContext.BizKasaContext.UserName;
                data.HotelId = WorkContext.BizKasaContext.HotelId;
                var invoiceRepo = unitOfWork.Repository<Invoice>();
                var invoiceDetailRepo = unitOfWork.Repository<InvoiceDetail>();
                if (data.Id > 0)
                {
                    var invoice = invoiceRepo.GetById(data.Id);
                    invoice.Address = data.Address;
                    invoice.CompanyName = data.CompanyName;
                    invoice.CustomerName = data.CustomerName;
                    invoice.Email = data.Email;
                    invoice.Address = data.Address;
                    invoice.InvoiceStatus = data.InvoiceStatus;
                    invoice.Mobile = data.Mobile;
                    invoice.Notes = data.Notes;
                    invoice.TotalAmount = data.TotalAmount;
                    invoice.UpdatedDate = DateTime.Now;
                    invoice.UserUpdate = data.UserUpdate;
                    invoice.UserId = data.UserId;
                    invoiceRepo.Update(invoice);
                    //if (data.InvoiceDetails.Any())
                    //{

                    //}

                    unitOfWork.Commit();
                    return data.Id;
                }
                else
                {
                    var newInvoice = new Invoice()
                    {
                        Address = data.Address,

                        CompanyName = data.CompanyName,
                        CustomerName = data.CustomerName,
                        Email = data.Email,
                        InvoiceStatus = data.InvoiceStatus,
                        Mobile = data.Mobile,
                        Notes = data.Notes,
                        TotalAmount = data.TotalAmount,
                        CheckInDate = data.CheckInDate,
                        CheckOutDate = data.CheckOutDate,
                        Surcharge = data.Surcharge,
                        Deductible = data.Deductible,
                        Prepay = data.Prepaid,
                        Cashed = data.Cashed,
                        CreatedDate = DateTime.Now,
                        HotelId = data.HotelId,
                        UpdatedDate = DateTime.Now,
                        UserId = data.UserId,
                        UserUpdate = data.UserUpdate,
                        InvoiceType = data.InvoiceType,
                        RoomClassName = data.RoomClassName,
                        RoomName = data.RoomName

                    };
                    newInvoice.OrderId = data.OrderId > 0 ? data.OrderId : newInvoice.OrderId;
                    invoiceRepo.Add(newInvoice);
                    if (data.InvoiceDetails.Any())
                    {
                        foreach (var item in data.InvoiceDetails)
                        {
                            var invoiceDetail = new InvoiceDetail()
                            {
                                Invoice = newInvoice,
                                Notes = item.Notes,
                                Price = item.Price,
                                Quantity = item.Quantity,
                                SubAmount = item.SubAmount,
                                CreatedDate = DateTime.Now,
                                Unit = item.Unit,
                                UpdatedDate = DateTime.Now,
                                UserId = data.UserId,
                                UserUpdate = data.UserUpdate,
                                Descriptions = item.Descriptions,
                                HotelId = data.HotelId,
                                CategoryInvoice = (int)item.CategoryInvoice,
                                ServiceId = item.ServiceId


                            };
                            if (string.IsNullOrWhiteSpace(invoiceDetail.Notes))
                            {
                                invoiceDetail.Notes = "x" + invoiceDetail.Quantity;
                            }
                            invoiceDetailRepo.Add(invoiceDetail);

                            if (item.ServiceId.HasValue)
                            {
                                var widgetRepo = unitOfWork.Repository<Widget>();
                                var widget = widgetRepo.GetById(item.ServiceId.Value);
                                widget.Price = item.Price;
                                if (data.InvoiceType == (int)InvoiceType.Receipt)
                                {

                                    widget.NumExport += item.Quantity;

                                }
                                if (data.InvoiceType == (int)InvoiceType.Payment)
                                {

                                    widget.NumImport += item.Quantity;
                                }
                                widget.Residual = widget.NumImport - widget.NumExport;
                                widgetRepo.Update(widget);

                            }
                        }
                    }

                    unitOfWork.Commit();
                    return newInvoice.Id;

                }
            }
            catch (Exception ex)
            {
                return 0;

            }

        }
        public bool UpdateStatusInvocie(int invoiceId, int status)
        {
            var invoiceRepo = unitOfWork.Repository<Invoice>();
            var invoice = invoiceRepo.GetMany(a => a.HotelId == WorkContext.BizKasaContext.HotelId && a.Id == invoiceId).FirstOrDefault();
            if (status == (int)OrderStatus.Completed)
            {
                var paid = (invoice.TotalAmount + invoice.Surcharge) - (invoice.Cashed + invoice.Prepay + invoice.Deductible);
                invoice.Cashed = paid > 0 ? paid : invoice.Cashed;
            }
            invoice.InvoiceStatus = status;
            invoiceRepo.Update(invoice);
            unitOfWork.Commit();
            return true;
        }


        public InvoiceResult GetInvoices(InvoiceFilterModel filter, out int total)
        {
            InvoiceResult result = new InvoiceResult();
            filter.Page.currentPage--;
            List<InvoiceRowModel> data = new List<InvoiceRowModel>();
            int hoteid = WorkContext.BizKasaContext.HotelId;


            filter.FromDate = filter.FromDate.HasValue
             ? filter.FromDate.Value.ToMinDate()
             : filter.IsShowInDay
             ? DateTime.Now.ToWorkingDate()
             : DateTime.Now.GetFirstDayOfMonth().ToMinDate();

            filter.ToDate = filter.ToDate.HasValue
         ? filter.ToDate.Value.ToMaxDate()
         : filter.IsShowInDay ? DateTime.Now.AddDays(1).ToWorkingDate()
         : DateTime.Now.GetLastDayOfMonth().ToMaxDate();


            if (filter.IsShowInDay && DateTime.Now <= DateTime.Now.ToWorkingDate())
            {
                filter.FromDate = filter.FromDate.Value.AddDays(-1).ToWorkingDate();
                filter.ToDate = DateTime.Now.ToWorkingDate();
            }




            var m_invoiceRepository = unitOfWork.Repository<Invoice>().GetQueryable()
                .Where(a => a.HotelId == hoteid && a.InvoiceType == filter.InvoiceType);

            if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                m_invoiceRepository = m_invoiceRepository.Where(a => a.CreatedDate >= filter.FromDate.Value && a.CreatedDate <= filter.ToDate.Value);

            if (filter.InvoiceStatus.HasValue)
                m_invoiceRepository = m_invoiceRepository.Where(a => a.InvoiceStatus == filter.InvoiceStatus.Value);

            if (filter.PaymentMethod.HasValue)
                m_invoiceRepository = m_invoiceRepository.Where(a => a.PaymentMethod == filter.PaymentMethod.Value);

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
                m_invoiceRepository = m_invoiceRepository.Where(a => a.CompanyName.Contains(filter.Keyword) || a.CustomerName.Contains(filter.Keyword) || a.Mobile.Contains(filter.Keyword) || a.Address.Contains(filter.Keyword));

            total = m_invoiceRepository.Count();
            if (!filter.IsExport)
            {
                m_invoiceRepository = m_invoiceRepository.OrderByDescending(a => a.CreatedDate).Skip(filter.Page.currentPage * filter.Page.pageSize)
                .Take(filter.Page.pageSize);
            }
            data = m_invoiceRepository
                .Select(a => new InvoiceRowModel
                {
                    Address = a.Address,
                    TotalAmount = a.TotalAmount,
                    CompanyName = a.CompanyName,
                    PassportId = a.Order.Customer.PassportId,
                    PassportCreated = a.Order.Customer.PassportCreatedDate,
                    CustomerName = a.CustomerName,
                    RoomClassName = a.RoomClassName,
                    RoomName = a.RoomName,
                    Deductible = a.Deductible,
                    Prepaid = a.Prepay,
                    Surcharge = a.Surcharge,
                    Cashed = a.Cashed,
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
                    InvoiceDetails = a.InvoiceDetails.Select(b => new InvoiceDetailRowModel
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
                }).ToList();


            result.Data = data;
            InvoiceSummary sum = new InvoiceSummary()
            {
                DeductibleAmount = total > 0 ? m_invoiceRepository.Sum(a => a.Deductible) : 0,
                TotalAmount = total > 0 ? m_invoiceRepository.Sum(a => a.Cashed + a.Prepay) : 0,
            };
            result.Summary = sum;
            return result;
        }





        public List<InvoiceDetailRowModel> GetInvoiceByPayment(InvoiceFilterModel filter, out int total)
        {
            filter.Page.currentPage--;
            List<InvoiceRowModel> result = new List<InvoiceRowModel>();
            int hoteid = WorkContext.BizKasaContext.HotelId;
            filter.FromDate = filter.FromDate.HasValue ? filter.FromDate.Value.ToMinDate() : filter.FromDate;
            filter.ToDate = filter.ToDate.HasValue ? filter.ToDate.Value.ToMaxDate() : filter.ToDate;
            var m_invoiceReponsitory = unitOfWork.Repository<Invoice>();
            var m_invoiceDetailReponsitory = unitOfWork.Repository<InvoiceDetail>();
            var invoices = m_invoiceReponsitory.GetQueryable()
                                .Where(a => a.HotelId == hoteid && a.InvoiceType == (int)InvoiceType.Payment);


            if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                invoices = invoices.Where(a => a.CreatedDate >= filter.FromDate.Value && a.CreatedDate <= filter.ToDate.Value);



            if (filter.CategoryInvoice.HasValue)
            {

                invoices = invoices.Where(a => a.InvoiceDetails.Any(b => b.CategoryInvoice == filter.CategoryInvoice.Value));
            }

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                var invoiceIds = m_invoiceDetailReponsitory.GetQueryable().Where(a => a.Descriptions.Contains(filter.Keyword) || a.Notes.Contains(filter.Keyword)).Select(a => a.InvoiceId).ToList();
                invoices = invoices.Where(a => invoiceIds.Contains(a.Id));
            }
            //invoices = invoices.Where(a => a.CompanyName.Contains(filter.Keyword) 
            //|| a.CustomerName.Contains(filter.Keyword) 
            //|| a.Mobile.Contains(filter.Keyword) 
            //|| a.Address.Contains(filter.Keyword) );
            total = invoices.Count();
            result = invoices.OrderByDescending(a => a.CreatedDate).Skip(filter.Page.currentPage * filter.Page.pageSize).Take(filter.Page.pageSize)
                 .Select(a => new InvoiceRowModel()
                 {
                     CreatedDate = a.CreatedDate,
                     InvoiceDetails = a.InvoiceDetails.Where(b => b.InvoiceId == a.Id)
                                                                    .Select(c => new InvoiceDetailRowModel()
                                                                    {
                                                                        Descriptions = string.IsNullOrEmpty(c.Notes) ? c.Descriptions : c.Descriptions + "( " + c.Notes + " )",
                                                                        CategoryInvoice = (CategoryInvoice)c.CategoryInvoice,
                                                                        Quantity = c.Quantity,
                                                                        SubAmount = c.SubAmount,
                                                                        CreatedDate = c.CreatedDate,
                                                                        Notes = c.Notes,
                                                                        Price = c.Price,
                                                                        Id = c.Id,
                                                                        UserUpdate = c.UserUpdate
                                                                    }).ToList()
                 }).ToList();

            return result.SelectMany(a => a.InvoiceDetails).ToList();

        }

        public StaticReportModel GetStaticReport(DateTime? FromDate, DateTime? ToDate)
        {
            FromDate = FromDate.HasValue ? FromDate.Value.ToMinDate() : DateTime.Now.AddDays(-15).ToMinDate();
            ToDate = ToDate.HasValue ? ToDate.Value.ToMaxDate() : DateTime.Now.ToMaxDate();

            int hotelId = WorkContext.BizKasaContext.HotelId;
            StaticReportModel result = new StaticReportModel();
            var query = _context.InvoiceDetail;
            var data = query.Where(a => a.HotelId == hotelId && a.CreatedDate >= FromDate.Value && a.CreatedDate <= ToDate.Value)
                .GroupBy(a => a.CategoryInvoice)
                .Select(c => new
                {
                    CatetoryInvoice = c.Select(d => d.CategoryInvoice).FirstOrDefault(),
                    SubAmount = c.Select(d => d.SubAmount).Sum()
                }).ToList();

            if (data.Any())
            {
                foreach (var item in data)
                {

                    switch (item.CatetoryInvoice)
                    {
                        case (int)CategoryInvoice.Electronic:
                            result.Electronic = item.SubAmount;
                            break;
                        //case (int)CategoryInvoice.Internet:
                        //    result.Internet = item.SubAmount;
                        //break;
                        case (int)CategoryInvoice.Orther:
                            result.Orther = item.SubAmount;
                            break;
                        case (int)CategoryInvoice.Recept:
                            result.Recept = item.SubAmount;
                            break;
                        //case (int)CategoryInvoice.Repair:
                        //    result.Repair = item.SubAmount;
                        // break;
                        //case (int)CategoryInvoice.Room:
                        //    result.Room = item.SubAmount;
                        //    break;
                        //case (int)CategoryInvoice.Service:
                        //    result.Service = item.SubAmount;
                        //    break;
                        //case (int)CategoryInvoice.Stored:
                        //    result.Stored = item.SubAmount;
                        //    break;
                        case (int)CategoryInvoice.Water:
                            result.Water = item.SubAmount;
                            break;
                            //case (int)CategoryInvoice.Deductible:
                            //    result.Deductible = item.SubAmount;
                            //    break;
                            //case (int)CategoryInvoice.Surcharge:
                            //    result.Surcharge = item.SubAmount;
                            //    break;

                    }
                }
            }


            return result;
        }

        public List<RoomPopularReportModel> GetRoomPopularReport()
        {
            DateTime date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);


            int hotelId = WorkContext.BizKasaContext.HotelId;
            var query = _context.Orders;
            var result = query.Where(a => a.HotelId == hotelId && a.CreatedDate >= firstDayOfMonth && a.CreatedDate <= lastDayOfMonth)
                .GroupBy(a => a.RoomId)
                .Select(c => new RoomPopularReportModel
                {
                    RoomId = c.Select(d => d.RoomId).FirstOrDefault(),
                    RoomName = c.Join(_context.Rooms, o => o.RoomId, r => r.Id, (o, r) => new { RoomName = r.Name }).Select(oc => oc.RoomName).FirstOrDefault(),
                    Count = c.Select(d => d.RoomId).Count()
                }).ToList();



            return result;
        }


        public List<ReceiptReportModel> GetReceiptReport(DateTime? FromDate, DateTime? ToDate)
        {
            FromDate = FromDate.HasValue ? FromDate.Value.ToMinDate() : DateTime.Now.AddDays(-15).ToMinDate();
            ToDate = ToDate.HasValue ? ToDate.Value.ToMaxDate() : DateTime.Now.ToMaxDate();
            List<ReceiptReportModel> result = new List<ReceiptReportModel>();
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var query = _context.Invoices;
            var data = query.Where(a => a.HotelId == hotelId && a.CreatedDate >= FromDate.Value && a.CreatedDate <= ToDate.Value && a.InvoiceType == (int)InvoiceType.Receipt)
                .GroupBy(a => new { Year = a.CreatedDate.Value.Year, Month = a.CreatedDate.Value.Month, Day = a.CreatedDate.Value.Day })
                .Select(c => new ReceiptReportModel
                {
                    Amount = c.Select(d => d.TotalAmount).Sum(),
                    CreatedDate = c.Select(d => d.CreatedDate).FirstOrDefault().Value
                }).ToList();

            do
            {
                FromDate = FromDate.Value.AddDays(1);
                ReceiptReportModel row = new ReceiptReportModel() { Amount = 0, CreatedDate = FromDate.Value };
                foreach (var item in data)
                {
                    if (FromDate.Value.Date == item.CreatedDate.Date)
                    {
                        row.Amount = item.Amount;
                        continue;
                    }
                }
                result.Add(row);

            } while (FromDate < ToDate);


            return result;
        }


        #region Shift

        public bool AddOrUpdateShift(ShiftDTO dto)
        {
            var m_shiftRepository = unitOfWork.Repository<Shift>();
            if (dto.UserId == WorkContext.BizKasaContext.UserId && WorkContext.BizKasaContext.UserType != (int)UserType.Admin)
            {
                base.AddError("Tài khoản nhận ca không hợp lệ");
                return false;
            }
            int hotelId = WorkContext.BizKasaContext.HotelId;
            int userLoginId = WorkContext.BizKasaContext.UserId;
            int shiftId = WorkContext.BizKasaContext.ShiftId;

            // lay thong tin tai khoan nhan ca
            var m_userRepository = unitOfWork.Repository<User>();
            var m_user = m_userRepository.Get(a => a.Id == dto.UserId);
            if (m_user.IsInShift)
            {
                base.AddError("Tài khoản này đang trong ca ");
                return false;
            }
            if (m_user.UserType == UserType.Admin)
            {
                base.AddError("Tài khoản này không được nhận ca! ");
                return false;
            }

            int m_shiftCount = m_shiftRepository.GetQueryable().Count(a => a.HotelId == hotelId);
            if (m_shiftCount <= 0)
            {
                var m_shift = new Shift()
                {
                    StartTime = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    OpenAmount = dto.OpenAmount,
                    UserId = dto.UserId,
                    HotelId = WorkContext.BizKasaContext.HotelId
                };
                m_shiftRepository.Add(m_shift);
            }
            else
            {
                var query = m_shiftRepository.GetQueryable()
                   .Where(a => !a.EndTime.HasValue && a.HotelId == hotelId);
                if (WorkContext.BizKasaContext.UserType != (int)UserType.Admin)
                {
                    query = query.Where(a => a.Id == shiftId);
                }

                var m_shiftCurrent = query.FirstOrDefault();

                if (m_shiftCurrent.UserId != userLoginId && WorkContext.BizKasaContext.UserType != (int)UserType.Admin)
                {
                    base.AddError("Tài khoản này không được giao ca !");
                    return false;
                }



                var m_shift = new Shift()
                {
                    StartTime = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    OpenAmount = dto.OpenAmount,
                    UserId = dto.UserId,
                    HotelId = WorkContext.BizKasaContext.HotelId
                };

                m_shiftRepository.Add(m_shift);

                m_shiftCurrent.EndTime = m_shift.StartTime;
                m_shiftCurrent.CloseAmount = m_shift.OpenAmount;
                m_shiftCurrent.ReceiptAmount = dto.ReceiptAmount;
                m_shiftCurrent.DeliveryAmount = dto.DeliveryAmount;
                //m_shiftCurrent.DeliveryManagerAmount = dto.DeliveryManagerAmount;
                m_shiftRepository.Update(m_shiftCurrent);


            }


            // danh dau tai khoan dang trong ca


            m_user.IsInShift = true;
            m_user.IsLocked = false;
            m_userRepository.Update(m_user);
            // cap nhat nhung tai khoan khac ngoai ca

            var m_userOutSide = m_userRepository
                .GetMany(a => a.Id != dto.UserId && a.HotelId == hotelId)
            .ToList();

            foreach (var item in m_userOutSide)
            {
                item.IsInShift = false;
                item.IsLocked = true;
                m_userRepository.Update(item);
            }

            // kiem tra du lieu giao ca


            unitOfWork.Commit();

            return !this.HasError;
        }

        public ShiftDTO GetShiftLastest(int shiftId)
        {
            var m_shiftRepository = unitOfWork.Repository<Shift>().GetQueryable();
            var m_shift = m_shiftRepository.Where(a =>
           a.Id == shiftId)
            .Select(a => new ShiftDTO()
            {
                CloseAmount = a.CloseAmount,
                DeliveryAmount = a.DeliveryAmount,
                CreatedDate = a.CreatedDate,
                Id = a.Id,
                UserId = a.UserId,
                StartTime = a.StartTime,
                OpenAmount = a.OpenAmount,
                DeliveryManagerAmount = a.DeliveryManagerAmount,
                ReceiptAmount = a.ReceiptAmount,
                EndTime = a.EndTime,

            }).FirstOrDefault();
            return m_shift;
        }

        public SummaryInShift SummaryInShift()
        {
            SummaryInShift result = new SummaryInShift();
            var m_shiftRepository = unitOfWork.Repository<Shift>();

            int m_userId = WorkContext.BizKasaContext.UserId;
            int m_hotelId = WorkContext.BizKasaContext.HotelId;
            int m_cur_shiftId = WorkContext.BizKasaContext.ShiftId;
            int shiftId = WorkContext.BizKasaContext.ShiftId;
            if (WorkContext.BizKasaContext.UserType == (int)UserType.Admin)
            {
                var m_lastShift = m_shiftRepository.GetQueryable()
                    .Where(a => a.HotelId == WorkContext.BizKasaContext.HotelId && a.UserId != m_userId && !a.EndTime.HasValue && a.User.IsInShift && !a.IsDeleted)
                    .OrderByDescending(a => a.Id).Take(1).FirstOrDefault();
                if (m_lastShift != null)
                {
                    shiftId = m_cur_shiftId = m_lastShift.Id;                     
                    m_userId = m_lastShift.UserId;
                }

            }
            var m_shiftLatest = GetShiftLastest(shiftId);
            var m_invoiceRepository = unitOfWork.Repository<Invoice>();
            var m_orderRepository = unitOfWork.Repository<Order>();
            var m_orderDetailRepository = unitOfWork.Repository<OrderDetail>();
            var fromDate = DateTime.Now.ToWorkingDate();
            var toDate = DateTime.Now.AddDays(1).ToWorkingDate();
            if (m_shiftLatest != null)
            {
                fromDate = m_shiftLatest.StartTime;
                result.ShiftPrev = m_shiftLatest;
                result.ManagerAmount = m_shiftLatest.DeliveryManagerAmount;
            }
            //  var m_invoiceCurrents

            var m_queryInvoice = m_invoiceRepository
                .GetQueryable()
                .Where(a => a.CheckOutDate >= fromDate
            && a.CheckOutDate <= toDate
            && a.InvoiceStatus == (int)InvoiceStatus.Completed);

            var m_orderQuery = m_orderDetailRepository.GetQueryable().Where(a => a.CreatedDate >= fromDate
            && a.CreatedDate <= toDate && !a.IsDeleted);


            var m_receiptQuery = m_invoiceRepository.GetQueryable().Where(a => a.CreatedDate >= fromDate
          && a.CreatedDate <= toDate
          && a.InvoiceType == (int)InvoiceType.Payment);

            m_queryInvoice = m_queryInvoice.Where(a => a.HotelId == m_hotelId);

            m_receiptQuery = m_receiptQuery.Where(a => a.HotelId == m_hotelId);

            // tinh tong tien khach tra trong ca
            int m_invoiceCount = m_queryInvoice.Count();
            result.InvoiceAmount = m_invoiceCount > 0 ? m_queryInvoice.Sum(a => a.Cashed) : 0;

            // tinh tong tien khach tra truoc trong ca
            int m_orderCount = m_orderQuery.Where(a => a.DetailTypeId == (int)CategoryInvoice.Prepaid && a.ShiftId == m_cur_shiftId).Count();
            result.ReceiptAmount = m_orderCount > 0 ? m_orderQuery.Where(a => a.DetailTypeId == (int)CategoryInvoice.Prepaid && a.ShiftId == m_cur_shiftId).Sum(a => a.SubAmount) : 0;

            // tinh tong tien chi trong ca
            //int m_receiptCount = m_receiptQuery.Count();
            //result.DeliveryAmount = m_receiptCount>0? m_receiptQuery.Sum(a => a.TotalAmount):0;

            return result;
        }
        public bool TransferToManager(ShiftTransferManagerDTO data)
        {
            try
            {
                if (data.TransferAmount > data.MaxTransferAmount)
                {
                    base.AddError("Số tiền giao quản lý không lớn hơn số tiền giao ca");
                    return false;
                }
                var m_shiftRepository = unitOfWork.Repository<Shift>();
                if (WorkContext.BizKasaContext.UserType == (int)UserType.Admin)
                {
                    var m_lastShift = m_shiftRepository.GetQueryable()
                        .Where(a => a.HotelId == WorkContext.BizKasaContext.HotelId && !a.EndTime.HasValue && a.User.IsInShift)
                        .OrderByDescending(a => a.Id).Take(1).FirstOrDefault();
                    if (m_lastShift != null)
                    {
                        data.ShiftId = m_lastShift.Id;
                        //m_userId = m_lastShift.UserId;
                    }

                }


                var shift = m_shiftRepository.GetQueryable().Where(a => a.Id == data.ShiftId && a.HotelId == data.HotelId).FirstOrDefault();
                if (shift == null)
                {
                    base.AddError("Ca không tồn tại");
                    return false;
                }
                bool isExistuser = unitOfWork.Repository<User>().GetQueryable().Where(a => a.Id == data.ManagerId && a.Password == data.ManagerPassword).Count() > 0;
                if (!isExistuser)
                {
                    base.AddError("Quản lý xác nhận không thành công");
                    return false;
                }
                shift.DeliveryManagerAmount = shift.DeliveryManagerAmount + data.TransferAmount;
                m_shiftRepository.Update(shift);

                unitOfWork.Commit();
                return !this.HasError;
            }
            catch (Exception ex)
            {

                base.AddError("Cập nhật dữ liệu không thành công");
                return false;
            }

        }
        #endregion
    }
}
