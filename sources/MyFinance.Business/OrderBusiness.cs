using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Data;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Domain.Entities;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Extention;
using System.Transactions;

namespace MyFinance.Business
{
    public interface IOrderBusiness : IBusinessBase
    {
        int AddOrder(OrderRowModel data);
        bool AddOrderMutil(OrderRowModel data);
        OrderRowModel GetOrderForEdit(int orderId);
        bool UpdateOrder(OrderRowModel data);
        bool BookingOrder(OrderRowModel data);
        OrderRowForCheckoutModel GetOrderForCheckOut(RequestCheckOutModel request);
        List<OrderRowCompany> GetOrderByCompany(int hotelId);
        OrderRowForCompanyCheckOut CompanyCheckOut(List<int> OrderIds, int mode);
        List<OrderBookingRowModel> GetBookingOrders(InvoiceFilterModel filter, out int total);
        bool TranferBookingToCheckIn(int orderid);
        
        Folio GetFolioCustomer(int companyId, int typeCustomer);
        List<OrderRowCompany> GetOrderBookingByCompany(OrderFilterModel filter, out int total);
        List<Country> GetCountries();
        List<OrderDetailDTO> AddOrderDetail(OrderDetailDTO data);
        bool DeleteOrderDetail(OrderDetailDTO data);
        bool ChangeRoomInOrder(ChangeRoomInOrderModel model);
        OrderRowForCheckoutModel ChangCalculatorMode(RequestCheckOutModel request);
        // void SaveCategory();
    }
    public class OrderBusiness : BusinessBase, IOrderBusiness
    {
        private readonly MyFinanceContext _context;

        private readonly IUnitOfWork unitOfWork;


        public OrderBusiness(IUnitOfWork unitOfWork
             , MyFinanceContext context
            )
        {
            this.unitOfWork = unitOfWork;
            this._context = context;
        }

        public List<Country> GetCountries()
        {
            return unitOfWork.Repository<Country>().GetAll().ToList();
        }
        public Folio GetFolioCustomer(int companyId, int typeCustomer)
        {

            Folio result = new Folio();
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var orders = unitOfWork.Repository<Order>().GetAll();
            var orderRepository = unitOfWork.Repository<Order>();
            if (typeCustomer == 1)
            {
                orders = orderRepository.GetMany(a => a.CompanyId == companyId && a.HotelId == hotelId);
            }
            else
            {
                orders = orderRepository.GetMany(a => a.Id == companyId && a.HotelId == hotelId);
            }

            var order = orders.FirstOrDefault();
            result.CompanyName = typeCustomer == 1 ? order.CompanyName : order.CompanyName;
            result.CheckInDate = order.CheckInDate.Value;
            result.CheckOutDate = order.CheckOutDate.Value;
            result.Note = order.Notes;

            var orderids = orders.Select(a => a.Id).ToList();
            var customers = (from a in _context.Customers
                             join b in _context.OrderCustomer on a.Id equals b.CustomerId
                             where orderids.Contains(b.OrderId)
                             select new CustomerRowModel()
                             {
                                 Name = a.Name,
                                 Mobile = a.Mobile,
                                 PassportId = a.PassportId,
                                 Address = a.Address,
                                 Email = a.Email,
                                 Notes = a.Notes
                             }).Distinct().ToList();
            result.Customers = customers;

            var roomids = orders.Select(x => x.RoomId).ToList();

            var folioItems = (from c in _context.Orders
                              join a in _context.Rooms on c.RoomId equals a.Id
                              join b in _context.RoomClasses on a.RoomClassId equals b.Id
                              where orderids.Contains(c.Id)
                              select new FolioItem()
                              {
                                  Price = c.Price,
                                  RoomClassName = b.Name,
                                  RoomName = a.Name
                              }).ToList();

            result.FolioItems = folioItems;

            return result;
        }


        public int AddOrder(OrderRowModel data)
        {
            try
            {
                var orderRepo = unitOfWork.Repository<Order>();
                var orderDetailRepo = unitOfWork.Repository<OrderDetail>();

                data.CheckInDate = data.CheckInDate.HasValue ? data.CheckInDate.Value : DateTime.Now;


                data.CheckInTime = data.CheckInDate.HasValue ? data.CheckInDate.Value.TimeOfDay : data.CheckInTime;
                data.CheckOutTime = data.CheckOutDate.HasValue ? data.CheckOutDate.Value.TimeOfDay : data.CheckOutTime;
                var ordercustomerRepository = unitOfWork.Repository<OrderCustomer>();
                var orderseviceRepository = unitOfWork.Repository<OrderService>();

                int result = 0;
                if (data.Id > 0)
                {
                    #region update order


                    var orderurrent = orderRepo.GetById(data.Id);
                    orderurrent.CheckOutDate = data.CheckOutDate;
                    orderurrent.CheckOutTime = data.CheckOutTime;
                    orderurrent.Price = data.Price;
                    orderurrent.Notes = data.Notes;
                    orderurrent.UpdatedDate = DateTime.Now;
                    orderurrent.Deductible = data.Deductible;
                    orderurrent.NumberVehicle = data.NumberVehicle;
                    orderurrent.Card = data.Card;
                    orderurrent.CustomerName = data.CustomerName;
                    
                    orderurrent.ConfigPriceId = data.ConfigPriceId;
                    orderRepo.Update(orderurrent);
                    //if (data.CompanyId.HasValue && !string.IsNullOrWhiteSpace(data.CompanyName))
                    //{
                    //    var com = customerRepository.GetById(data.CompanyId.Value);
                    //    com.Name = data.CompanyName;
                    //    customerRepository.Update(com);
                    //}

                    // insert or update customer in order

                    var orderCus = ordercustomerRepository.GetMany(a => a.OrderId == data.Id);
                    if (orderCus.Any() && data.Customers.Any())
                    {
                        foreach (var item in orderCus)
                        {
                            ordercustomerRepository.Delete(item);
                        }
                    }

                    if (data.Customers.Any())
                    {
                        foreach (var item in data.Customers)
                        {
                            if (item.Id > 0)
                            {
                                var row = new OrderCustomer() { OrderId = data.Id, CustomerId = item.Id };
                                ordercustomerRepository.Add(row);
                                IoC.Get<ICustomerBusiness>().InsertOrUpdateCustomer(item);
                                var customer = unitOfWork.Repository<Customer>().GetById(item.Id);
                                customer.IsHasCheckin = true;
                                unitOfWork.Repository<Customer>().Update(customer);
                            }
                            else
                            {
                                var cusId = IoC.Get<ICustomerBusiness>().InsertOrUpdateCustomer(item);
                                var row = new OrderCustomer() { OrderId = data.Id, CustomerId = cusId };
                                ordercustomerRepository.Add(row);
                                var customer = unitOfWork.Repository<Customer>().GetById(cusId);
                                customer.IsHasCheckin = true;
                                unitOfWork.Repository<Customer>().Update(customer);
                            }


                        }
                    }


                    // end


                    // insert or update OrderService

                    var orderser = orderseviceRepository.GetMany(a => a.OrderId == data.Id);
                    if (orderser.Any() && data.Services.Any())
                    {
                        foreach (var item in orderser)
                        {
                            orderseviceRepository.Delete(item);
                        }
                    }
                    if (data.Services != null && data.Services.Any())
                    {
                        foreach (var item in data.Services)
                        {
                            var row = new MyFinance.Domain.Entities.OrderService() { OrderId = data.Id, ServiceId = item.Id, Quantity = item.Quantity, Price = item.Price };
                            orderseviceRepository.Add(row);
                        }
                    }
                    //end

                    result = data.Id;
                    unitOfWork.Commit();
                    #endregion
                }
                else
                {
                    #region add order


                    var roomRepo = unitOfWork.Repository<Room>();
                    var order = AutoMapper.Mapper.Map<Order>(data);

                    order.OrderStatus =(int) OrderStatus.CheckIn;
                    order.HotelId = WorkContext.BizKasaContext.HotelId;
                    order.ShiftId = WorkContext.BizKasaContext.ShiftId;
                    order.CaculatorMode = CaculatorMode.ByHour;
                    if (order.CheckInDate.Value.Date == DateTime.Now.Date)
                    {
                        var room = roomRepo.Get(a => a.Id == data.RoomId && a.HotelId == WorkContext.BizKasaContext.HotelId);
                        if (room == null)
                        {
                            base.AddError("Phòng chọn không hợp lệ");
                            return 0;
                        }
                        if (room.RoomStatus == RoomStatus.Active)
                        {
                            base.AddError("Phòng chọn không hợp lệ");
                            return 0;
                        }

                        var m_orderExist = orderRepo.GetQueryable()
                            .Where(a => a.RoomId == room.Id && a.OrderStatus == (int)OrderStatus.CheckIn)
                            .ToList();
                        if (m_orderExist.Count > 0)
                        {
                            foreach (var item in m_orderExist)
                            {
                                item.OrderStatus = (int)OrderStatus.Cancel;
                                orderRepo.Update(item);
                            }
                        }
                        room.RoomStatus = RoomStatus.Active;
                        roomRepo.Update(room);


                    }
                    else
                        order.OrderStatus = (int)OrderStatus.Booking;

                    order.CreatedDate = DateTime.Now;
                    // customer

                    if (data.Customers.Any())
                    {
                        foreach (var item in data.Customers)
                        {
                            item.HotelId = WorkContext.BizKasaContext.HotelId;
                            int CusID = IoC.Get<ICustomerBusiness>().InsertOrUpdateCustomer(item);
                            if (item.IsPrimary)
                                order.CustomerId = CusID;

                            var customer = unitOfWork.Repository<Customer>().GetById(CusID);
                            customer.IsHasCheckin = true;
                            unitOfWork.Repository<Customer>().Update(customer);

                            var row = new OrderCustomer() { Order = order, CustomerId = CusID };
                            ordercustomerRepository.Add(row);
                        }

                    }



                    //
                    if (data.Services != null && data.Services.Any())
                    {
                        foreach (var item in data.Services)
                        {
                            var row = new MyFinance.Domain.Entities.OrderDetail()
                            {
                                Order = order,
                                DetailTypeId =(int) CategoryInvoice.Service,
                                RelatedId=item.Id,
                                Quantity = item.Quantity,
                                Price = item.Price,
                                SubAmount=item.Price*item.Quantity,
                                CreatedDate=DateTime.Now,
                                ShiftId=order.ShiftId.Value,
                                Title=item.Name
                            };
                            orderDetailRepo.Add(row);
                        }
                    }

                    // check config price
                    if (order.ConfigPriceId <= 0)
                    {
                        var m_room = unitOfWork.Repository<Room>().Get(a => a.Id == order.RoomId);
                        if (m_room != null)
                        {

                            var m_configPrice = unitOfWork.Repository<ConfigPrice>().Get(a =>
                            a.RoomClassId == m_room.RoomClassId
                            && a.IsDefault == true
                            && a.IsActive == true
                            && a.IsDeleted == false);

                            order.ConfigPriceId = m_configPrice != null ? m_configPrice.Id : 0;
                            if (order.CaculatorMode == CaculatorMode.ByDay)
                                order.Price = m_configPrice != null ? m_configPrice.PriceByDay : 0;
                            if (order.CaculatorMode == CaculatorMode.ByHour)
                                order.Price = m_configPrice != null ? m_configPrice.PriceByDay : 0;
                            if (order.CaculatorMode == CaculatorMode.ByNight)
                                order.Price = m_configPrice != null ? m_configPrice.PriceByNight : 0;
                        }

                    }

                    //
                    orderRepo.Add(order);
                    #endregion
                    //


                    unitOfWork.Commit();
                    result = order.Id;
                }



                return result;
            }
            catch (Exception ex)
            {

                return 0;
            }
           
        }

