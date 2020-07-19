using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Threading;
using System.Threading.Tasks;

namespace Database.Abstraction.Common
{
    public interface IDbContext
    {
        
        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

      
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);

       
        void Dispose();

        
        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;        

       
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        //EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class; //todo
 
    }
}
