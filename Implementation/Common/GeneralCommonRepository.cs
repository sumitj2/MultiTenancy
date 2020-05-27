using Abstraction.CommonInterfaces;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Implementation.Common
{
    public abstract class GeneralCommonRepository<T> : IRepositoryBase<T> //CommonRepositoryBase<T>
        where T : class
    {
        private DbContext dataContext;

        private  DbSet<T> dbSet;

        public DbSet<T> dbSetBase
        {
            get
            {
                return dbSet;
            }
            set
            {
                dbSet = value;
            }
        }
        public GeneralCommonRepository(IDbContextBase context)
        {
            dataContext = (DbContext)context;
            if (dataContext != null)
                this.dbSet = this.dataContext.Set<T>();
        }
        public void Add(T entity)
        {
            this.dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            this.dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteById(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                dbSet.Remove(entity);
        }


        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }


        public virtual T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            IEnumerable<T> returnvalues;
            returnvalues = this.dbSet.AsEnumerable<T>();
            return returnvalues;

        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> returnvalues;
            returnvalues = this.dbSet.AsEnumerable<T>();
            return returnvalues;

        }

        public T GetBy(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> enumerable = dataContext.Set<T>().Where(predicate);

            EnumerableQuery<T> queryable = new EnumerableQuery<T>(enumerable);
            return queryable;
        }


        public IQueryable<T> GetIncluding(params Expression<Func<T, object>>[] includes)
        {
            if (includes != null)
            {
                IIncludableQueryable<T, object> query = null;

                if (includes.Length > 0)
                {
                    query = dbSet.Include(includes[0]);
                }
                for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
                {
                    query = query.Include(includes[queryIndex]);
                }
                return query == null ? dbSet : (IQueryable<T>)query;
            }
            return null;
        }
        //public IEnumerable<TEntity> Include(params Expression<Func<TEntity, object>>[] includes)
        //{
        //    IDbSet<TEntity> dbSet = Context.Set<TEntity>();

        //    IEnumerable<TEntity> query = null;
        //    foreach (var include in includes)
        //    {
        //        query = dbSet.Include(include);
        //    }

        //    return query ?? dbSet;
        //}
        public IEnumerable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            //   IDbSet<T> dbSet = Context.Set<T>();

            IIncludableQueryable<T, object> dbSet = null;

            IEnumerable<T> query = null;
            foreach (var include in includes)
            {
                query = dbSet.Include(include);
            }

            return query ?? dbSet;
        }


        public virtual void AddBulkInsert(IList<T> entity)
        {
            dataContext.BulkInsert<T>(entity);
        }
    }
}
