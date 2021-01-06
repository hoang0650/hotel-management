using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Repositories
{
    public interface IUtilityMappingReponsitory : IRepository<UtilityMapping>
    {

    }
    public class UtilityMappingReponsitory : RepositoryBase<UtilityMapping>, IUtilityMappingReponsitory
    {
        public UtilityMappingReponsitory(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
