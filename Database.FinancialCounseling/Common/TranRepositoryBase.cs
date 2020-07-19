
using Database.Abstraction.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Common
{
   
    public abstract class TranRepositoryBase<T> : ITranRepository<T>
         where T : class
    {
       
        private IDbContext _existingDBcontext;

        
        private DbSet<T> _dbSet;

        
        public IDbContext dbContextBase
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

      
        public DbSet<T> dbSetBase
        {
            get
            {
                return _dbSet;
            }
            set
            {
                _dbSet = value;                
            }
        }

     
        private DbContext dbContext { get; set; }

        
        public virtual void Add(T entity)
        {
              this._dbSet.Add(entity);
            
        }

       
        public virtual void Update(T entity)
        {
            this._dbSet.Attach(entity);
            _existingDBcontext.Entry(entity).State = EntityState.Modified;
        }


      
        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
       
        public void DeleteById(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                _dbSet.Remove(entity);
        }

       
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = _dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbSet.Remove(obj);
        }

        
        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        
        public virtual IEnumerable<T> GetAll()
        {
            IEnumerable<T> returnvalues;
            returnvalues = this._dbSet.AsEnumerable<T>();
            return returnvalues;

        }

      
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> returnvalues;
            returnvalues = this._dbSet.AsEnumerable<T>();
            return returnvalues;
        }

        
        public T GetBy(Expression<Func<T, bool>> where)
        {
            return _dbSet.Where(where).FirstOrDefault<T>();
        }

        
        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> enumerable = _existingDBcontext.Set<T>().Where(predicate);

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
                    query = _dbSet.Include(includes[0]);
                }
                for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
                {
                    query = query.Include(includes[queryIndex]);
                }

                return query == null ? _dbSet : (IQueryable<T>)query;
            }
            return null;
        }

       
        public virtual void AddBulkInsert(IList<T> entity)
        {
        }
    }
}
