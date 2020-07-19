using Abstraction.CommonInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Implementation.Common
{
    public abstract class TransactionRepository<T>  : ITransactionRepository<T>
        where T : class
    {
        public TransactionRepository()
        {
                
        }

        private IDbContextBase _existingDBcontext;


        /// <summary>
        /// DBSet 
        /// </summary>
        private DbSet<T> dbSet;

        /// <summary>
        /// DBContext base
        /// </summary>
        public IDbContextBase dbContextBase
        {
            get
            {
                return _existingDBcontext;
            }
            set
            {
                _existingDBcontext = value;
                this.dbSetBase = this.dbContextBase.Set<T>();
            }
        }

        /// <summary>
        /// Db Set base
        /// </summary>
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

        /// <summary>
        /// DB context 
        /// </summary>
       
        private DbContext dbContext { get; set; }
     

        public void Add(T entity)
        {
           dbSet.Add(entity);
            
        }

        public virtual void Update(T entity)
        {
            this.dbSet.Attach(entity);
            dbContextBase.Entry(entity).State = EntityState.Modified;
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
            IEnumerable<T> enumerable = dbContextBase.Set<T>().Where(predicate);

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
          //  dbContextBase.BulkInsert<T>(entity);
        }

    }
}
