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
    public interface ICustomerRepository : IRepository<Customer>
    {
        
    }
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       

    }
   
}