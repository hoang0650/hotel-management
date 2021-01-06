using MyFinance.Business;
using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
using MyFinance.Extention;
using MyFinance.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.ApiService
{
    public interface ICustomerServices {
        Response<List<CustomerRowModel>> GetCustomerByName(CustomerSearchModel filter);
        Response<List<CustomerRowModel>> GetCustomerPassportId(string passportId);
        Response<DataPaging<List<CustomerRowViewModel>>> GetListCustomer(CustomerSearchModel filter);
        Response<List<InvoiceRowModel>> GetInvoicesByCustomer(List<int> OrderIds);
        Response<CustomerRowModel> GetCustomerById(int Id);

        Response<int> InsertOrUpdateCustomer(CustomerRowModel model);

        Response<DataPaging<List<CustomerCheckinRowModel>>> GetListCustomerCheckIn(CustomerSearchModel model);
    }
    public partial class TikasaService
    {
        public Response<List<CustomerRowModel>> GetCustomerByName(CustomerSearchModel filter)
        {

            List<CustomerRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ICustomerBusiness>().GetCustomerByName(filter);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<List<CustomerRowModel>> GetCustomerPassportId(string passportId)
        {

            List<CustomerRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ICustomerBusiness>().GetCustomerPassportId(passportId);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<DataPaging<List<CustomerRowViewModel>>> GetListCustomer(CustomerSearchModel filter)
        {
            List<CustomerRowViewModel> result = null;
            int total = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ICustomerBusiness>().GetListCustomer(filter, out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result, total));
        }


        public Response<List<InvoiceRowModel>> GetInvoicesByCustomer(List<int> OrderIds)
        {
            List<InvoiceRowModel> result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ICustomerBusiness>().GetInvoicesByCustomer(OrderIds);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<CustomerRowModel> GetCustomerById(int Id)
        {
            CustomerRowModel result = null;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ICustomerBusiness>().GetCustomerById(Id);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<int> InsertOrUpdateCustomer(CustomerRowModel model)
        {
            int result = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ICustomerBusiness>().InsertOrUpdateCustomer(model);
            });

            return BusinessProcess.Current.ToResponse(result);
        }

        public Response<DataPaging<List<CustomerCheckinRowModel>>> GetListCustomerCheckIn(CustomerSearchModel model)
        {
            List<CustomerCheckinRowModel> result = null;
            int total = 0;
            BusinessProcess.Current.Process(p =>
            {
                result = IoC.Get<ICustomerBusiness>().GetListCustomerCheckIn(model,out total);
            });

            return BusinessProcess.Current.ToResponse(DataPaging.Create(result,total));
        }


    }
}
