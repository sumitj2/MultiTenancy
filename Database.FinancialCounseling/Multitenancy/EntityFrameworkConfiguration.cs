
using Database.Abstraction.Common;
using Database.Common;
using Database.DataAccess;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace Database.Multitenancy
{
  
    public static class EntityFrameworkConfiguration
    {
       
        public static void ConfigureService(IServiceCollection services, IConfigurationRoot configuration)
        {
            //gets default connection
            string connectionString = configuration.GetConnectionString(DefaultConstants.GeneralCommonDataDBConnectionString);

            // Database connection settings
            var connectionOptions = services.BuildServiceProvider().GetRequiredService<IOptions<ConnectionSettings>>();

            RegisterDatabaseType(services, connectionOptions);

            var databaseTypeInstance = services.BuildServiceProvider().GetRequiredService<IDatabaseType>();

            databaseTypeInstance.EnableDatabase(services, connectionOptions);

            // Entity framework configuration
            services.AddDbContext<GenericTranDBContext>(options =>
                databaseTypeInstance.GetContextBuilder(options, connectionOptions, connectionString));

            services.AddScoped<IDbContext, GenericTranDBContext>();
        }

      
        public static TBuilder GetMigrationInformation<TBuilder, TExtension>(RelationalDbContextOptionsBuilder<TBuilder, TExtension> builder)
             where TBuilder : RelationalDbContextOptionsBuilder<TBuilder, TExtension>
            where TExtension : RelationalOptionsExtension, new()
        {

            return builder.MigrationsAssembly(typeof(GenericTranDBContext).Assembly.GetName().Name);
        }

        
        private static void RegisterDatabaseType(IServiceCollection services, IOptions<ConnectionSettings> connectionOptions)
        {
            var databaseInterfaceType = typeof(IDatabaseType);
            var instanceType = connectionOptions.Value.DatabaseType.ToString();
            var instance = databaseInterfaceType.Assembly.GetTypes().FirstOrDefault(x =>
             databaseInterfaceType.IsAssignableFrom(x)
             &&
             string.Equals(instanceType, x.Name, StringComparison.OrdinalIgnoreCase));
            services.AddSingleton<IDatabaseType>((IDatabaseType)Activator.CreateInstance(instance));
        }

    }
}
