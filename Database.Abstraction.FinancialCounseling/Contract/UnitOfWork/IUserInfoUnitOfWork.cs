using Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Database.Abstraction.Contract.UnitOfWork
{
    public interface IUserInfoUnitOfWork
    {
        void CreateBaseDBContext(string facilityTenant, string databaseName, string serverPath, string userName, string password);
        Task AddUserInfo(UserInfo userInfo);

    }
}
