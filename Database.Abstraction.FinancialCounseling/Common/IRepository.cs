using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Database.Abstraction.Common
{
   
    public interface IRepository<T> where T : class
    {
       
        void Add(T entity);

        void Update(T entity);

      
        void DeleteById(int id);

       
        void Delete(T entity);

        
        void Delete(Expression<Func<T, bool>> where);

   
        T GetById(int id);

        
        T GetBy(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);

        IQueryable<T> Find(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetIncluding(params Expression<Func<T, object>>[] includes);

        void AddBulkInsert(IList<T> entity);
    }
}
