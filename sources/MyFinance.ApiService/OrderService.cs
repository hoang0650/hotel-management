using System.Collections.Generic;
using MyFinance.Domain.BusinessModel;
using MyFinance.Domain.Enum;
using MyFinance.Domain.Entities;
using MyFinance.Utils;
using MyFinance.Core;
using MyFinance.Extention;
using MyFinance.Business;
using System.ServiceModel;

namespace MyFinance.ApiService
{

    public partial class TikasaService 
    {

        public Response<int> AddOrder(OrderRowModel data)
        {
            int result =0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().AddOrder(data);
            });

            return BusinessProcess.Current.ToResponse(result);

        }

        public Response<bool> BookingOrder(OrderRowModel data)
        {

            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().BookingOrder(data);
            });

            return BusinessProcess.Current.ToResponse(result);

        }

       
        public  Response<DataPaging<List<OrderBookingRowModel>>> GetBookingOrders(InvoiceFilterModel filter)
        {

            List<OrderBookingRowModel> result = null;
              int total=0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().GetBookingOrders(filter,out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result,total));
        }

        public Response<bool> TranferBookingToCheckIn(int orderid)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().TranferBookingToCheckIn(orderid);
            });

            return BusinessProcess.Current.ToResponse(result);
           
        }

        public Response<bool> UpdateOrder(OrderRowModel data)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().UpdateOrder(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }



        public Response<bool> AddOrderMutil(OrderRowModel data)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().AddOrderMutil(data);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response< OrderRowModel> GetOrderForEdit(int orderId)
        {
            OrderRowModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().GetOrderForEdit( orderId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }


        public Response<OrderRowForCheckoutModel> GetOrderForCheckOut(RequestCheckOutModel request)
        {
            OrderRowForCheckoutModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().GetOrderForCheckOut(request);
            });

            return BusinessProcess.Current.ToResponse(result);

        }

     

        public Response< List<OrderRowCompany> >GetOrderByCompany(int hotelId)
        {
            List<OrderRowCompany> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().GetOrderByCompany(hotelId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<OrderRowForCompanyCheckOut> CompanyCheckOut(List<int> OrderIds, int mode)
        {
            OrderRowForCompanyCheckOut result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().CompanyCheckOut(OrderIds,mode);
            });

            return BusinessProcess.Current.ToResponse(result);
        }
        public Response<bool> ChangeRoomInOrder(ChangeRoomInOrderModel model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().ChangeRoomInOrder(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

         public Response<Folio> GetFolioCustomer(int companyId, int typeCustomer)
         {
             Folio result = null;
             BusinessProcess.Current.Process(p =>
             {
                 result = IoC.Get<IOrderBusiness>().GetFolioCustomer(companyId, typeCustomer);
             });

             return BusinessProcess.Current.ToResponse(result);
         }


         public Response<DataPaging<List<OrderRowCompany>>> GetOrderBookingByCompany(OrderFilterModel filter)
         {

             List<OrderRowCompany> result = null;
             int total = 0;
             BusinessProcess.Current.Process(p =>
             {
                 result = IoC.Get<IOrderBusiness>().GetOrderBookingByCompany(filter, out total);
             });

             return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
         }


         public Response<List<Country>> GetCountries()
         {
             List<Country> result = null;
             BusinessProcess.Current.Process(p =>
             {
                 result = IoC.Get<IOrderBusiness>().GetCountries();
             });

             return BusinessProcess.Current.ToResponse(result);
         }
        public Response<List<OrderDetailDTO>> AddOrderDetail(OrderDetailDTO model)
        {
            List<OrderDetailDTO> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().AddOrderDetail(model);
            });

            return BusinessProcess.Current.ToResponse(result);

        }
        public Response<bool> DeleteOrderDetail(OrderDetailDTO model)
        {
            bool result = false;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().DeleteOrderDetail(model);
            });

            return BusinessProcess.Current.ToResponse(result);

        }
        public Response<OrderRowForCheckoutModel> ChangCalculatorMode(RequestCheckOutModel request)
        {
            OrderRowForCheckoutModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<IOrderBusiness>().ChangCalculatorMode(request);
            });

            return BusinessProcess.Current.ToResponse(result);

        }
         
    }
    [ServiceContract]
    public interface IOrderService
    {
        Response<OrderRowForCheckoutModel> ChangCalculatorMode(RequestCheckOutModel request);
        Response<bool> DeleteOrderDetail(OrderDetailDTO model);
        Response<List<OrderDetailDTO>> AddOrderDetail(OrderDetailDTO model);
        Response<List<Country>> GetCountries();
        Response<DataPaging<List<OrderRowCompany>>> GetOrderBookingByCompany(OrderFilterModel filter);
        Response<Folio> GetFolioCustomer(int companyId, int typeCustomer);
        [OperationContract]
        Response<int> AddOrder(OrderRowModel data);
        Response<bool> AddOrderMutil(OrderRowModel data);
        Response<OrderRowModel> GetOrderForEdit( int orderId);
        [OperationContract]
        Response<bool> UpdateOrder(OrderRowModel data);
        Response<bool> BookingOrder(OrderRowModel data);
        [OperationContract]
        Response<OrderRowForCheckoutModel> GetOrderForCheckOut(RequestCheckOutModel request);
        Response<List<OrderRowCompany>> GetOrderByCompany(int hotelId);
        Response<OrderRowForCompanyCheckOut> CompanyCheckOut(List<int> OrderIds, int mode);
        Response<DataPaging<List<OrderBookingRowModel>>> GetBookingOrders(InvoiceFilterModel filter);
        Response<bool> TranferBookingToCheckIn(int orderid);
        [OperationContract]
        Response<bool> ChangeRoomInOrder(ChangeRoomInOrderModel model);
    }
}
