using Abstraction.CommonInterfaces;
using Abstraction.DataAccessInterfaces.RepositoryInterfaces;
using Abstraction.DataAccessInterfaces.UnitOfWorkInterfaces;
using Enitities;
using Implementation.Common;
using Implementation.MultiTenancy.MultitenancyInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.DataAccessImplementaion.UnitOfWorkImplementaion
{
    public class AddUserInfoInTenantsUnitOfWork :TransactionUnitOfWork, IAddUserInfoInTenantsUnitOfWork, IDisposable
    {
        private IContextFactory _contextFactory;
        private IDbContextBase dbContextBase;
        private IAddUsersInfoInTenants _addUsersInfoInTenants;
        public AddUserInfoInTenantsUnitOfWork(IContextFactory contextFactory, IAddUsersInfoInTenants addUsersInfoInTenants) :base(contextFactory)
        {
            _contextFactory = contextFactory;
            _addUsersInfoInTenants = addUsersInfoInTenants;
        }
        public async void AddUserInfo(UserInfo userInfo)
        {

            this.dbContextBase = _contextFactory.GetDbContext();
            _addUsersInfoInTenants.dbContextBase = this.dbContextBase;
            _addUsersInfoInTenants.dbSetBase = this.dbContextBase.Set<UserInfo>();

              _addUsersInfoInTenants.AddUserDetails(userInfo);
           // return res;
        }

        public void CreateBaseDBContext(string facilityTenant, string databaseName, string serverPath)
        {
            _contextFactory.DatabaseName = databaseName;
            _contextFactory.ServerPathName = serverPath;
             this.dbContextBase = _contextFactory.GetDbContext();
        }
    }
}
