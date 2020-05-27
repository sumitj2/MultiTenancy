using Abstraction.CommonInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Common
{
    public class GeneralCommonUnitOfWork : IUnitOfWorkBase
    {
        IDbContextBase dataContext;

        private bool disposed = false;
        public GeneralCommonUnitOfWork(IDbContextBase dbContext)
        {
            this.dataContext = dbContext;
        }
        public void Commit()
        {
            this.dataContext.SaveChanges();
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
                    dataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        ~GeneralCommonUnitOfWork()
        {
            Dispose(false);
        }
    }
}
