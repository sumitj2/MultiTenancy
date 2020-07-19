using Abstraction.CommonInterfaces;
using Implementation.MultiTenancy.MultitenancyInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Common
{
    public class TransactionUnitOfWork :IUnitOfWorkBase
    {
        private bool disposed = false;
        private readonly IDbContextBase _dbContext;
        public TransactionUnitOfWork(IContextFactory contextFactory)
        {
            _dbContext = contextFactory.GetDbContext();
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        ~TransactionUnitOfWork()
        {
            Dispose(false);
        }
    }
}
