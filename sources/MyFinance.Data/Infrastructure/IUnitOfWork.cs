using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFinance.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
        IRepository<T> Repository<T>() where T : class;
    }
}
