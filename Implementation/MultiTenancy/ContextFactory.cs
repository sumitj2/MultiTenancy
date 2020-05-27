
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Implementation.DataAccessImplementaion.MultitenancyInterface;
using Abstraction.CommonInterfaces;
using Implementation.MultiTenancy.MultitenancyInterface;
using Implementation.DataAccessImplementaion;

namespace Implementation.MultiTenancy
{
    public class ContextFactory : IContextFactory
    {
     
        private readonly IOptions<ConnectionSettings> _connectionOptions;

        private readonly IDatabaseType _databaseType;

        
        IConfiguration _configuration;

        /// <summary>
        /// Private property for injecting dependency IUserDetailsUnitOfWork interface
        /// </summary>
        //private readonly IUserDetailsUnitOfWork _iUserDetailsUnitOfWork;

      
        public ContextFactory(IOptions<ConnectionSettings> connectionOptions,
            IDatabaseType databaseType,/* IUserDetailsUnitOfWork iUserDetailsUnitOfWork,*/ IConfiguration configuration)
        {
            _connectionOptions = connectionOptions;
            _databaseType = databaseType;
            //_iUserDetailsUnitOfWork = iUserDetailsUnitOfWork;
            _configuration = configuration;
        }

        public IDbContextBase DbContext => new GenericTranDBContext(ChangeDatabaseNameInConnectionString(DatabaseName, ServerPathName).Options);

        
        private string ExistingDatabaseName;

    
        public string DatabaseName
        {
            get
            {
                if ((ExistingDatabaseName == null && ExistingDatabaseName == string.Empty))
                {
                    ExistingDatabaseName = "";
                }
                return ExistingDatabaseName;
            }
            set
            {
                ExistingDatabaseName = value;
            }
        }

        private string ExistingServerName;

        public string ServerPathName
        {
            get
            {
                if ((ExistingServerName == null && ExistingServerName == string.Empty))
                {
                    ExistingServerName = "";
                }
                return ExistingServerName;
            }
            set
            {
                ExistingServerName = value;
            }
        }

     
        private DbContextOptionsBuilder<GenericTranDBContext> ChangeDatabaseNameInConnectionString(string databaseName, string serverPathName)
        {
            //  Create Connection String Builder using Default connection string
            var connectionBuilder = _databaseType.GetConnectionBuilder(_connectionOptions.Value.DefaultConnection);

            var connectionString = string.Empty;

            // Create DbContextOptionsBuilder with new Database name
            var contextOptionsBuilder = new DbContextOptionsBuilder<GenericTranDBContext>();

            //get connection string from appsettings.json based on tenantid passed in header
            connectionString = _configuration.GetConnectionString("TransStatesConnectionString");

            //set connection string based on tenantid passed in header
            _databaseType.SetConnectionString(contextOptionsBuilder, connectionString, databaseName, serverPathName);


            return contextOptionsBuilder;
        }
    }
}
