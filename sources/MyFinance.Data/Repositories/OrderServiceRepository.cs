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
    public interface IOrderServiceRepository : IRepository<OrderService>
    {
        
    }
    public class OrderServiceRepository : RepositoryBase<OrderService>, IOrderServiceRepository
    {
        public OrderServiceRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       

    }
   
}