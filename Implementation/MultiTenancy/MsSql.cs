using Implementation.DataAccessImplementaion.MultitenancyInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Data.SqlClient;

namespace Implementation.MultiTenancy
{
    public class MsSql : IDatabaseType
    {
        /// <summary>
        /// EnableDatabase
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionOptions"></param>
        /// <returns></returns>
        public IServiceCollection EnableDatabase(IServiceCollection services, IOptions<ConnectionSettings> connectionOptions)
        {
            return services;
        }

        /// <summary>
        /// GetConnectionBuilder
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public DbConnectionStringBuilder GetConnectionBuilder(string connectionString)
        {
            return new SqlConnectionStringBuilder(connectionString);
        }
        
        public DbContextOptionsBuilder GetContextBuilder(DbContextOptionsBuilder optionsBuilder, IOptions<ConnectionSettings> connectionOptions, string connectionString)
        {
            return optionsBuilder.UseSqlServer(connectionString, b => EntityFrameworkConfiguration.GetMigrationInformation(b));
        }

        
        public DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString, string databaseName, string serverPathName, string userName, string password) where TContext : DbContext
        {
            if (string.IsNullOrEmpty(databaseName) && string.IsNullOrEmpty(serverPathName))
            {
                //set default connection of accretive database
                return contextOptionsBuilder.UseSqlServer(connectionString);
            }
            else
            {
                var connection = string.Format(connectionString, serverPathName, databaseName,userName,password);
                //set connection string based on tenantid
                return contextOptionsBuilder.UseSqlServer(connection);
            }
        }
    }
}
