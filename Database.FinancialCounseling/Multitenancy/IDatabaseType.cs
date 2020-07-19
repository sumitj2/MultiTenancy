using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data.Common;

namespace Database.Multitenancy
{
   
    public interface IDatabaseType
    {
        IServiceCollection EnableDatabase(IServiceCollection services, IOptions<ConnectionSettings> connectionOptions);

      
        DbContextOptionsBuilder GetContextBuilder(DbContextOptionsBuilder optionsBuilder, IOptions<ConnectionSettings> connectionOptions, string connectionString);

     
        DbConnectionStringBuilder GetConnectionBuilder(string connectionString);

        DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString, string databaseName, string serverPathName, string userName, string password) where TContext : DbContext;
    }
}
