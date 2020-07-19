
using Database.Abstraction.Common;
using Database.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Database.Multitenancy
{
    
    public class ContextFactory : IContextFactory
    {       
        
        private readonly IOptions<ConnectionSettings> _connectionOptions;

      
        private readonly IDatabaseType _databaseType;

       
        IConfiguration _configuration;

       

       
        public ContextFactory(IOptions<ConnectionSettings> connectionOptions,            
            IDatabaseType databaseType, IConfiguration configuration)
        {
            _connectionOptions = connectionOptions;            
            _databaseType = databaseType;
            
            _configuration = configuration;
        }


        public IDbContext DbContext
        {
            get
            {
                var res = ChangeDatabaseNameInConnectionString(DatabaseName, ServerPathName, userName, password).Options;
                return new GenericTranDBContext(res);
            }
        }

        private string ExistingDatabaseName;

        private string ExistingPassword;
        public string password
        {
            get
            {
                if ((ExistingPassword == null && ExistingPassword == string.Empty))
                {
                    ExistingPassword = "";
                }
                return ExistingPassword;
            }
            set
            {
                ExistingPassword = value;
            }
        }

        private string ExistingUserName;
        public string userName
        {
            get
            {
                if ((ExistingUserName == null && ExistingUserName == string.Empty))
                {
                    ExistingUserName = "";
                }
                return ExistingUserName;
            }
            set
            {
                ExistingUserName = value;
            }
        }
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

      
        private DbContextOptionsBuilder<GenericTranDBContext> ChangeDatabaseNameInConnectionString(string databaseName, string serverPathName, string userName,string password)
        {
            //  Create Connection String Builder using Default connection string
            var connectionBuilder = _databaseType.GetConnectionBuilder(_connectionOptions.Value.DefaultConnection);

            var connectionString = string.Empty;

            // Create DbContextOptionsBuilder with new Database name
            var contextOptionsBuilder = new DbContextOptionsBuilder<GenericTranDBContext>();
            
            //get connection string from appsettings.json based on tenantid passed in header
            connectionString = _configuration.GetConnectionString("TransStatesConnectionString");

             connectionBuilder = _databaseType.GetConnectionBuilder(connectionString);

            //set connection string based on tenantid passed in header
            _databaseType.SetConnectionString(contextOptionsBuilder, connectionString, databaseName, serverPathName,userName,password);                
            

            return contextOptionsBuilder;
        }
    }
}
