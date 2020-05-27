using Abstraction.CommonInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Common
{
    public abstract class TransactionRepository<T>  : CommonRepositoryBase<T>, ITransactionRepository<T>
        where T : class
    {
        public TransactionRepository()
        {
                
        }

        private IDbContextBase _existingDBcontext;


        /// <summary>
        /// DBSet 
        /// </summary>
        private DbSet<T> _dbSet;

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

    }
}
