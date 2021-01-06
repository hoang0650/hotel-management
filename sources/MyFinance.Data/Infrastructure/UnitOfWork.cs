using MyFinance.Extention;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFinance.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private MyFinanceContext dataContext;
        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected MyFinanceContext DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.Commit();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            if (repositories.Keys.Contains(typeof(TEntity)) == true)
        {
            return repositories[typeof(TEntity)] as IRepository<TEntity>;
        }
          
           IDatabaseFactory dataFactory = IoC.Get<IDatabaseFactory>();
           IRepository<TEntity> repo = new Repository<TEntity>(dataFactory);
           repositories.Add(typeof(TEntity), repo);
        return repo;
        }
    }
}
