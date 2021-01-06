using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Entities;
using MyFinance.Domain.Enum;
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
    public interface IOrderProxyService : IBusinessBase
    {
        OrderRowForCheckoutModel GetOrderForCheckOut(int hotelId, int orderId, int mode);
        bool UpdateOrder(OrderRowModel model);
        int AddOrder(OrderRowModel data);
        OrderRowModel GetOrderForEdit(int orderId);
        List<OrderBookingRowModel> GetBookingOrders(InvoiceFilterModel filter, out int total);
        List<OrderRowCompany> GetOrderByCompany(int hotelId);
        Folio GetFolioCustomer(int companyId, int typeCustomer);
        bool AddOrderMutil(OrderRowModel data);
        OrderRowForCompanyCheckOut CompanyCheckOut(List<int> OrderIds, CaculatorMode mode);
        bool BookingOrder(OrderRowModel data);
        bool TranferBookingToCheckIn(int orderid);
        bool ChangeRoomInOrder(int orderid, int roomid, int configId);
        List<OrderRowCompany> GetOrderBookingByCompany(OrderFilterModel data, out int total);
        List<Country> GetCountries();
    }

    public class OrderProxyService : BaseProxyService, IOrderProxyService
    {
        public List<Country> GetCountries()
        {
            string url = "api/Order/GetCountries";
            return GetDataService<List<Country>>(url);
        }

        public List<OrderRowCompany> GetOrderBookingByCompany(OrderFilterModel data, out int total)
        {
            string url = "api/Order/GetOrderBookingByCompany";
            return PostOutTotalService<List<OrderRowCompany>>(data, url, out total);
        }
        public bool ChangeRoomInOrder(int orderid, int roomid, int configId)
        {
            string url = "api/Order/ChangeRoomInOrder";
            return PostStructService<bool>(new { OrderId = orderid, RoomId = roomid, ConfigPriceId = configId }, url);
        }
        public bool TranferBookingToCheckIn(int orderid)
        {
            string url = "api/Order/TranferBookingToCheckIn";
            return PostStructService<bool>(new { OrderId = orderid }, url);
        }
        public bool BookingOrder(OrderRowModel data)
        {
            string url = "api/Order/BookingOrder";
            return PostStructService<bool>(data, url);
        }
        public OrderRowForCompanyCheckOut CompanyCheckOut(List<int> OrderIds, CaculatorMode mode)
        {
            string url = "api/Order/CompanyDoCheckOut";
            return PostService<OrderRowForCompanyCheckOut>(new { OrderIds = OrderIds, Mode = mode }, url);
        }
        public bool AddOrderMutil(OrderRowModel model)
        {
            string url = "api/Order/AddOrderMutil";
            return PostStructService<bool>(model, url);
        }
        public Folio GetFolioCustomer(int companyId, int typeCustomer)
        {
            string url = "api/Order/GetFolioCustomer";
            return PostService<Folio>(new {CompanyId=companyId,TypeCustomer=typeCustomer }, url);

        }


        public List<OrderRowCompany> GetOrderByCompany(int hotelId)
        {
            string url = "api/Order/GetOrderByCompany";
            return GetDataService<List<OrderRowCompany>>( url);
        }
        public List<OrderBookingRowModel> GetBookingOrders(InvoiceFilterModel filter, out int total)
        {
            string url = "api/Order/GetBookingOrders";
            return PostOutTotalService<List<OrderBookingRowModel>>(filter, url,out total);

        }
        public bool UpdateOrder(OrderRowModel model)
        {
            string url = "api/Order/UpdateOrder";
            return PostStructService<bool>(model, url);
            
        }


        public  int AddOrder(OrderRowModel model)
        {
            string url = "api/Order/AddOrder";
            return PostStructService<int>(model, url);
           
        }


        public OrderRowForCheckoutModel GetOrderForCheckOut(int hotelId, int orderId, int mode)
        {
            string url = "api/Order/GetOrderForCheckOut";
            return PostService<OrderRowForCheckoutModel>(new { OrderId = orderId, Mode = mode }, url);
       

        }

        public OrderRowModel GetOrderForEdit( int orderId)
        {

            string url = "api/Order/GetOrderForEdit";
            return PostService<OrderRowModel>(new { OrderId = orderId }, url);
         

        }
    }
}
