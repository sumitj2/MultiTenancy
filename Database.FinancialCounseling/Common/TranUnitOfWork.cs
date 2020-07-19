
using Database.Abstraction.Common;

using Database.Multitenancy;
using System;

namespace Database.Common
{
    
    public class TranUnitOfWork: ITranUnitOfWork
    {
      
        private Abstraction.Common.IDbContext dbContext;

       
        private bool disposed = false;

       
        public TranUnitOfWork()
        {                
        }

        
        public TranUnitOfWork(IContextFactory contextFactory)
        {
            this.dbContext = contextFactory.DbContext;
        }

       
        public void Commit()
        {
            dbContext.SaveChanges();
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
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

               
        ~TranUnitOfWork()
        {
            Dispose(false);
        }
    }
}
