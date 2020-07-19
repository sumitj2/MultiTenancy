using Database.Abstraction.Common;

namespace Database.Multitenancy
{
    public interface IContextFactory
    {
        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <value>
        /// The database context.
        /// </value>
        IDbContext DbContext { get; }
        
        /// <summary>
        /// DatabaseName
        /// </summary>
        string DatabaseName { get; set; }

        /// <summary>
        /// ServerPathName
        /// </summary>
        string ServerPathName { get; set; }

        string userName { get; set; }

        string password { get; set; }
    }
}
