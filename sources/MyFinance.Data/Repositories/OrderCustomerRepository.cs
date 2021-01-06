using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyFinance.Domain;
using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Enum;
using MyFinance.Domain.Entities;
namespace MyFinance.Data

{
    public interface IOrderCustomerRepository : IRepository<OrderCustomer>
    {
        
    }
    public class OrderCustomerRepository : RepositoryBase<OrderCustomer>, IOrderCustomerRepository
    {
        public OrderCustomerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

       
    }
   
}