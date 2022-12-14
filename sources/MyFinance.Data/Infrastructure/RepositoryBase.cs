using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MyFinance.Domain;
using System.Data;
using System.Linq.Expressions;
using System.Data.Objects;

namespace MyFinance.Data.Infrastructure
{
public abstract class RepositoryBase<T> where T : class
{
    private MyFinanceContext dataContext;
    private readonly IDbSet<T> dbset;
    protected RepositoryBase(IDatabaseFactory databaseFactory)
    {
        DatabaseFactory = databaseFactory;
        dbset = DataContext.Set<T>();
    }

    private readonly ObjectContext _objContext;

    private ObjectSet<T> _objSet;
    private ObjectSet<T> ObjSet
    {
        get
        {
            if (_objSet == null)
            {
                _objSet = _objContext.CreateObjectSet<T>();
            }
            return _objSet;
        }
    }

    protected IEnumerable<T> ExecQuery<T>(string query, params object[] paras)
    {
        return this._objContext.ExecuteStoreQuery<T>(query, paras);
    }



    protected IDatabaseFactory DatabaseFactory
    {
        get; private set;
    }

    protected MyFinanceContext DataContext
    {
        get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
    }
    public virtual void Add(T entity)
    {
        dbset.Add(entity);           
    }
    public virtual void Update(T entity)
    {
        dbset.Attach(entity);
        dataContext.Entry(entity).State = System.Data.Entity.EntityState.Modified;
    }
    public virtual void Delete(T entity)
    {
        dbset.Remove(entity);           
    }
    public virtual void Delete(Expression<Func<T, bool>> where)
    {
        IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
        foreach (T obj in objects)
            dbset.Remove(obj);
    } 
    public virtual T GetById(long id)
    {
        return dbset.Find(id);
    }
    public virtual T GetById(string id)
    {
        return dbset.Find(id);
    }
    public virtual IEnumerable<T> GetAll()
    {
        return dbset.ToList();
    }
    public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
    {
        return dbset.Where(where).ToList();
    }
    public T Get(Expression<Func<T, bool>> where)
    {
        return dbset.Where(where).FirstOrDefault<T>();
    } 

}
}
