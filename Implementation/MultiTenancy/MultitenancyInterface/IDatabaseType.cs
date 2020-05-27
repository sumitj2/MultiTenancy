using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Data.Common;
using Implementation.MultiTenancy;



namespace Implementation.DataAccessImplementaion.MultitenancyInterface
{
    public interface IDatabaseType
    {
        
        IServiceCollection EnableDatabase(IServiceCollection services, IOptions<ConnectionSettings> connectionOptions);

        DbContextOptionsBuilder GetContextBuilder(DbContextOptionsBuilder optionsBuilder, IOptions<ConnectionSettings> connectionOptions, string connectionString);

        DbConnectionStringBuilder GetConnectionBuilder(string connectionString);

        DbContextOptionsBuilder<TContext> SetConnectionString<TContext>(DbContextOptionsBuilder<TContext> contextOptionsBuilder, string connectionString, string databaseName, string serverPathName) where TContext : DbContext;
    }
}
