using MyFinance.Data.Infrastructure;
using MyFinance.Domain;
using MyFinance.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Repositories
{
    public interface IInvoiceRepository : IRepository<Invoice>
    {

    }
    public class InvoiceRepository : RepositoryBase<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}
