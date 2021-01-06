using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Repositories
{
    public interface ITokenRepository :IRepository<Token>{ }
    public class TokenRepository :RepositoryBase<Token>, ITokenRepository
    {
        public TokenRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

    }
}