        public bool BookingOrder(OrderRowModel data)
        {
            data.CheckInTime = data.CheckInDate.HasValue ? data.CheckInDate.Value.TimeOfDay : data.CheckInTime;
            data.CheckOutTime = data.CheckOutDate.HasValue ? data.CheckOutDate.Value.TimeOfDay : data.CheckOutTime;
            var orderRepository = unitOfWork.Repository<Order>();
            var customerRepository = unitOfWork.Repository<Customer>();
            var ordercustomerRepository = unitOfWork.Repository<OrderCustomer>();

            bool result = true;
            //data.OrderStatus = (int)OrderStatus.CheckIn;
            var order = AutoMapper.Mapper.Map<Order>(data);
            order.CreatedDate = DateTime.Now;
            order.UpdatedDate = DateTime.Now;
            orderRepository.Add(order);


            // insert or update customer in order

            if (data.Customers.Any())
            {
                foreach (var item in data.Customers)
                {

                    if (!string.IsNullOrWhiteSpace(item.PassportId))
                    {
                        var cusCurrent = customerRepository.GetMany(a => a.PassportId == item.PassportId).FirstOrDefault();
                        if (cusCurrent != null)
                        {
                            cusCurrent.Address = item.Address;
                            cusCurrent.Email = item.Email;
                            cusCurrent.Mobile = item.Mobile;
                            cusCurrent.Notes = item.Notes;
                            cusCurrent.Name = item.Name;
                            cusCurrent.PassportAgency = item.PassportAgency;
                            cusCurrent.UpdatedDate = DateTime.Now;
                            customerRepository.Update(cusCurrent);
                            var row = new OrderCustomer() { Order = order, CustomerId = cusCurrent.Id };
                            ordercustomerRepository.Add(row);
                        }
                        else
                        {
                            var row = AutoMapper.Mapper.Map<Customer>(item);
                            row.HotelId = data.HotelId;
                            row.IsHasCheckin = true;
                            customerRepository.Add(row);
                            var ordercusadd = new OrderCustomer() { Order = order, Customer = row };
                            ordercustomerRepository.Add(ordercusadd);
                        }
                    }
                    else
                    {
                        var row = AutoMapper.Mapper.Map<Customer>(item);
                        row.IsHasCheckin = true;
                        row.HotelId = data.HotelId;
                        customerRepository.Add(row);
                        var ordercusadd = new OrderCustomer() { Order = order, Customer = row };
                        ordercustomerRepository.Add(ordercusadd);
                    }

                }
            }

            //end




            unitOfWork.Commit();

            return result;
        }


        public List<OrderBookingRowModel> GetBookingOrders(InvoiceFilterModel filter, out int total)
        {
            filter.Page.currentPage--;
            int hotelId = WorkContext.BizKasaContext.HotelId;
            filter.FromDate = filter.FromDate.HasValue ? filter.FromDate.Value.ToMinDate() : filter.FromDate;
            filter.ToDate = filter.ToDate.HasValue ? filter.ToDate.Value.ToMaxDate() : filter.ToDate;

            List<OrderBookingRowModel> result = new List<OrderBookingRowModel>();
            var order = (from a in _context.Orders
                         where a.HotelId == hotelId && a.OrderStatus == (int)OrderStatus.Booking
                         select new OrderBookingRowModel
                         {
                             OrderId = a.Id,
                             CustomerName = a.CustomerName,
                             RoomName = (from x in _context.Rooms where x.Id == a.RoomId select x.Name).FirstOrDefault(),
                             CheckInDate = a.CheckInDate.Value,
                             CheckOutDate = a.CheckOutDate.Value,
                             CheckInTime = a.CheckInTime.Value,
                             CheckOutTime = a.CheckOutTime,
                             CreatedDate = a.CreatedDate.Value,
                             OrderCode = string.Empty
                         });

            if (filter.FromDate.HasValue && filter.ToDate.HasValue)
                order = order.Where(a => a.CreatedDate >= filter.FromDate.Value && a.CreatedDate <= filter.ToDate.Value);
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
                order = order.Where(a => a.OrderCode.Contains(filter.Keyword) || a.CustomerName.Contains(filter.Keyword) || a.RoomName.Contains(filter.Keyword));
            total = order.Count();
            result = order.OrderByDescending(a => a.CreatedDate).Skip(filter.Page.currentPage * filter.Page.pageSize).Take(filter.Page.pageSize).ToList();


            return result;
        }

        public bool TranferBookingToCheckIn(int orderid)
        {
            var order = unitOfWork.Repository<Order>().GetById(orderid);
            var room = unitOfWork.Repository<Room>().GetById(order.RoomId);
            if (room.RoomStatus == RoomStatus.Active)
            {
                AddError("Phòng đang được sử dụng, Vui lòng chọn Phòng Khác !");
                return false;
            }
            order.CheckInDate = DateTime.Now;
            order.CheckInTime = DateTime.Now.TimeOfDay;
            order.OrderStatus = (int)OrderStatus.CheckIn;
            room.RoomStatus = RoomStatus.Active;
            unitOfWork.Repository<Order>().Update(order);
            unitOfWork.Repository<Room>().Update(room);
            unitOfWork.Commit();
            return !this.HasError;

        }
        private void ProcessOrder(OrderRowModel data)
        {
           
                decimal totalamount = 0;
                var orderRepository = unitOfWork.Repository<Order>();
                var customerRepository = unitOfWork.Repository<Customer>();
                var roomRepository = unitOfWork.Repository<Room>();

                var order = orderRepository.GetById(data.Id);
                order.TotalAmount = data.TotalAmount;
                order.CheckOutDate = DateTime.Now;
                totalamount = data.TotalAmount;
                var com = order.CustomerId.HasValue ? customerRepository.GetById(order.CustomerId.Value) :
                    null;

                var room = roomRepository.GetById(data.RoomId);
                var roomClass = (from a in _context.RoomClasses
                                 where a.Id == room.RoomClassId
                                 select a.Name).FirstOrDefault();
                List<InvoiceDetailRowModel> invoiceDetails = new List<InvoiceDetailRowModel>();
                var sb = new StringBuilder();
                if (data.TimeUseds.Any())
                {
                    foreach (var item in data.TimeUseds)
                    {
                        sb.AppendLine(item.Description);
                    }
                }
                var invoiceDetail = new InvoiceDetailRowModel()
                {
                    Quantity = 1,
                    Descriptions = "Tiền phòng -" + room.Name,
                    Notes = sb.ToString(),
                    Price = order.TotalAmount,
                    SubAmount = order.TotalAmount,
                    CategoryInvoice = CategoryInvoice.Room

                };
                invoiceDetails.Add(invoiceDetail);
                if (data.Services != null && data.Services.Any())
                {
                    foreach (var item in data.Services)
                    {
                        var invoicerowDetail = new InvoiceDetailRowModel()
                        {
                            Quantity = item.Quantity,
                            ServiceId = item.Id,
                            Notes = "x " + item.Quantity,
                            Price = item.Price,
                            Descriptions = item.Name,
                            SubAmount = item.Quantity * item.Price,
                            CategoryInvoice = CategoryInvoice.Service

                        };
                        totalamount += invoicerowDetail.SubAmount;
                        invoiceDetails.Add(invoicerowDetail);
                    }
                }

                var customer = data.CustomerId.HasValue? unitOfWork.Repository<Customer>().GetById(data.CustomerId.Value):null;

                var invoice = new InvoiceRowModel()
                {
                    InvoiceDetails = invoiceDetails,
                    OrderId = order.Id,
                    CompanyName = order.CompanyName,
                    CustomerName = order.CustomerName,
                    RoomName = room.Name,
                    RoomClassName = roomClass,
                    HotelId = order.HotelId,
                    TotalAmount = totalamount,
                    CheckInDate = data.CheckInDate,
                    CheckOutDate = data.CheckOutDate,
                    Notes = data.Notes,
                    Deductible = data.Deductible,
                    Prepaid = data.Prepaid,
                    Surcharge = data.Surcharge,
                    Cashed = data.Cashed,
                    UserId = WorkContext.BizKasaContext.UserId,
                    InvoiceType = (int)InvoiceType.Receipt,
                    InvoiceStatus = data.OrderStatus,
                    UserUpdate = WorkContext.BizKasaContext.UserName,
                    PaymentMethod = (int)data.PaymentMethod,
                    CreatedDate = DateTime.Now

                };





                int count = order.CompanyId.HasValue ? orderRepository.GetMany(a => a.CompanyId == order.CompanyId && a.OrderStatus == (int)OrderStatus.CheckIn).Count() : 0;
                if (com != null && count <= 1)
                {
                    com.IsHasCheckin = false;
                    customerRepository.Update(com);
                }
                if (customer != null)
                {
                    invoice.Email = customer.Email;
                    invoice.Address = customer.Address;
                    invoice.Mobile = customer.Mobile;
                    customer.IsHasCheckin = false;
                    customerRepository.Update(customer);
                }


                invoice.InvoiceStatus = CalculateInvoiceStatus(invoice);
            // add or update invoice
            using (var transaction =new TransactionScope())
            {
                if (IoC.Get<IInvoiceBusiness>().InsertOrUpdateInvoice(invoice) > 0)
                {
                    // update order info
                    order.OrderStatus = (int)OrderStatus.Completed;
                    orderRepository.Update(order);

                    var config = unitOfWork.Repository<HotelConfig>().Get(a => a.HotelId == WorkContext.BizKasaContext.HotelId);
                    // update room status
                    room.RoomStatus = config.HasCleaner ? RoomStatus.Refresh : RoomStatus.InActive;
                    roomRepository.Update(room);
                }
                unitOfWork.Commit();
                transaction.Complete();
            }

           
           
        }

        private int CalculateInvoiceStatus(InvoiceRowModel invoice)
        {
            if (invoice.InvoiceStatus == (int)OrderStatus.NotPaid) return invoice.InvoiceStatus;
            decimal total = invoice.TotalAmount;
            decimal Paid = invoice.Cashed + invoice.Prepaid;

            if (total - Paid > 0 && Paid > 0)
            {
                return (int)OrderStatus.Paid;
            }
            if (Paid <= 0)
                return (int)OrderStatus.NotPaid;
            return (int)OrderStatus.Completed;
        }

