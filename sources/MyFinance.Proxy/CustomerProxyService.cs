using MyFinance.Core;
using MyFinance.Domain.BusinessModel;
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
    public interface ICustomerProxyService : IBusinessBase
    {
        List<InvoiceRowModel> GetInvoicesByCustomer(List<int> OrderIds);
        List<CustomerRowViewModel> GetListCustomer(CustomerSearchModel model, out int total);
        List<CustomerCheckinRowModel> GetListCustomerCheckIn(CustomerSearchModel model, out int total);
        List<CustomerRowModel> GetCustomerPassportId(string passportId);
        List<CustomerRowModel> GetCustomerByName(CustomerSearchModel model);
        CustomerRowModel GetCustomerById(int id);
        int InsertOrUpdateCustomer(CustomerRowModel model);
    }
    public class CustomerProxyService : BaseProxyService, ICustomerProxyService
    {
        public int InsertOrUpdateCustomer(CustomerRowModel model)
        {
            string url = "api/Customer/InsertOrUpdateCustomer";
            return PostStructService<int>(model, url);
        }
        public CustomerRowModel GetCustomerById(int id)
        {
            string url = "api/Customer/GetCustomerById";
            return PostService<CustomerRowModel>(new { Id = id }, url);
        }
        public List<CustomerRowModel> GetCustomerByName(CustomerSearchModel model)
        {
            string url = "api/Customer/GetCustomerByName";
            return PostService<List<CustomerRowModel>>(model, url);
        }
        public List<CustomerRowModel> GetCustomerPassportId(string passportId)
        {
            string url = "api/Customer/GetCustomerPassportId";
            return PostService<List<CustomerRowModel>>(new { PassportId = passportId }, url);
        }
        public List<CustomerCheckinRowModel> GetListCustomerCheckIn(CustomerSearchModel model, out int total)
        {
            string url = "api/Customer/GetListCustomerCheckIn";
            return PostOutTotalService<List<CustomerCheckinRowModel>>(model, url, out total);
        }
        public List<InvoiceRowModel> GetInvoicesByCustomer(List<int> OrderIds)
        {
            string url = "api/Customer/GetInvoicesByCustomer";
            return PostService<List<InvoiceRowModel>>(new { OrderIds = OrderIds }, url);
        }
        public List<CustomerRowViewModel> GetListCustomer(CustomerSearchModel model, out int total)
        {
            string url = "api/Customer/GetListCustomer";
            return PostOutTotalService<List<CustomerRowViewModel>>(model,url,out total);
        }
    }
}
