using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Repositories
{
    public interface IConfigPriceReponsitory : IRepository<ConfigPrice> { }
    public class ConfigPriceReponsitory : RepositoryBase<ConfigPrice>, IConfigPriceReponsitory
    {
        public ConfigPriceReponsitory(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
