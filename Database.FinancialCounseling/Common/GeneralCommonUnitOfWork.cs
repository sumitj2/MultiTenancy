
using Database.Abstraction.Common;
using System;

namespace Database.Common
{
  
    public class GeneralCommonUnitOfWork : IUnitOfWork
    {
        
        private readonly IDbContextCore dataContext;

       
        private bool disposed = false;

       
        public GeneralCommonUnitOfWork(IDbContextCore dbContext)
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

   
    public interface IDbContextCore : IDisposable
    {
        int SaveChanges();
    }
}
