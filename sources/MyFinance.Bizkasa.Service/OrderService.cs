using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyFinance.Domain;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Domain.Entities;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Proxy;

namespace MyFinance.Bizkasa.Service
{
  
    public partial class TikasaService 
    {

        public Response<int> AddOrder(OrderRowModel data)
        {
            int result =0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().AddOrder(data);//IoC.Get<IOrderBusiness>().AddOrder(data);
            });

            return BusinessProcess.Current.ToResponse(result);

        }

        public Response<bool> BookingOrder(OrderRowModel data)
        {

            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().BookingOrder(data);
            });

            return BusinessProcess.Current.ToResponse(result);

        }

       
        public  Response<DataPaging<List<OrderBookingRowModel>>> GetBookingOrders(InvoiceFilterModel filter)
        {

            List<OrderBookingRowModel> result = null;
              int total=0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().GetBookingOrders(filter, out total);//IoC.Get<IOrderBusiness>().GetBookingOrders(filter,out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result,total));
        }

        public Response<bool> TranferBookingToCheckIn(int orderid)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().TranferBookingToCheckIn(orderid);
            });

            return BusinessProcess.Current.ToResponse(result);
           
        }

        public Response<bool> UpdateOrder(OrderRowModel data)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().UpdateOrder(data);//IoC.Get<IOrderBusiness>().UpdateOrder(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }



        public Response<bool> AddOrderMutil(OrderRowModel data)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().AddOrderMutil(data);//IoC.Get<IOrderBusiness>().AddOrderMutil(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response< OrderRowModel> GetOrderForEdit( int orderId)
        {
            OrderRowModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().GetOrderForEdit( orderId); //IoC.Get<IOrderBusiness>().GetOrderForEdit(hotelId, orderId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<OrderRowForCheckoutModel> GetOrderForCheckOut(int hotelId, int orderId,int mode)
        {
            OrderRowForCheckoutModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().GetOrderForCheckOut(hotelId, orderId, mode);//IoC.Get<IOrderBusiness>().GetOrderForCheckOut(hotelId, orderId, mode);
            });

            return BusinessProcess.Current.ToResponse(result);

        }

     

        public Response< List<OrderRowCompany> >GetOrderByCompany(int hotelId)
        {
            List<OrderRowCompany> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().GetOrderByCompany(hotelId);//IoC.Get<IOrderBusiness>().GetOrderByCompany(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<OrderRowForCompanyCheckOut> CompanyCheckOut(List<int> OrderIds, CaculatorMode mode)
        {
            OrderRowForCompanyCheckOut result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().CompanyCheckOut(OrderIds, mode);//IoC.Get<IOrderBusiness>().CompanyCheckOut(OrderIds,hotelId,mode);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> ChangeRoomInOrder(int orderid, int roomid, int configId)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderProxyService>().ChangeRoomInOrder(orderid, roomid, configId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

         public Response<Folio> GetFolioCustomer(int companyId, int typeCustomer)
         {
             Folio result = null;
             BusinessProcess.Current.Process(p =>
             {
                 result = IoC.Get<IOrderProxyService>().GetFolioCustomer(companyId, typeCustomer);
             });

             return BusinessProcess.Current.ToResponse(result);
         }
         public Response<DataPaging<List<OrderRowCompany>>> GetOrderBookingByCompany(OrderFilterModel data)
         {

             List<OrderRowCompany> result = null;
             int total = 0;
             BusinessProcess.Current.Process(p =>
             {
                 result = IoC.Get<IOrderProxyService>().GetOrderBookingByCompany(data, out total);//IoC.Get<IOrderBusiness>().GetBookingOrders(filter,out total);
             });

             return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
         }


         public Response<List<Country>> GetCountries()
         {
             List<Country> result = null;
             BusinessProcess.Current.Process(p =>
             {
                 result = IoC.Get<IOrderProxyService>().GetCountries();//IoC.Get<IOrderBusiness>().GetOrderByCompany(hotelId);
             });

             return BusinessProcess.Current.ToResponse(result);
         }

    }

    public interface IOrderService
    {
        Response<List<Country>> GetCountries();
        Response<DataPaging<List<OrderRowCompany>>> GetOrderBookingByCompany(OrderFilterModel data);
        Response<Folio> GetFolioCustomer(int companyId, int typeCustomer);
        Response<int> AddOrder(OrderRowModel data);
        Response<bool> AddOrderMutil(OrderRowModel data);
        Response<OrderRowModel> GetOrderForEdit(int orderId);
        Response<bool> UpdateOrder(OrderRowModel data);
        Response<bool> BookingOrder(OrderRowModel data);
        Response<OrderRowForCheckoutModel> GetOrderForCheckOut(int hotelId, int orderId, int mode);
        Response<List<OrderRowCompany>> GetOrderByCompany(int hotelId);
        Response<OrderRowForCompanyCheckOut> CompanyCheckOut(List<int> OrderIds, CaculatorMode mode);
        Response<DataPaging<List<OrderBookingRowModel>>> GetBookingOrders(InvoiceFilterModel filter);
        Response<bool> TranferBookingToCheckIn(int orderid);

        Response<bool> ChangeRoomInOrder(int orderid, int roomid, int configId);
    }
}
