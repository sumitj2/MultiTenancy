using Enitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.DataAccessInterfaces.UnitOfWorkInterfaces
{
    public interface IAddUserInfoInTenantsUnitOfWork
    {
       void AddUserInfo(UserInfo userInfo);
        void CreateBaseDBContext(string facilityTenant, string databaseName, string serverPath);
    }
}
