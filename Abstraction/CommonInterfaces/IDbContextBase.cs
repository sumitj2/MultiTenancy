using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Abstraction.CommonInterfaces
{
    public interface IDbContextBase  : IDisposable
    {
        int SaveChanges();  


        int SaveChanges(bool acceptAllChangesOnSuccess);


        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken);
       new void Dispose();


        DbSet<TEntity> Set<TEntity>()
            where TEntity : class;

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
      
      //  EntityEntry<TEntity> Entry<TEntity>([NotNullAttribute] TEntity entity) where TEntity : class; //todo

    }
}