        //item.OrderId, item.RoomId,item.TotalAmount
        private void ProcessOrderAttachment(OrderRowForCheckoutModel data)
        {
            decimal totalamount = 0;
            var orderRepository = unitOfWork.Repository<Order>();
            var customerRepository = unitOfWork.Repository<Customer>();
            var roomRepository = unitOfWork.Repository<Room>();


            var order = orderRepository.GetById(data.Id);
            order.OrderStatus = data.OrderStatus;
            order.CheckOutDate = DateTime.Now;

            var com = order.CustomerId.HasValue ? customerRepository.GetById(order.CustomerId.Value) :
                null;

            var room = roomRepository.GetById(data.RoomId);
            var roomClass = (from a in _context.RoomClasses
                             where a.Id == room.RoomClassId
                             select a.Name).FirstOrDefault();
            List<InvoiceDetailRowModel> invoiceDetails = new List<InvoiceDetailRowModel>();
            var sb = new StringBuilder();
            if (data.TimeUseds.Any())
            {
                foreach (var item in data.TimeUseds)
                {
                    sb.Append(item.Description);
                    sb.AppendLine();
                    totalamount += item.SumAmount;
                }
            }



            var invoiceDetail = new InvoiceDetailRowModel()
            {

                Quantity = 1,
                Descriptions = "Tiền phòng -" + room.Name,
                Notes = sb.ToString(),
                Price = order.TotalAmount,
                SubAmount = order.TotalAmount,
                CategoryInvoice = CategoryInvoice.Room

            };
            invoiceDetails.Add(invoiceDetail);




            if (data.Services != null && data.Services.Any())
            {
                foreach (var item in data.Services)
                {
                    var invoicerowDetail = new InvoiceDetailRowModel()
                    {
                        Quantity = item.Quantity,
                        ServiceId = item.Id,
                        Notes = "x " + item.Quantity,
                        Price = item.Price,
                        Descriptions = item.Name,
                        SubAmount = item.Quantity * item.Price,
                        CategoryInvoice = CategoryInvoice.Service

                    };
                    totalamount += invoicerowDetail.SubAmount;
                    invoiceDetails.Add(invoicerowDetail);
                }
            }
            var customer = unitOfWork.Repository<Customer>().GetById(data.CustomerId.Value);

            var invoice = new InvoiceRowModel()
            {

                InvoiceDetails = invoiceDetails,
                OrderId = order.Id,
                CompanyName = order.CompanyName,
                CustomerName = order.CustomerName,
                RoomName = room.Name,
                RoomClassName = roomClass,
                HotelId = order.HotelId,
                TotalAmount = totalamount,
                CheckInDate = data.CheckInDate,
                CheckOutDate = data.CheckOutDate,
                Notes = data.Notes,
                Deductible = data.Deductible,
                Prepaid = data.Prepaid,
                Cashed = totalamount,
                Surcharge = data.Surcharge,
                InvoiceStatus = data.OrderStatus,
                InvoiceType = (int)InvoiceType.Receipt,
                UserId = WorkContext.BizKasaContext.UserId,
                UserUpdate = WorkContext.BizKasaContext.UserName,
                PaymentMethod = (int)data.PaymentMethod,
                CreatedDate = DateTime.Now

            };



            int count = order.CompanyId.HasValue ? orderRepository.GetMany(a => a.CompanyId == order.CompanyId && a.OrderStatus == (int)OrderStatus.CheckIn).Count() : 0;
            if (com != null && count <= 1)
            {
                com.IsHasCheckin = false;
                customerRepository.Update(com);
            }

            if (customer != null)
            {
                invoice.Email = customer.Email;
                invoice.Address = customer.Address;
                invoice.Mobile = customer.Mobile;
                customer.IsHasCheckin = false;
                customerRepository.Update(customer);
            }
            invoice.InvoiceStatus = CalculateInvoiceStatus(invoice);

            IoC.Get<IInvoiceBusiness>().InsertOrUpdateInvoice(invoice);
            order.TotalAmount = totalamount;
            orderRepository.Update(order);
            var config = (from a in _context.HotelConfig
                          where a.HotelId == WorkContext.BizKasaContext.HotelId
                          select a).FirstOrDefault();

            room.RoomStatus = config.HasCleaner ? RoomStatus.Refresh : RoomStatus.InActive;

            roomRepository.Update(room);
            unitOfWork.Commit();
        }
        public bool UpdateOrder(OrderRowModel data)
        {
            try
            {
                var orderseviceRepository = unitOfWork.Repository<OrderService>();
                var ordercustomerRepository = unitOfWork.Repository<OrderCustomer>();
                var customerRepository = unitOfWork.Repository<Customer>();

                var orderRepo = unitOfWork.Repository<Order>();
                bool result = true;
                if (data.Id > 0)
                {
                    var ordercurrent = orderRepo.GetById(data.Id);
                    ordercurrent.Deductible = data.Deductible;
                    ordercurrent.Cashed = data.Cashed;
                    ordercurrent.Surcharge = data.Surcharge;
                    ordercurrent.Prepaid = data.Prepaid;
                    ordercurrent.Notes = data.Notes;
                    ordercurrent.UpdatedDate = DateTime.Now;
                    ordercurrent.OrderStatus = data.OrderStatus;
                    ordercurrent.PaymentMethod = (int)data.PaymentMethod;
                    ordercurrent.CustomerName = data.CustomerName;
                    ordercurrent.PassportId = data.PassportId;
                    //ordercurrent.p
                    ordercurrent.Price = data.Price;
                    ordercurrent.ConfigPriceId = data.ConfigPriceId;
                    data.CustomerId = ordercurrent.CustomerId;
                    if (data.OrderStatus == (int)OrderStatus.Completed || data.OrderStatus == (int)OrderStatus.NotPaid)
                    {
                        ordercurrent.UserId = WorkContext.BizKasaContext.UserId;
                        ordercurrent.CheckOutDate = DateTime.Now;
                        ordercurrent.CheckOutTime = DateTime.Now.TimeOfDay;
                    }

                    orderRepo.Update(ordercurrent);
                    unitOfWork.Commit();
                    // insert or update OrderService
                    
                    //end


                    // insert or update customer in order

                    var orderCus = ordercustomerRepository.GetMany(a => a.OrderId == data.Id);
                    if (orderCus.Any() && data.Customers != null && data.Customers.Any())
                    {
                        foreach (var item in orderCus)
                        {
                            ordercustomerRepository.Delete(item);
                        }
                    }

                    if (data.Customers != null && data.Customers.Any())
                    {
                        foreach (var item in data.Customers)
                        {

                            if (!string.IsNullOrWhiteSpace(item.PassportId))
                            {
                                var cusCurrent = customerRepository.GetMany(a => a.PassportId == item.PassportId).FirstOrDefault();
                                if (cusCurrent != null)
                                {
                                    cusCurrent.Address = item.Address;
                                    cusCurrent.Email = item.Email;
                                    cusCurrent.Mobile = item.Mobile;
                                    cusCurrent.Notes = item.Notes;
                                    cusCurrent.Name = item.Name;
                                    cusCurrent.PassportAgency = item.PassportAgency;
                                    cusCurrent.UpdatedDate = DateTime.Now;

                                    customerRepository.Update(cusCurrent);

                                    var row = new OrderCustomer() { OrderId = data.Id, CustomerId = cusCurrent.Id };
                                    ordercustomerRepository.Add(row);
                                }
                                else
                                {
                                    var row = AutoMapper.Mapper.Map<Customer>(item);
                                    row.HotelId = data.HotelId;
                                    row.CustomerType = CustomerType.Customer;
                                    customerRepository.Add(row);
                                    var ordercusadd = new OrderCustomer() { OrderId = data.Id, Customer = row };
                                    ordercustomerRepository.Add(ordercusadd);
                                }
                            }
                            else
                            {
                                var row = AutoMapper.Mapper.Map<Customer>(item);
                                row.HotelId = data.HotelId;
                                row.CustomerType = CustomerType.Company;
                                customerRepository.Add(row);
                                var ordercusadd = new OrderCustomer() { OrderId = data.Id, Customer = row };
                                ordercustomerRepository.Add(ordercusadd);
                            }
                        }
                    }

                    // end
                    if (data.OrderStatus == (int)OrderStatus.Completed || data.OrderStatus == (int)OrderStatus.NotPaid)
                    {
                       
                        var req = new RequestCheckOutModel()
                        {
                            hotelId=data.HotelId,
                            orderId=data.Id,
                           // isByNight=
                           mode=(int)data.CaculatorMode
                        };
                        var orderComplete = GetOrderForCheckOut(req);
                        CompletedOrder(orderComplete);

                    }
                }
               
                return result;
            }
            catch (Exception ex)
            {
                base.AddError(ex.Message);
                return false;
            }

        }

        /// <summary>
        /// xư ly hoan tat order
        /// </summary>
        /// <param name="data"></param>
        private void CompletedOrder(OrderRowForCheckoutModel order)
        {
            try
            {
                var customerRepository = unitOfWork.Repository<Customer>();
                var roomRepository = unitOfWork.Repository<Room>();

                List<InvoiceDetailRowModel> invoiceDetails = new List<InvoiceDetailRowModel>();

                if (order.OrderDetails != null && order.OrderDetails.Any())
                {
                    foreach (var item in order.OrderDetails)
                    {
                        var invoicerowDetail = new InvoiceDetailRowModel()
                        {
                            Quantity = item.Quantity,
                            ServiceId = item.RelatedId,
                            Notes = "x " + item.Quantity,
                            Price = item.Price,
                            Descriptions = item.Title,
                            SubAmount = item.SubAmount,
                            CategoryInvoice = (CategoryInvoice)item.DetailTypeId,
                            CreatedDate = DateTime.Now,
                            HotelId = order.HotelId,


                        };

                        invoiceDetails.Add(invoicerowDetail);
                    }
                }
                var customer = order.CustomerId.HasValue ? unitOfWork.Repository<Customer>().GetById(order.CustomerId.Value) : null;

                var invoice = new InvoiceRowModel()
                {
                    InvoiceDetails = invoiceDetails,
                    OrderId = order.Id,
                    //ShiftId=order.
                    CompanyName = order.CompanyName,
                    CustomerName = order.CustomerName,
                    RoomName = order.RoomName,
                    RoomClassName = order.RoomClassName,
                    HotelId = order.HotelId,
                    TotalAmount = order.TotalAmount,
                    ShiftId = order.ShiftId,
                    CheckInDate = order.CheckInDate,
                    CheckOutDate = order.CheckOutDate,
                    Notes = order.Notes,
                    Deductible = order.DeductibleAmount,
                    Prepaid = order.PrepaidAmount,
                    Surcharge = order.SurchargeAmount,
                    Cashed = order.Cashed,
                    UserId = WorkContext.BizKasaContext.UserId,
                    InvoiceType = (int)InvoiceType.Receipt,
                    InvoiceStatus = order.OrderStatus,
                    UserUpdate = WorkContext.BizKasaContext.UserName,
                    PaymentMethod = (int)order.PaymentMethod,
                    CreatedDate = DateTime.Now

                };
                if (customer != null)
                {
                    invoice.Email = customer.Email;
                    invoice.Address = customer.Address;
                    invoice.Mobile = customer.Mobile;
                    customer.IsHasCheckin = false;
                    customerRepository.Update(customer);
                }


                invoice.InvoiceStatus = CalculateInvoiceStatus(invoice);
                // add or update invoice
                using (var transaction = new TransactionScope())
                {
                    int SuccessInsert = IoC.Get<IInvoiceBusiness>().InsertOrUpdateInvoice(invoice);
                    if (SuccessInsert > 0)
                    {
                        // update order info
                        var room = roomRepository.GetById(order.RoomId);
                        var config = unitOfWork.Repository<HotelConfig>().Get(a => a.HotelId == order.HotelId);
                        // update room status
                        room.RoomStatus = config.HasCleaner ? RoomStatus.Refresh : RoomStatus.InActive;
                        roomRepository.Update(room);
                    }
                    unitOfWork.Commit();
                    transaction.Complete();
                }
            }
            catch (Exception ex)
            {
                
                return;
            }
        }



