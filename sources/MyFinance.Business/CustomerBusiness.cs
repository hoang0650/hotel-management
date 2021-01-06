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
    public interface ICustomerBusiness
    {
        int InsertOrUpdateCustomer(CustomerRowModel model);
        List<CustomerRowModel> GetCustomerByName(CustomerSearchModel filter);
        List<CustomerRowModel> GetCustomerPassportId(string passportId);
        List<CustomerRowViewModel> GetListCustomer(CustomerSearchModel model, out int total);
        List<InvoiceRowModel> GetInvoicesByCustomer(List<int> OrderIds);
        CustomerRowModel GetCustomerById(int id);
        List<CustomerCheckinRowModel> GetListCustomerCheckIn(CustomerSearchModel model, out int total);
    }
    public class CustomerBusiness : BusinessBase, ICustomerBusiness
    {
        private readonly MyFinanceContext _context;
        private readonly IUnitOfWork unitOfWork;
        public CustomerBusiness(
            IUnitOfWork unitOfWork
             , MyFinanceContext context
            )
        {
            this.unitOfWork = unitOfWork;
            this._context = context;
        }
        public int InsertOrUpdateCustomer(CustomerRowModel model)
        {
            var customerRepo = unitOfWork.Repository<Customer>();
            if (model.Id > 0)
            {
                var customer = customerRepo.GetById(model.Id);
                customer.Address = model.Address;
                customer.Email = model.Email;
                customer.Mobile = model.Mobile;
                customer.Notes = model.Notes;
                customer.Name = model.Name;
                
                customer.PassportAgency = model.PassportAgency;
                customer.National = model.National;
                //customer.PassportCreatedDate = model.PassportCreatedDate;
                customer.BirthDate = model.BirthDate.HasValue ? model.BirthDate : customer.BirthDate;
             
                customer.UpdatedDate = DateTime.Now;
                
                customerRepo.Update(customer);
                unitOfWork.Commit();
                return model.Id;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(model.PassportId))
                {
                    var isExist = customerRepo.GetMany(a => a.PassportId == model.PassportId && a.HotelId == model.HotelId).FirstOrDefault();
                    if (isExist != null)
                    {
                        isExist.Address = model.Address;
                        isExist.Email = model.Email;
                        isExist.Mobile = model.Mobile;
                        isExist.Notes = model.Notes;
                        isExist.National = model.National;
                        isExist.BirthDate = model.BirthDate.HasValue?model.BirthDate: isExist.BirthDate;
                        isExist.Name = model.Name;
                        isExist.PassportAgency = model.PassportAgency;
                        isExist.UpdatedDate = DateTime.Now;
                        customerRepo.Update(isExist);
                        unitOfWork.Commit();
                        return isExist.Id;
                    }
                    else
                    {
                        var row = AutoMapper.Mapper.Map<Customer>(model);
                        row.HotelId = WorkContext.BizKasaContext.HotelId;
                        row.CreatedDate = DateTime.Now;
                        row.CustomerType = CustomerType.Customer;
                        customerRepo.Add(row);
                        unitOfWork.Commit();
                        return row.Id;
                    }

                }
                else
                {
                    var row = AutoMapper.Mapper.Map<Customer>(model);
                    row.HotelId = WorkContext.BizKasaContext.HotelId;
                    row.CreatedDate = DateTime.Now;
                    row.CustomerType = CustomerType.Company;
                    customerRepo.Add(row);
                  
                    return row.Id;
                }
            }

        }

        public List<CustomerRowModel> GetCustomerByName(CustomerSearchModel filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Keyword)) return null;
            var customers = (from a in _context.Customers
                             where a.Name.Contains(filter.Keyword) && a.HotelId == WorkContext.BizKasaContext.HotelId && a.CustomerType==(CustomerType)filter.CustomerType
                             select new CustomerRowModel
                             {
                                 Address = a.Address,
                                 PassportAgency = a.PassportAgency,
                                 PassportId = a.PassportId,
                                 Email = a.Email,
                                 PassportCreatedDate = a.PassportCreatedDate,
                                 Name = a.Name,
                                 Mobile = a.Mobile,
                                 BirthDate=a.BirthDate,
                                 Id = a.Id,
                                 HotelId = a.HotelId
                             }).Take(20).ToList();
            return customers;
        }


        public List<CustomerRowModel> GetCustomerPassportId(string passportId)
        {
            if (string.IsNullOrWhiteSpace(passportId)) return null;
            var customers = (from a in _context.Customers
                             where a.PassportId.Contains(passportId) && a.HotelId == WorkContext.BizKasaContext.HotelId
                             select new CustomerRowModel
                             {
                                 Address = a.Address,
                                 PassportAgency = a.PassportAgency,
                                 PassportId = a.PassportId,
                                 Email = a.Email,
                                 PassportCreatedDate = a.PassportCreatedDate,
                                 Name = a.Name,
                                 Mobile = a.Mobile,
                                 BirthDate=a.BirthDate,
                                 Id = a.Id,
                                 HotelId = a.HotelId
                             }).Take(20).ToList();
            return customers;
        }

        public List<CustomerRowViewModel> GetListCustomer(CustomerSearchModel model, out int total)
        {
            model.Page.currentPage--;
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var query = unitOfWork.Repository<Customer>().GetQueryable().Where(a => a.HotelId == hotelId && a.PassportId.Length > 0);
          
            if(!string.IsNullOrWhiteSpace(model.Keyword))
            {
                query=query.Where(a => a.Name.Contains(model.Keyword) || a.Mobile.Contains(model.Keyword) || a.PassportId.Contains(model.Keyword));
            }
            var result = query.Select(b => new CustomerRowViewModel
            {
                Address = b.Address,
                Name = b.Name,
                Email = b.Email,
                Mobile = b.Mobile,
                Id = b.Id,
                PassportId = b.PassportId,
                BirthDate = b.BirthDate,
                CreatedDate = b.CreatedDate,
                //OrderIds=  b.OrderCustomers.Select(v=>v.OrderId).ToList(),
                //NumCheckIn =  b.OrderCustomers.Count(),
                //TotalAmount =  b.OrderCustomers.Sum(c=>c.Order.TotalAmount)

            }).OrderByDescending(a => a.Id).Skip(model.Page.currentPage * model.Page.pageSize).Take(model.Page.pageSize).ToList();

            total = query.Count();

            return result;
        }

        public CustomerRowModel GetCustomerById(int id)
        {
            var customer = unitOfWork.Repository<Customer>().GetById(id);
            if (customer == null) { AddError("Không tìm thấy khách hàng !"); return null; }
            var map = AutoMapper.Mapper.Map<CustomerRowModel>(customer);
            return map;
        }
        public List<InvoiceRowModel> GetInvoicesByCustomer(List<int> OrderIds)
        {
            if (OrderIds == null)
            {
                base.AddError("Danh sách hóa đơn rỗng !");
                return null;
            }
            int hoteid = WorkContext.BizKasaContext.HotelId;
            var invoices = _context.Invoices.Where(a => a.HotelId == hoteid && OrderIds.Contains(a.OrderId.Value))
                .Select(a => new InvoiceRowModel
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
                    InvoiceDetails = _context.InvoiceDetail.Where(d => d.InvoiceId == a.Id)
                        .Select(v => new InvoiceDetailRowModel
                        {
                            Descriptions = v.Descriptions,
                            Quantity = v.Quantity,
                            SubAmount = v.SubAmount,
                            CreatedDate = v.CreatedDate,
                            Notes = v.Notes,
                            Price = v.Price,
                            Id = v.Id,
                            UserUpdate = v.UserUpdate
                        }).ToList()

                }).ToList();




            return invoices;
        }


        public List<CustomerCheckinRowModel> GetListCustomerCheckIn(CustomerSearchModel model, out int total)
        {
            int hotelId = WorkContext.BizKasaContext.HotelId;
            var query = unitOfWork.Repository<Order>()
                .GetQueryable()
                .Where(a=>a.HotelId== hotelId 
                && a.OrderStatus == (int)OrderStatus.CheckIn
                && a.Room.RoomStatus==RoomStatus.Active)
                .Select(a => new CustomerCheckinRowModel
                {
                    Id = a.CustomerId.HasValue? a.Customer.Id:0,
                    CheckInDate =  a.CheckInDate,
                    CheckOutDate = a.CheckOutDate,

                    Name = a.CustomerId.HasValue?a.Customer.Name:a.CustomerName,
                    BirthDate= a.CustomerId.HasValue ? a.Customer.BirthDate :null,
                    RoomClassName = a.Room.RoomClass.Name,
                    RoomName = a.Room.Name,
                    PassportId = a.CustomerId.HasValue ? a.Customer.PassportId : string.Empty
                    
                }).ToList();

            total = query.Count();
            return query;
        }
    }
}
