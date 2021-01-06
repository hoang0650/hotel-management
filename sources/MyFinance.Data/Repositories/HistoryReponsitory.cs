using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Repositories
{
    public interface IHistoryReponsitory : IRepository<History>
    {
    }
    public class HistoryReponsitory : RepositoryBase<History>, IHistoryReponsitory
    {
        public HistoryReponsitory(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
            {
            } 

    }
}
