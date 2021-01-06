using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Repositories
{
    public interface IUtilityReponsitory : IRepository<Utility>
    {

    }
    public class UtilityReponsitory : RepositoryBase<Utility>, IUtilityReponsitory
    {
        public UtilityReponsitory(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
