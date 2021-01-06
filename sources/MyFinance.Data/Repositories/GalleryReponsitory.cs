using MyFinance.Data.Infrastructure;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Repositories
{
    public interface IGalleryReponsitory : IRepository<Gallery>
    {

    }
    public class GalleryReponsitory : RepositoryBase<Gallery>, IGalleryReponsitory
    {
        public GalleryReponsitory(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
