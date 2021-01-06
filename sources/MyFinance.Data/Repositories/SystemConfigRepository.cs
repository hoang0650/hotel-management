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
    public interface ISystemConfigRepository : IRepository<HotelConfig>
    {
        
    }
    public class SystemConfigRepository : RepositoryBase<HotelConfig>, ISystemConfigRepository
    {
        public SystemConfigRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
       

    }
   
}