        public bool ChangeRoomInOrder(ChangeRoomInOrderModel model)
        {
            try
            {
                if (model.configPriceId <= 0)
                {
                    this.AddError("Chưa chọn cấu hình giá!");
                    return false;
                }
                var orderRepo = unitOfWork.Repository<Order>();
                var order = orderRepo.GetQueryable().Where(a => a.Id == model.orderId && a.HotelId == model.hotelId).FirstOrDefault();
                if (order == null)
                {
                    this.AddError("Thông tin nhận phòng không tồn tại !");
                    return false;
                }
                var roomRepo = unitOfWork.Repository<Room>();
                var roomNew = roomRepo.GetQueryable().Where(a => a.Id == model.roomId && a.HotelId == model.hotelId).FirstOrDefault();
                if (roomNew == null)
                {
                    this.AddError("Phòng được chọn không tồn tại !");
                    return false;
                }

                if (model.isOnlyChangeRoom)
                {



                    var roomOld = roomRepo.GetById(order.RoomId);
                    roomOld.RoomStatus = RoomStatus.InActive;
                    roomRepo.Update(roomOld);

                    roomNew.RoomStatus = RoomStatus.Active;
                    roomRepo.Update(roomNew);

                    var config = unitOfWork.Repository<ConfigPrice>().GetById(model.configPriceId);
                    order.ConfigPriceId = model.configPriceId;
                    order.Price = config.PriceByDay;
                    order.RoomId = model.roomId;
                    order.UpdatedDate = DateTime.Now;
                    orderRepo.Update(order);

                    unitOfWork.Commit();

                    return true;
                }

                if (order.OrderStatus == (int)OrderStatus.CheckIn)
                {
                    var orderNew = new OrderRowModel()
                    {
                        RoomId = model.roomId,
                        ConfigPriceId = model.configPriceId,
                        HotelId = order.HotelId,
                        CaculatorMode = CaculatorMode.ByHour,
                        CustomerName = order.CustomerName
                    };
                    int newOrderId = AddOrder(orderNew);
                    if (newOrderId <= 0)
                    {
                        this.AddError("Chuyển phòng không thành công");
                        return false;
                    }
                    var roomOld = roomRepo.GetById(order.RoomId);
                    var data = new MergeOrderModel()
                    {
                        DestinationId = newOrderId,
                        SourceId = model.orderId,
                        SourceRoomName = roomOld.Name
                    };
                    bool attResult = AttachOrder(data);
                    if (!attResult)
                    {
                        this.AddError("Chuyển phòng không thành công");
                        return false;
                    }

                    roomNew.RoomStatus = RoomStatus.Active;
                    order.UpdatedDate = DateTime.Now;
                    roomRepo.Update(roomNew);
                }

                unitOfWork.Commit();
                return !this.HasError;
            }
            catch (Exception ex)
            {
                return false;

            }




        }
        /// <summary>
        /// Attach order 
        /// </summary>
        /// <returns></returns>
        public bool AttachOrder(MergeOrderModel model)
        {
            try
            {
                var repo = unitOfWork.Repository<Order>();
                var order = repo.GetQueryable().Where(a => a.Id == model.SourceId).FirstOrDefault();
                order.Notes = string.Format("Chuyển thanh toán từ phòng {0}", model.SourceRoomName);
                order.ParentId = model.DestinationId;
                order.OrderStatus = (int)OrderStatus.NotPaid;
                order.CheckOutDate = DateTime.Now;
                order.CheckOutTime = DateTime.Now.TimeOfDay;
               
                repo.Update(order);

                var repoRoom = unitOfWork.Repository<Room>();
                var room = repoRoom.GetById(order.RoomId);
                room.RoomStatus =RoomStatus.InActive;
                repoRoom.Update(room);
                unitOfWork.Commit();
                return !this.HasError;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public bool AddOrderMutil(OrderRowModel data)
        {
            if (data.Company == null)
            {
                this.AddError("Cần nhập tên công ty / đoàn!");
                return false;
            }
            if (string.IsNullOrWhiteSpace(data.Company.Name))
            {
                this.AddError("Cần nhập tên công ty /đoàn !");
                return false;
            }
            //if (string.IsNullOrWhiteSpace(data.CustomerName))
            //{
            //    this.AddError("Cần nhập tên người đại diện !");
            //    return false;
            //}
            if (data.RoomIds.Count <= 0)
            {
                this.AddError("Cần nhập số phòng sử dụng !");
                return false;
            }
            if (!data.CheckInDate.HasValue || !data.CheckOutDate.HasValue)
            {
                this.AddError("Cần nhập thời gian ra vào !");
                return false;
            }
            bool result = true;
            using (var transaction = new TransactionScope())
            {


                if (data == null) return false;

                var customerRepository = unitOfWork.Repository<Customer>();
                var roomRepository = unitOfWork.Repository<Room>();
                var ordercustomerRepository = unitOfWork.Repository<OrderCustomer>();
                var orderRepository = unitOfWork.Repository<Order>();
                Customer com = null;
                if (data.Company.Id <= 0)
                {
                    data.Company.CustomerType = CustomerType.Company;
                    com = AutoMapper.Mapper.Map<Customer>(data.Company); //new Customer() { Name = data.CompanyName, CustomerType = CustomerType.Company, HotelId = data.HotelId, IsHasCheckin = true };
                    com.IsHasCheckin = data.CheckInDate.Value.Date == DateTime.Now.Date;
                    com.HotelId = WorkContext.BizKasaContext.HotelId;
                    customerRepository.Add(com);

                }
                else
                {
                    var company = customerRepository.GetById(data.Company.Id);
                    company.Address = data.Company.Address;
                    company.Mobile = data.Company.Mobile;
                    company.Name = data.Company.Name;
                    company.IsHasCheckin = data.CheckInDate.Value.Date == DateTime.Now.Date;
                    customerRepository.Update(company);
                }


                int cusId = 0;
                if (data.Customers != null)
                {
                    var cus = data.Customers.Where(a => a.IsPrimary).FirstOrDefault();
                    if (cus != null && cus.Id <= 0)
                        cusId = IoC.Get<ICustomerBusiness>().InsertOrUpdateCustomer(cus);
                    else if (cus.Id > 0)
                    {
                        cusId = data.CustomerId.Value;
                        var company = customerRepository.GetById(cus.Id);
                        company.Address = cus.Address;
                        company.Mobile = cus.Mobile;
                        company.Name = cus.Name;
                        company.Email = cus.Email;
                        company.Notes = cus.Notes;
                        customerRepository.Update(company);
                    }


                }
                bool isfirstPrepaid = false;
                foreach (var item in data.RoomIds)
                {
                    if (isfirstPrepaid) data.Prepaid = 0;

                    data.CheckInTime = data.CheckInDate.HasValue ? data.CheckInDate.Value.TimeOfDay : data.CheckInTime;
                    data.CheckOutTime = data.CheckOutDate.HasValue ? data.CheckOutDate.Value.TimeOfDay : data.CheckOutTime;
                    data.RoomId = item.RoomId;

                    if (data.CheckInDate.Value.Date == DateTime.Now.Date)
                    {
                        var room = roomRepository.GetById(item.RoomId);
                        room.RoomStatus = RoomStatus.Active;
                        roomRepository.Update(room);
                        data.OrderStatus = (int)OrderStatus.CheckIn;
                    }
                    else
                        data.OrderStatus = (int)OrderStatus.Booking;

                    if (item.ConfigPriceId > 0)
                    {
                        var configPrice = _context.ConfigPrice.Where(a => a.Id == item.ConfigPriceId).FirstOrDefault();
                        data.Price = configPrice.PriceByDay;
                        data.ConfigPriceId = item.ConfigPriceId;
                    }
                    else
                    {
                        var rl = (from c in _context.ConfigPrice
                                  join b in _context.RoomClasses on c.RoomClassId equals b.Id
                                  join a in _context.Rooms on b.Id equals a.RoomClassId
                                  where a.Id == item.RoomId
                                  where c.IsDefault == true
                                  select new
                                  {
                                      Id = c.Id,
                                      Price = c.PriceByDay
                                  }).FirstOrDefault();

                        data.Price = rl.Price;
                        data.ConfigPriceId = rl.Id;

                    }


                    var order = AutoMapper.Mapper.Map<Order>(data);

                    order.CustomerId = cusId;
                    order.CreatedDate = DateTime.Now;
                    order.UpdatedDate = DateTime.Now;
                    var rowcom = new OrderCustomer() { Order = order };
                    if (com != null)
                    {
                        //order.Company = com;
                        rowcom.Customer = com;
                    }
                    else
                    {
                        order.CompanyId = data.Company.Id;
                        rowcom.CustomerId = data.Company.Id;
                    }

                    ordercustomerRepository.Add(rowcom);

                    if (cusId > 0)
                    {
                        var row = new OrderCustomer() { Order = order, CustomerId = cusId };
                        ordercustomerRepository.Add(row);
                    }
                    if (data.Prepaid > 0) isfirstPrepaid = true;
                    orderRepository.Add(order);

                    unitOfWork.Commit();

                }
                transaction.Complete();
            }


            return result;
        }

        public OrderRowModel GetOrderForEdit(int orderId)
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var order = (from a in _context.Orders
                         where a.HotelId == hotelId
                         where a.Id == orderId
                         select new OrderRowModel
                         {
                             Id = a.Id,
                             RoomId = a.RoomId,
                             RoomName = (from x in _context.Rooms where x.Id == a.RoomId select x.Name).FirstOrDefault(),
                             CheckInDate = a.CheckInDate.Value,
                             CheckOutDate = a.CheckOutDate.Value,
                             CheckInTime = a.CheckInTime,
                             CheckOutTime = a.CheckOutTime,
                             PassportId=a.PassportId,
                             Price = a.Price,
                             Notes = a.Notes,
                             ConfigPriceId = a.ConfigPriceId,
                             Customers = (from c in _context.OrderCustomer
                                          join d in _context.Customers on c.CustomerId equals d.Id
                                          where c.OrderId == a.Id && d.HotelId == hotelId && d.CustomerType == CustomerType.Customer
                                          select new CustomerRowModel
                                          {
                                              Address = d.Address,
                                              PassportAgency = d.PassportAgency,
                                              PassportCreatedDate = d.PassportCreatedDate.Value,
                                              PassportId = d.PassportId,
                                              Email = d.Email,
                                              Name = d.Name,
                                              Mobile = d.Mobile,
                                              Notes = d.Notes,
                                              Id = d.Id
                                          }).ToList(),
                             Services = (from e in _context.OrderService
                                         join f in _context.Widgets on e.ServiceId equals f.Id
                                         where e.OrderId == orderId
                                         where f.HotelId == hotelId
                                         select new ServiceRowModel
                                         {
                                             Id = f.Id,
                                             Name = f.Name,
                                             Price = f.Price,
                                             Quantity = e.Quantity
                                         }).ToList(),
                             Surcharge = a.Surcharge,
                             Prepaid = a.Prepaid,
                             OrderStatus = a.OrderStatus
                         }).FirstOrDefault();
            return order;

        }
        public OrderRowForCheckoutModel GetOrderForCheckOut(RequestCheckOutModel request)
        {
            var m_orderRepository = unitOfWork.Repository<Order>().GetQueryable();
           
            var row = GetOrderRowCheckOut(request, m_orderRepository);

            StringBuilder sb = new StringBuilder();
            if (row.TimeUseds != null)
            {
                foreach (var item in row.TimeUseds)
                {
                    sb.AppendLine(item.Description);
                }
            }
            var itemDetail = new OrderDetailDTO()
            {
                Quantity = 1,
                Title = sb.ToString(),
                Note = "Tiền phòng " + row.RoomName,
                Price = row.RoomAmount,
                SubAmount = row.RoomAmount,
                DetailTypeId = (int)CategoryInvoice.Room

            };
            row.OrderDetails.Add(itemDetail);

            if (row.OrderAttachments != null)
            {
                foreach (var item in row.OrderAttachments)
                {
                    var itemAttach = new OrderDetailDTO()
                    {
                        Quantity = 1,
                        Title = item.Note,
                        Note = "Hóa đơn ký gửi thanh toán",
                        Price = item.TotalAmount,
                        SubAmount = item.TotalAmount,
                        DetailTypeId = (int)CategoryInvoice.Room

                    };
                    row.OrderDetails.Add(itemAttach);
                }
            }


            return row;
        }


        public OrderRowForCheckoutModel ChangCalculatorMode(RequestCheckOutModel request)
        {
            if (!request.mode.HasValue)
            {
                base.AddError("Dữ liệu không hợp lệ");
                return null;
            }
            try
            {
                var repo = unitOfWork.Repository<Order>();
                var order = repo.GetQueryable().Where(a => a.HotelId == request.hotelId && a.Id == request.orderId).FirstOrDefault();
                if (order == null)
                {
                    base.AddError("Dữ liệu không tồn tại");
                    return null;
                }

                order.CaculatorMode = (CaculatorMode)request.mode.Value;
                repo.Update(order);
                unitOfWork.Commit();

             return   GetOrderForCheckOut(request);
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        public OrderRowForCheckoutModel GetOrderRowCheckOut(RequestCheckOutModel request, IQueryable<Order> m_orderRepository)
        {
           
            var m_order = m_orderRepository
                .Where(m => m.HotelId == request.hotelId && m.Id == request.orderId)
                .Select(a => new OrderRowForCheckoutModel
            {
                Id = a.Id,
                RoomId = a.RoomId,
                HotelId = a.HotelId,
                CaculatorMode = a.CaculatorMode,
                ConfigPriceId = a.ConfigPriceId,
                CustomerName = a.CustomerName,
                PassportId=a.PassportId,
                CustomerId = a.CustomerId,
                RoomName =a.Room.Name,
                CheckInDate = a.CheckInDate.Value,
                CheckOutDate = a.CheckOutDate,
                CheckInTime = a.CheckInTime.Value,
                Price = a.Price,
                RoomClassName =a.Room.RoomClass.Name,
                Services =a.OrderServices.Select(b=> new ServiceRowModel
                {
                    Id = b.Service.Id,
                    Name = b.Service.Name,
                    Price = b.Service.PricePaid,
                    Quantity = b.Quantity
                }).ToList(),
                Customers =a.OrderCustomers.Select(c=> new CustomerRowModel
                {
                    Address = c.Customer.Address,
                    PassportId = c.Customer.PassportId,
                    Name = c.Customer.Name,
                    Id = c.Customer.Id
                }).ToList(),
                OrderDetails = a.OrderDetails.Where(h => !h.IsDeleted).Select(x => new OrderDetailDTO()
                {
                    OrderId = a.Id,
                    RelatedId = x.RelatedId,
                    SubAmount = x.SubAmount,
                    CreatedDate = x.CreatedDate,
                    DetailTypeId = x.DetailTypeId,
                    Id = x.Id,
                    Note = x.Note,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    ShiftId = x.ShiftId,
                    Title = x.Title,
                    //UpdatedDate=x.UpdatedDate
                }).ToList(),
                Surcharge = a.Surcharge,
                Prepaid = a.Prepaid,
                Deductible = a.Deductible,
                OrderStatus = a.OrderStatus,
                Cashed = a.Cashed,
                Notes = a.Notes,
                PaymentMethod = (PaymentMethod)a.PaymentMethod

            }).FirstOrDefault();
            
            var config =unitOfWork.Repository<HotelConfig>().GetQueryable().Where(a=>a.HotelId== request.hotelId).FirstOrDefault();
            //order.TimeUseds = CaculatorTimeUsed(order, config, mode);

            List<TimeUsed> TimeUseds = new List<TimeUsed>();
            m_order.isByNight = request.isByNight;
            // kiêm tra có tính giờ qua đêm hay không?
            bool isOverNight = IsOverNight(m_order.CheckInTime, config.StartOverNight, config.EndOverNight);
            // lấy thông tin cấu hình giá
            var configPrice = GetConfigPrice(m_order.ConfigPriceId);


            m_order.Price = isOverNight ? configPrice.PriceByNight : configPrice.PriceByDay;
            if (!request.mode.HasValue)
                request.mode = (int)m_order.CaculatorMode;
            if(request.mode==0)
                request.mode = (int)CaculatorMode.ByHour;
            DateTime timeLine = m_order.CheckInDate;

            if (!m_order.CheckOutDate.HasValue)
            {
                m_order.CheckOutTime = DateTime.Now.TimeOfDay;
                m_order.CheckOutDate = DateTime.Now;
            }
         


            var FirstDay = CaculatorFirstDay(m_order, config, request.mode,ref timeLine);
            var MainsDay = CaculatorMainsDay(m_order, config, request.mode, ref timeLine);
            var OverDay = CaculatorOverDay(m_order, config, request.mode, ref timeLine);
            //var EarlyDay = CaculatorEarlyDay(order, config, mode);
            var OverCustomer = CaculatorOverCustomer(m_order, config);

            if (FirstDay != null) TimeUseds.AddRange(FirstDay);
            if (MainsDay != null) TimeUseds.Add(MainsDay);
            if (OverDay != null) TimeUseds.Add(OverDay);
           // if (EarlyDay != null) TimeUseds.Add(EarlyDay);
            if (OverCustomer != null) TimeUseds.Add(OverCustomer);

            m_order.TimeUseds = TimeUseds;


            m_order.OrderAttachments = GetOrderAttachment(request.orderId, m_orderRepository);

            OverWriteAmount(m_order);

            return m_order;

        }
        private void OverWriteAmount(OrderRowForCheckoutModel orderRow)
        {
            if (orderRow.OrderDetails != null)
            {

                orderRow.MiniBars = orderRow.OrderDetails.Where(a => a.DetailTypeId == (int)CategoryInvoice.Service).ToList();
                orderRow.MiniBarAmount = orderRow.MiniBars.Sum(a => a.SubAmount);

                orderRow.Surcharges = orderRow.OrderDetails.Where(a => a.DetailTypeId == (int)CategoryInvoice.Surcharge).ToList();
                orderRow.SurchargeAmount = orderRow.Surcharges.Sum(a => a.SubAmount);

                orderRow.Prepaids = orderRow.OrderDetails.Where(a => a.DetailTypeId == (int)CategoryInvoice.Prepaid).ToList();
                orderRow.PrepaidAmount = orderRow.Prepaids.Sum(a => a.SubAmount);

                orderRow.Deductibles = orderRow.OrderDetails.Where(a => a.DetailTypeId == (int)CategoryInvoice.Deductible).ToList();
                orderRow.DeductibleAmount = orderRow.Deductibles.Sum(a => a.SubAmount);
            }

            if (orderRow.TimeUseds != null)
            {
                orderRow.RoomAmount = orderRow.TimeUseds.Sum(a => a.SumAmount);
            }
            if (orderRow.OrderAttachments != null)
            {
                orderRow.AttachmentAmount = orderRow.OrderAttachments.Sum(a => a.TotalAmount);
            }
            orderRow.SubAmount = orderRow.MiniBarAmount + orderRow.RoomAmount + orderRow.SurchargeAmount + orderRow.AttachmentAmount;
            orderRow.TotalAmount = orderRow.SubAmount - orderRow.DeductibleAmount;
            orderRow.Cashed = orderRow.TotalAmount - orderRow.PrepaidAmount;

        }


        public List<OrderDetailDTO> AddOrderDetail(OrderDetailDTO data)
        {
            try
            {
                var orderDetailRepo = unitOfWork.Repository<OrderDetail>();
                if (data.RelatedId.HasValue)
                {
                    var existOrder = orderDetailRepo.GetQueryable()
                        .Where(a => a.RelatedId == data.RelatedId && !a.IsDeleted && a.OrderId==data.OrderId).FirstOrDefault();
                    if (existOrder != null)
                    {
                        existOrder.Quantity += data.Quantity;
                        existOrder.SubAmount = existOrder.Quantity * existOrder.Price;
                        existOrder.UpdatedDate = DateTime.Now;
                        orderDetailRepo.Update(existOrder);
                    }else
                    {
                        var orderDetail = new OrderDetail()
                        {
                            OrderId = data.OrderId,
                            Price = data.Price,
                            SubAmount = data.Price * data.Quantity,
                            Quantity = data.Quantity,
                            CreatedDate = DateTime.Now,
                            DetailTypeId = data.DetailTypeId,
                            RelatedId = data.RelatedId,
                            ShiftId = data.ShiftId,
                            Title = data.Title,

                        };
                        orderDetailRepo.Add(orderDetail);
                    }
                    unitOfWork.Commit();
                    
                    return null;
                }

                var orderDetail1 = new OrderDetail()
                {
                    OrderId = data.OrderId,
                    Price = data.Price,
                    SubAmount = data.Price * data.Quantity,
                    Quantity = data.Quantity,
                    CreatedDate = DateTime.Now,
                    DetailTypeId = data.DetailTypeId,
                    RelatedId = data.RelatedId,
                    ShiftId = data.ShiftId,
                    Title = data.Title,

                };
                orderDetailRepo.Add(orderDetail1);

                unitOfWork.Commit();
              
                return null;
            }
            catch (Exception ex)
            {
                base.AddError("Lỗi khi cập nhật dữ liệu");
               
                return null;

            }



        }

      
        public bool DeleteOrderDetail(OrderDetailDTO data)
        {
            try
            {
                if (data.Id <= 0)
                {
                    base.AddError("Dữ liệu không hợp lệ");
                    return false;
                }
                var orderDetailRepo = unitOfWork.Repository<OrderDetail>();

                var orderDetail = orderDetailRepo.GetQueryable().Where(a => a.Id == data.Id).FirstOrDefault();
                if (orderDetail == null)
                {
                    base.AddError("Dữ liệu không hợp lệ");
                    return false;
                }
                orderDetail.IsDeleted = true;
                orderDetailRepo.Update(orderDetail);
                unitOfWork.Commit();
                return true;
                //var result = GetOrderDetailsBy(data.DetailTypeId);
                //return result;
            }
            catch (Exception ex)
            {
                base.AddError("Lỗi khi cập nhật dữ liệu");
               
                return false;

            }
        }
        public List<RoomAttributeViewModel> GetAttributeConfig(Additional modeAdd, int hotelId, int roomclassId)
        {
            List<RoomAttributeViewModel> result = new List<RoomAttributeViewModel>();
            var byday = (from a in _context.ConfigPrice
                         join b in _context.RoomExtend on a.Id equals b.ConfigPriceId
                         where a.Id == roomclassId && b.Additional == modeAdd
                         select new RoomAttributeViewModel
                         {
                             Additional = b.Additional,
                             Key = b.Key,
                             Value = b.Value
                         }).ToList();
            result = byday;


            return result;
        }


        private List<TimeUsed> CaculatorUseConfigDay(OrderRowForCheckoutModel order, HotelConfig config)
        {
            List<TimeUsed> result = new List<TimeUsed>();
            var startDate = order.CheckInDate;
            DateTime endDate = DateTime.Now;

            var isOverTime = endDate.Hour > config.TimeCheckOut;

            TimeSpan diff = endDate - startDate;
            int days = diff.Days;
            int dayInConfig = 0;
            var configTime = (from a in _context.RoomExtend
                              where a.ConfigPriceId == order.ConfigPriceId
                              select a).ToList();

            if (configTime.Any())
            {
                for (var i = 0; i < days; i++)
                {
                    var CheckDate = startDate.AddDays(i);
                    foreach (var item in configTime)
                    {
                        if (CheckDate.DayOfWeek == (DayOfWeek)item.Key)
                        {
                            dayInConfig += 1;
                            TimeUsed row = new TimeUsed()
                            {
                                SumAmount = item.Value,
                                Description = string.Format("1 {0}", CommonUtil.ConvertDayOfWeekVN((DayOfWeek)item.Key)),
                                UnitUsed = 1
                            };

                            result.Add(row);
                        }
                    }

                }
            }


            int dayUse = isOverTime ? (days - dayInConfig) + 1 : (days - dayInConfig);

            TimeUsed data = new TimeUsed()
            {
                SumAmount = order.Price * dayUse,
                Description = string.Format("{0} ngày ({1} - {2})"
                                            , dayUse
                                            , string.Format("{0}:{1} {2}",
                                                         order.CheckInTime.Hours < 10 ? "0" + order.CheckInTime.Hours : order.CheckInTime.Hours.ToString(),
                                                         order.CheckInTime.Minutes < 10 ? "0" + order.CheckInTime.Minutes : order.CheckInTime.Minutes.ToString(),
                                                         order.CheckInDate.ToStringDateVN())
                                     , string.Format("{0}:{1} {2}",
                                        config.TimeCheckOut,
                                          "00",
                                          isOverTime ? endDate.AddDays(1).ToStringDateVN() : endDate.ToStringDateVN())),
                UnitUsed = dayUse
            };
            result.Add(data);



            return result;
        }


        private DateTime NextDate(DateTime prev,int timeCheckout)
        {
            return new DateTime(prev.Year
                                              , prev.Month
                                              , prev.Day
                                              , timeCheckout, 00, 00);
        }

        private List<TimeUsed> CaculatorFirstDay(OrderRowForCheckoutModel order, HotelConfig config, int? modecacu,ref DateTime timeLine)
        {
            if (order == null) { return null; }

            List<TimeUsed> result = new List<TimeUsed>();
            var dayuse = order.CheckOutDate.Value - timeLine;
            var today = order.CheckOutDate.Value;

            // kiêm tra có tính giờ qua đêm hay không?
            bool isOverNight = IsOverNight(order.CheckInTime, config.StartOverNight, config.EndOverNight);
            // modecacu = isOverNight ?(int) CaculatorMode.ByNight : modecacu;
            bool isEarlyCheckin = order.CheckInTime.Hours < config.StartOverNight;

            var roomclass = order.ConfigPriceId;

            // lấy thông tin cấu hình giá
            var configPrice = GetConfigPrice(order.ConfigPriceId);
            if (configPrice == null) { return null; }
            var row = new TimeUsed();
            DateTime limitDate = DateTime.Now;
            string dem = isOverNight ? "đêm" : "ngày";
            decimal valueCalcu = 0;
            switch (modecacu)
            {
                
                case (int)CaculatorMode.ByNight:

                    byNight:
                    order.CaculatorMode = CaculatorMode.ByNight;
                    // check xem co cau hinh phu thu check in som theo qua dem khong?
                    var configEarly = GetAttributeConfig(Additional.CheckinBynight, order.HotelId, roomclass);
                    if (configEarly.Any() && !isOverNight)
                    {
                        int start = config.StartOverNight- configEarly.Last().Key ;
                        isOverNight = order.CheckInTime >= CommonUtil.ToTimeSpan(start);
                    }
                    if (!isOverNight) goto byday;                   
                   

                    if (order.CheckInTime >=CommonUtil.ToTimeSpan( config.StartOverNight ))
                    {
                       
                        string destime = string.Format(" {0} "+ dem+" ( {1} - {2} )",
                            1,
                             string.Format("{0}:{1} {2}",
                                     order.CheckInTime.Hours < 10 ? "0" + order.CheckInTime.Hours : order.CheckInTime.Hours.ToString(),
                                       order.CheckInTime.Minutes < 10 ? "0" + order.CheckInTime.Minutes : order.CheckInTime.Minutes.ToString(),
                                        order.CheckInDate.ToStringDateVN()),
                             string.Format("{0}:{1} {2}",
                                    config.TimeCheckOut,
                                      "00",
                                      order.CheckInDate.AddDays(1).ToStringDateVN()));


                        timeLine = NextDate(order.CheckInDate.AddDays(1), config.TimeCheckOut);
                        row = new TimeUsed();
                        row.UnitUsed = 1;
                        row.SumAmount = isOverNight ? configPrice.PriceByNight : configPrice.PriceByDay;
                        row.Description = destime;
                        result.Add(row);
                    }
                    else if(order.CheckInTime <= CommonUtil.ToTimeSpan(config.EndOverNight))
                    {
                    
                        string destime = string.Format(" {0} " + dem + " ( {1} - {2} )",
                            1,
                             string.Format("{0}:{1} {2}",
                                     order.CheckInTime.Hours < 10 ? "0" + order.CheckInTime.Hours : order.CheckInTime.Hours.ToString(),
                                       order.CheckInTime.Minutes < 10 ? "0" + order.CheckInTime.Minutes : order.CheckInTime.Minutes.ToString(),
                                        order.CheckInDate.ToStringDateVN()),
                             string.Format("{0}:{1} {2}",
                                    config.TimeCheckOut,
                                      "00",
                                      order.CheckInDate.ToStringDateVN()));
                        timeLine = NextDate(order.CheckInDate, config.TimeCheckOut);

                        row = new TimeUsed();
                        row.UnitUsed = 1;
                        row.SumAmount = isOverNight ? configPrice.PriceByNight : configPrice.PriceByDay;
                        row.Description = destime;
                        result.Add(row);
                    }
                    else 
                    {
                        if (configEarly.Any())
                        {
                            int m_hourEarly = config.StartOverNight - order.CheckInTime.Hours;

                            if (m_hourEarly <= configEarly.Last().Key && !order.isByNight)
                            {
                                foreach (var item in configEarly)
                                {
                                    if (m_hourEarly <= item.Key) { valueCalcu = item.Value; break; }
                                }

                                string destimeHour = string.Format("Sớm {0} giờ ( {1} - {2} )",
                                  m_hourEarly,
                                  string.Format("{0}:{1}",
                                         order.CheckInTime.Hours < 10 ? "0" + order.CheckInTime.Hours : order.CheckInTime.Hours.ToString(),
                                              order.CheckInTime.Minutes < 10 ? "0" + order.CheckInTime.Minutes : order.CheckInTime.Minutes.ToString()),
                                  string.Format("{0}:{1}",
                                        config.StartOverNight < 10 ? "0" + config.StartOverNight : config.StartOverNight.ToString(),
                                         "00"));
                                row = new TimeUsed();
                                row.UnitUsed = 1;
                                row.SumAmount = valueCalcu;
                                row.Description = destimeHour;
                                result.Add(row);

                                string destime = string.Format(" {0} ngày ( {1} - {2} )",
                            1,
                             string.Format("{0}:{1} {2}",
                                     config.StartOverNight < 10 ? "0" + config.StartOverNight : config.StartOverNight.ToString(),
                                       "00",
                                        order.CheckInDate.ToStringDateVN()),
                             string.Format("{0}:{1} {2}",
                                    config.TimeCheckOut,
                                      "00",
                                      order.CheckInDate.AddDays(1).ToStringDateVN()));
                                //order.CheckOutDate = new DateTime(order.CheckInDate.Year, order.CheckInDate.Month, order.CheckInDate.AddDays(1).Day, config.TimeCheckOut, 00, 00);

                                timeLine = NextDate(order.CheckInDate.AddDays(1), config.TimeCheckOut);
                                row = new TimeUsed();
                                row.UnitUsed = 1;
                                row.SumAmount = configPrice.PriceByNight;
                                row.Description = destime;
                                result.Add(row);

                            }
                            else
                            {



                                string destime2 = string.Format(" {0}  " + dem + " ( {1} - {2} )",
                      1,
                       string.Format("{0}:{1} {2}",
                               order.CheckInTime.Hours < 10 ? "0" + order.CheckInTime.Hours : order.CheckInTime.Hours.ToString(),
                                 order.CheckInTime.Minutes < 10 ? "0" + order.CheckInTime.Minutes : order.CheckInTime.Minutes.ToString(),
                                  order.CheckInDate.ToStringDateVN()),
                       string.Format("{0}:{1} {2}",
                              config.TimeCheckOut,
                                "00",
                                order.CheckInDate.AddDays(1).ToStringDateVN()));

                                timeLine = NextDate(order.CheckInDate.AddDays(1), config.TimeCheckOut);
                                row = new TimeUsed();
                                row.UnitUsed = 1;
                                row.SumAmount = configPrice.PriceByDay;
                                row.Description = destime2;
                                result.Add(row);




                            }
                        }

                    }
                    break;
                case (int)CaculatorMode.ByDay:
                    byday:
                    order.CaculatorMode = CaculatorMode.ByDay;
                    if (isOverNight) goto byNight;
                    bool isCheckinEarly = false;
                    var configEarlyDay = GetAttributeConfig(Additional.CheckinByDay, order.HotelId, roomclass);
                    if (configEarlyDay.Any() && order.CheckInTime < CommonUtil.ToTimeSpan(config.TimeCheckIn))
                    {
                        // phu thu checkin sớm
                        int m_hourEarly = config.TimeCheckIn - order.CheckInTime.Hours;

                        if (m_hourEarly <= configEarlyDay.Last().Key)
                        {
                            isCheckinEarly = true;// danh dấu phụ thu check in sớm
                            foreach (var item in configEarlyDay)
                            {
                                if (m_hourEarly <= item.Key) { valueCalcu = item.Value; break; }
                            }

                            string destimeHour = string.Format("Sớm {0} giờ ( {1} - {2} )",
                              m_hourEarly,
                              string.Format("{0}:{1}",
                                     order.CheckInTime.Hours < 10 ? "0" + order.CheckInTime.Hours : order.CheckInTime.Hours.ToString(),
                                          order.CheckInTime.Minutes < 10 ? "0" + order.CheckInTime.Minutes : order.CheckInTime.Minutes.ToString()),
                              string.Format("{0}:{1}",
                                    config.TimeCheckIn < 10 ? "0" + config.TimeCheckIn : config.TimeCheckIn.ToString(),
                                     "00"));
                            row = new TimeUsed();
                            row.UnitUsed = 1;
                            row.SumAmount = valueCalcu;
                            row.Description = destimeHour;
                            result.Add(row);
                        }
                       
                    }
                    string startTimestr =!isCheckinEarly? string.Format("{0}:{1} {2}",
                                     order.CheckInTime.Hours < 10 ? "0" + order.CheckInTime.Hours : order.CheckInTime.Hours.ToString(),
                                       order.CheckInTime.Minutes < 10 ? "0" + order.CheckInTime.Minutes : order.CheckInTime.Minutes.ToString(),
                                        order.CheckInDate.ToStringDateVN()):
                                        string.Format("{0}:{1} {2}",
                                    config.TimeCheckIn,
                                      "00",
                                        order.CheckInDate.ToStringDateVN())
                                        ;

                    string destime1 = string.Format(" {0} "+dem+" ( {1} - {2} )",
                            1,
                             startTimestr,
                             string.Format("{0}:{1} {2}",
                                    config.TimeCheckOut,
                                      "00",
                                      order.CheckInDate.AddDays(1).ToStringDateVN()));

                    timeLine = NextDate(order.CheckInDate.AddDays(1), config.TimeCheckOut);

                    row = new TimeUsed();
                    row.UnitUsed = 1;
                    row.SumAmount = isOverNight ? configPrice.PriceByNight : configPrice.PriceByDay;
                    row.Description = destime1;
                    result.Add(row);
                    break;
                case (int)CaculatorMode.ByHour:
                    order.CaculatorMode = CaculatorMode.ByHour;
                    if (order.CheckInTime >= CommonUtil.ToTimeSpan(0) && order.CheckInTime<=CommonUtil.ToTimeSpan(config.EndOverNight)) goto byNight;
                    if (dayuse.Days >= 1) goto byday;
                    int hourUse = 0;
                     valueCalcu = 0;
                    //làm tròn giờ
                    //  DateTime date = DateTimeAround(config.TimeRound);
                    //hourUse =(today - order.CheckInDate).Hours;
                    hourUse = dayuse.Minutes >= config.TimeRound ? dayuse.Hours + 1 : dayuse.Hours;
                    
                    var configAdd = GetAttributeConfig(Additional.PriceByHour, order.HotelId, roomclass);
                  
                    if (configAdd.Any())
                    {
                        
                        if (hourUse > configAdd.Last().Key)
                        {
                                goto byday;

                          //      string destime2 = string.Format(" {0} " + dem + " ( {1} - {2} )",
                          //1,
                          // string.Format("{0}:{1} {2}",
                          //         order.CheckInTime.Hours < 10 ? "0" + order.CheckInTime.Hours : order.CheckInTime.Hours.ToString(),
                          //           order.CheckInTime.Minutes < 10 ? "0" + order.CheckInTime.Minutes : order.CheckInTime.Minutes.ToString(),
                          //            order.CheckInDate.ToStringDateVN()),
                          // string.Format("{0}:{1} {2}",
                          //        config.TimeCheckOut,
                          //          "00",
                          //          order.CheckInDate.AddDays(1).ToStringDateVN()));

                          //  limitDate = order.CheckInDate.AddDays(1);
                          //  order.CheckOutDate = new DateTime(limitDate.Year
                          //                                    , limitDate.Month
                          //                                    , limitDate.Day
                          //                                    , config.TimeCheckOut, 00, 00);
                          //  row = new TimeUsed();
                          //  row.UnitUsed = 1;
                          //  row.SumAmount =isOverNight? configPrice.PriceByNight :configPrice.PriceByDay;
                          //  row.Description = destime2;
                          //  result.Add(row);
                        }
                        else
                        {
                            foreach (var item in configAdd)
                            {
                                if (hourUse <= item.Key) { valueCalcu = item.Value; break; }
                            }

                            string destimeHour = string.Format("{0} giờ ( {1} - {2} )",
                              hourUse,
                              string.Format("{0}:{1}",
                                     order.CheckInTime.Hours < 10 ? "0" + order.CheckInTime.Hours : order.CheckInTime.Hours.ToString(),
                                          order.CheckInTime.Minutes < 10 ? "0" + order.CheckInTime.Minutes : order.CheckInTime.Minutes.ToString()),
                              string.Format("{0}:{1}",
                                     today.Hour < 10 ? "0" + today.Hour : today.Hour.ToString(),
                                       today.Minute < 10 ? "0" + today.Minute : today.Minute.ToString()));

                          
                            timeLine = NextDate(order.CheckInDate.AddDays(1), config.TimeCheckOut);
                            row = new TimeUsed();
                            row.UnitUsed = 1;
                            row.SumAmount = valueCalcu;
                            row.Description = destimeHour;
                            result.Add(row);
                        }
                    }
                    else
                    {
                        string destimeall = string.Format(" {0} " + dem + " ( {1} - {2} )",
                           1,
                            string.Format("{0}:{1} {2}",
                                    order.CheckInTime.Hours < 10 ? "0" + order.CheckInTime.Hours : order.CheckInTime.Hours.ToString(),
                                      order.CheckInTime.Minutes < 10 ? "0" + order.CheckInTime.Minutes : order.CheckInTime.Minutes.ToString(),
                                       order.CheckInDate.ToStringDateVN()),
                            string.Format("{0}:{1} {2}",
                                   config.TimeCheckOut,
                                     "00",
                                     order.CheckInDate.AddDays(1).ToStringDateVN()));

                        timeLine = NextDate(order.CheckInDate.AddDays(1), config.TimeCheckOut);
                        row = new TimeUsed();
                        row.UnitUsed = 1;
                        row.SumAmount = isOverNight ? configPrice.PriceByNight : configPrice.PriceByDay;
                        row.Description = destimeall;
                        result.Add(row);
                    }
                    break;
            }



            return result;
        }
        private TimeUsed CaculatorMainsDay(OrderRowForCheckoutModel order, HotelConfig config, int? modecacu , ref DateTime timeLine)
        {
            if (order == null) { return null; }
            if (!order.CheckOutDate.HasValue) return null;
            TimeUsed result = new TimeUsed();
            var dayuse = order.CheckOutDate.Value - timeLine;
            var today = order.CheckOutDate.Value;
            if (dayuse.Days < 1)
                return null;
            
            // lấy thông tin cấu hình giá
            var configPrice = GetConfigPrice(order.ConfigPriceId);
            if (configPrice == null) { return null; }


            string destime = string.Format(" {0} ngày ( {1} - {2} )",
                                   dayuse.Days,
                                     string.Format("{0}:{1} {2}",
                                            config.TimeCheckOut,
                                                "00",
                                               timeLine.ToStringDateVN()),
                                     string.Format("{0}:{1} {2}",
                                            config.TimeCheckOut,
                                              "00",
                                             timeLine.AddDays(dayuse.Days).ToStringDateVN()));

            


          //  DateTime limitDate = order.CheckOutDate.Value.AddDays(dayuse.Days);
            timeLine = NextDate(timeLine.AddDays(dayuse.Days), config.TimeCheckOut);

            result.UnitUsed = dayuse.Days;
            result.SumAmount = dayuse.Days * configPrice.PriceByDay;
            result.Description = destime;

            return result;
        }
        private TimeUsed CaculatorOverDay(OrderRowForCheckoutModel order, HotelConfig config, int? modecacu , ref DateTime timeLine)
        {
            if (order == null) { return null; }
            if (!order.CheckOutDate.HasValue) return null;
            TimeUsed result = new TimeUsed();
            var dayuse =  order.CheckOutDate.Value - timeLine;
            if (!(order.CheckOutDate.Value>= timeLine)) return null;

            var today = order.CheckOutDate.Value;
           // kiem tra qua gio 
            bool isOverTime = IsOvertime(config.TimeCheckOut, order.CheckOutDate.Value);
            //
            // kiêm tra có tính giờ qua đêm hay không?
            bool isOverNight = IsOverNight(order.CheckInTime, config.StartOverNight, config.EndOverNight);
            
            
            // kiểm tra nếu qua đêm và không tính trễ sơm
            if (isOverNight && order.isByNight) return null;



            Additional mode = Additional.CheckoutByDay;

            // lấy thông tin cấu hình giá
            var configPrice = GetConfigPrice(order.ConfigPriceId);
            if (configPrice == null) { return null; }

            if ((today.Hour < config.TimeCheckOut)
                && ( order.CheckOutDate.Value.Day> timeLine.Day)
                && dayuse.Days <= 1
                )
            {
                string destime = string.Format(" {0} ngày ( {1} - {2} )",
                             1,
                              string.Format("{0}:{1} {2}",
                                     config.TimeCheckOut,
                                      "00",
                                         today.AddDays(-1).ToStringDateVN()),
                              string.Format("{0}:{1} {2}",
                                     config.TimeCheckOut,
                                       "00",
                                       today.ToStringDateVN()));
                result.UnitUsed = 1;
                result.SumAmount = configPrice.PriceByDay;
                result.Description = destime;
                return result;
            }
            
            if (isOverTime)
            {
                int hourUse = 0;
                decimal valueCalcu = 0;
                mode = isOverNight ? Additional.CheckoutBynight : Additional.CheckoutByDay;
                //làm tròn giờ
                //today = DateTimeAround(config.TimeRound);
                // tinh so gio qua
                hourUse = today.Hour - config.TimeCheckOut;
                hourUse = today.Minute >= config.TimeRound ? hourUse + 1 : hourUse;

                var configAdd1 = GetAttributeConfig(mode, order.HotelId, order.ConfigPriceId);
                if (configAdd1.Any())
                {
                    if (hourUse <= configAdd1.Last().Key)
                    {
                        foreach (var item in configAdd1)
                        {
                            if (hourUse <= item.Key) { valueCalcu = item.Value; break; }
                        }
                        string destime = string.Format("Quá {0} giờ ( {1} - {2} )",
                           hourUse,
                           string.Format("{0}:{1}",
                                  config.TimeCheckOut,
                                      "00"),
                           string.Format("{0}:{1}",
                                  today.Hour < 10 ? "0" + today.Hour : today.Hour.ToString(),
                                    today.Minute < 10 ? "0" + today.Minute : today.Minute.ToString()));
                        result.UnitUsed = hourUse;
                        result.SumAmount = valueCalcu;
                        result.Description = destime;
                        return result;

                    }
                    else
                    {
                        string destime = string.Format(" {0} ngày ( {1} - {2} )",
                              1,
                               string.Format("{0}:{1} {2}",
                                      config.TimeCheckOut,
                                       "00",
                                          today.ToStringDateVN()),
                               string.Format("{0}:{1} {2}",
                                      config.TimeCheckOut,
                                        "00",
                                        today.AddDays(1).ToStringDateVN()));
                        result.UnitUsed = 1;
                        result.SumAmount = configPrice.PriceByDay;
                        result.Description = destime;
                        return result;
                    }
                }
            }
            return null;



        }

        /// <summary>
        /// Làm tròn giờ dựa vào cấu hình
        /// </summary>
        /// <param name="timeAround"></param>
        /// <returns></returns>
        private DateTime DateTimeAround(int timeAround)
        {
            var today = DateTime.Now;
            // lam tron theo cau hinh
            if (today.Minute > timeAround)
            {
                // tinh so phut lam tron 
                var dateAfter = today.AddMinutes(61 - today.Minute);
                // neu lon hon ngay hien tai thi ko cong them
                if (dateAfter.Day <= today.Day)
                {
                    today = today.AddMinutes(61 - today.Minute);
                }

            }
            return today;
        }
        /// <summary>
        /// Kiểm tra quá giờ trả phòng
        /// </summary>
        /// <param name="configCheckout"></param>
        /// <returns></returns>
        private bool IsOvertime(int configCheckout,DateTime checkoutDate)
        {
            var today = checkoutDate;
           
            if (today.Hour > configCheckout)
                return true;
            if ((today.Hour == configCheckout) && today.Minute > 0)
                return true;
            return false;
        }
        /// <summary>
        /// Kiểm tra có tính giờ qua đêm hay không 
        /// </summary>
        /// <param name="checkinTime">gio check in</param>
        /// <param name="startOverNight">giờ bắt đầu tính qua đêm</param>
        /// <param name="endOverNight">giờ kết thúc tính qua đêm</param>
        /// <returns>qua đêm hoặc không</returns>
        private bool IsOverNight(TimeSpan checkinTime,int startOverNight, int endOverNight)
        {
          //
            if (checkinTime >=CommonUtil.ToTimeSpan(startOverNight) || checkinTime <= CommonUtil.ToTimeSpan(endOverNight))
                return true;
           
            return false;
        }

        /// <summary>
        /// Lấy thông tin cấu hình giá theo Id
        /// </summary>
        /// <param name="configId">Id cấu hình giá</param>
        /// <returns>ConfigPrice object</returns>
        private ConfigPrice GetConfigPrice(int configId)
        {
            var m_configPrice = unitOfWork.Repository<ConfigPrice>().Get(a => a.Id == configId);
            return m_configPrice;
        }

        private TimeUsed CaculatorEarlyDay(OrderRowForCheckoutModel order, HotelConfig config, int? modecacu)
        {
            if (order == null) { return null; }

            TimeUsed result = new TimeUsed();
            var dayuse = DateTime.Now - order.CheckInDate;
            var today = DateTime.Now;
            var isOverNight = order.CheckInTime.Hours > config.StartOverNight || order.CheckInTime.Hours < config.EndOverNight;
            var isEarly = isOverNight ? order.CheckInTime.Hours < config.StartOverNight : order.CheckInTime.Hours < config.TimeCheckIn;
            if (!isEarly)
                return null;

            if (dayuse.Days <= 1)
                return null;

            Additional mode = Additional.CheckinByDay;
            var roomclass = order.ConfigPriceId;

            var configPrice = (from a in _context.ConfigPrice
                               where a.Id == order.ConfigPriceId
                               select a).FirstOrDefault();
            if (configPrice == null) { return null; }

            int hourUse = 0;
            decimal valueCalcu = 0;
            mode = isOverNight ? Additional.CheckinBynight : Additional.CheckinByDay;
            hourUse = isOverNight ? order.CheckInTime.Hours - config.StartOverNight : config.TimeCheckIn - order.CheckInTime.Hours;
            var configAdd1 = GetAttributeConfig(mode, order.HotelId, roomclass);
            if (configAdd1.Any())
            {
                if (hourUse <= configAdd1.Last().Key && dayuse.Days >= 1)
                {
                    foreach (var item in configAdd1)
                    {
                        if (hourUse <= item.Key) { valueCalcu = item.Value; break; }
                    }
                    string destime = string.Format("Sớm {0} giờ ( {1} - {2} )",
                       hourUse,
                       string.Format("{0}:{1}",
                          order.CheckInTime.Hours,
                                  "00"),
                       string.Format("{0}:{1}",
                       isOverNight ? config.StartOverNight : config.TimeCheckIn,
                                "00"));
                    result.UnitUsed = hourUse;
                    result.SumAmount = valueCalcu;
                    result.Description = destime;

                }
                else return null;


            }
            else
            {
                string destime = string.Format(" {0} ngày ( {1} - {2} )",
                          1,
                           string.Format("{0}:{1} {2}",
                                   today.Hour < 10 ? "0" + today.Hour : today.Hour.ToString(),
                                    today.Minute < 10 ? "0" + today.Minute : today.Minute.ToString(),
                                      today.ToStringVN()),
                           string.Format("{0}:{1} {2}",
                                  config.TimeCheckIn,
                                    "00",
                                    today.AddDays(1).ToStringVN()));
                result.UnitUsed = 1;
                result.SumAmount = isOverNight ? configPrice.PriceByNight : configPrice.PriceByDay;
                result.Description = destime;
            }


            return result;
        }

        private TimeUsed CaculatorOverCustomer(OrderRowForCheckoutModel order, HotelConfig config)
        {
            if (order == null) { return null; }
            var roomclass = (from a in _context.Rooms
                             join b in _context.RoomClasses on a.RoomClassId equals b.Id
                             where a.Id == order.RoomId
                             select b).FirstOrDefault();



            var NumCustomer = _context.OrderCustomer.Count(a => a.OrderId == order.Id);
            decimal valueCalcu = 0;
            if (roomclass.NumCustomer < NumCustomer)
            {
                TimeUsed result = new TimeUsed();
                int numOver = NumCustomer - roomclass.NumCustomer;
                Additional mode = Additional.CustomerOver;
                var configAdd1 = GetAttributeConfig(mode, order.HotelId, order.ConfigPriceId);
                if (configAdd1.Any())
                {

                    foreach (var item in configAdd1)
                    {
                        if (NumCustomer <= item.Key) { valueCalcu = item.Value; break; }
                    }
                    string destime = string.Format("Quá {0} người",
                       numOver);
                    if (valueCalcu == 0)
                    {
                        int numOverEnd = numOver - configAdd1.Last().Key;
                        valueCalcu = configAdd1.Last().Value + (numOverEnd * config.OverCustomer);
                    }
                    result.UnitUsed = numOver;
                    result.SumAmount = valueCalcu;
                    result.Description = destime;
                    return result;
                }
                else
                {
                    string destime = string.Format("Quá {0} người",
                         numOver);
                    valueCalcu = numOver * config.OverCustomer;
                    result.UnitUsed = numOver;
                    result.SumAmount = valueCalcu;
                    result.Description = destime;
                    return result;

                }
            }

            return null;

        }

        public List<OrderRowCompany> GetOrderByCompany(int hotelId)
        {
            List<OrderRowCompany> result = new List<OrderRowCompany>();
            var customerRepository = unitOfWork.Repository<Customer>();
            var orderRepository = unitOfWork.Repository<Order>();

            var cus = customerRepository.GetMany(a => a.HotelId == hotelId && a.IsHasCheckin && a.CustomerType == CustomerType.Company).ToList();
            if (cus == null) return null;

            foreach (var item in cus)
            {
                List<OrderRowForCheckoutModel> OrderRowForCheckout = new List<OrderRowForCheckoutModel>();
                var orderCom = new OrderRowCompany() { CompanyId = item.Id, CompanyName = item.Name };
                var order = orderRepository.GetMany(a => a.CompanyId == item.Id && a.HotelId == hotelId && a.OrderStatus == (int)OrderStatus.CheckIn).Select(c => c.Id).ToList();
                foreach (var row in order)
                {
                    RequestCheckOutModel request = new RequestCheckOutModel()
                    {
                        hotelId= hotelId,
                        orderId=row,
                        mode = (int)CaculatorMode.ByDay
                    };
                    var data = GetOrderForCheckOut(request);
                    OrderRowForCheckout.Add(data);
                }
                orderCom.Orders = OrderRowForCheckout;
                result.Add(orderCom);
            }
            return result;
        }

        public List<OrderRowCompany> GetOrderBookingByCompany(OrderFilterModel filter, out int total)
        {
            filter.Page.currentPage--;
            List<OrderRowCompany> result = new List<OrderRowCompany>();
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var orderRepository = unitOfWork.Repository<Order>();
            var customerRepository = unitOfWork.Repository<Customer>();
            var CusIds = orderRepository.GetMany(a => a.HotelId == hotelId && a.OrderStatus == filter.OrderStatus).Select(c => c.CompanyId).Distinct().Skip(filter.Page.currentPage * filter.Page.pageSize).Take(filter.Page.pageSize).ToList();
            if (!CusIds.Any())
            {
                total = 0;
                return null;
            };
            total = CusIds.Count;
            foreach (var item in CusIds)
            {
                List<OrderRowForCheckoutModel> OrderRowForCheckout = new List<OrderRowForCheckoutModel>();
                var company = customerRepository.GetById(item.Value);
                var orderCom = new OrderRowCompany() { CompanyId = item.Value, CompanyName = company.Name };
                var order = orderRepository.GetMany(a => a.CompanyId == item.Value && a.HotelId == hotelId && a.OrderStatus == filter.OrderStatus).Select(c => c.Id).ToList();
                foreach (var row in order)
                {
                    RequestCheckOutModel request = new RequestCheckOutModel()
                    {
                        hotelId = hotelId,
                        orderId = row,
                        mode = (int)CaculatorMode.ByDay
                    };
                    var data = GetOrderForCheckOut(request);
                    OrderRowForCheckout.Add(data);
                }
                orderCom.Orders = OrderRowForCheckout;
                result.Add(orderCom);
            }
            return result;
        }



        public OrderRowForCompanyCheckOut CompanyCheckOut(List<int> OrderIds, int mode)
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            OrderRowForCompanyCheckOut result = new OrderRowForCompanyCheckOut();
            if (!OrderIds.Any()) return null;
            if (OrderIds.Count > 1)
            {
                RequestCheckOutModel request = new RequestCheckOutModel()
                {
                    hotelId = hotelId,
                    orderId = OrderIds[0],
                    mode = mode
                };
                result.OrderKey = GetOrderForCheckOut(request);
            }
            List<OrderRowAttachment> orderAttach = new List<OrderRowAttachment>();
            //for (int i = 1; i < OrderIds.Count; i++)
            //{
            //    var attach = GetOrderAttachment(OrderIds[i], hotelId, mode);
            //    if (attach != null) orderAttach.Add(attach);
            //}
            result.OrderAttach = orderAttach;
            return result;
        }

        private List<OrderRowAttachment> GetOrderAttachment(int orderId, IQueryable<Order> m_orderRepository)
        {

            var orderAttach = m_orderRepository.Where(a => a.ParentId == orderId && a.OrderStatus != (int)OrderStatus.Completed).Select(a => new {
                Id = a.Id,
                mode = a.CaculatorMode,
                hotelId = a.HotelId,
                
            }).ToList();
            if (!orderAttach.Any()) return null;
            List<OrderRowAttachment> result = new List<OrderRowAttachment>();
            foreach (var item in orderAttach)
            {
                var req = new RequestCheckOutModel()
                {
                    hotelId=item.hotelId,
                    orderId=item.Id,
                    mode=(int)item.mode,
                    
                };
                var attachOrder = GetOrderRowCheckOut(req, m_orderRepository);
                var attachItem = new OrderRowAttachment()
                {
                    OrderId = attachOrder.Id,
                    TotalAmount = attachOrder.TotalAmount - attachOrder.PrepaidAmount,
                    Note = attachOrder.Notes
                };
                result.Add(attachItem);
            }

            return result;
        }

     


    }
}
