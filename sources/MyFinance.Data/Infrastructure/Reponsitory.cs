using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private MyFinanceContext dataContext;
        private readonly IDbSet<TEntity> dbset;
        private readonly DbSet<TEntity> dbcontext;
        public Repository(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<TEntity>();
            dbcontext= DataContext.Set<TEntity>();
        }

        private readonly ObjectContext _objContext;

        private ObjectSet<TEntity> _objSet;
        private ObjectSet<TEntity> ObjSet
        {
            get
            {
                if (_objSet == null)
                {
                    _objSet = _objContext.CreateObjectSet<TEntity>();
                }
                return _objSet;
            }
        }

        protected IEnumerable<TEntity> ExecQuery<TEntity>(string query, params object[] paras)
        {
            return this._objContext.ExecuteStoreQuery<TEntity>(query, paras);
        }



        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected MyFinanceContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
        public virtual void Add(TEntity entity)
        {
            dbset.Add(entity);
        }
        public virtual void Update(TEntity entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }
        public virtual void Delete(TEntity entity)
        {
            dbset.Remove(entity);
        }
        public virtual void Delete(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = dbset.Where<TEntity>(where).AsEnumerable();
          
            foreach (TEntity obj in objects)
                dbset.Remove(obj);
        }
        public virtual void DeleteRange(Expression<Func<TEntity, bool>> where)
        {
            IEnumerable<TEntity> objects = dbset.Where<TEntity>(where).AsEnumerable();
            dbcontext.RemoveRange(objects);
           
        }
        public virtual TEntity GetById(long id)
        {
            return dbset.Find(id);
        }
        public virtual TEntity GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return dbset.ToList();
        }
        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return dbset.Where(where).ToList();
        }
        public TEntity Get(Expression<Func<TEntity, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<TEntity>();
        }
        public IQueryable<TEntity> GetQueryable()
        {
            return this.dataContext.Set<TEntity>();
        }
    }
}
