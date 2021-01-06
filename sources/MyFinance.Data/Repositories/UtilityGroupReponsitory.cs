using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Repositories
{
    public interface IUtilityGroupReponsitory : IRepository<UtilityGroup>
    {

    }
    public class UtilityGroupReponsitory : RepositoryBase<UtilityGroup>, IUtilityGroupReponsitory
    {
        public UtilityGroupReponsitory(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
