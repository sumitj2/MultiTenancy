using Database.Abstraction.Common;
using Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Database.Abstraction.Contract.Repository
{
    /// <summary>
    /// Coverage repository
    /// </summary>
    public interface IuserInfoRepository : ITranRepository<UserInfo>
    {
        void AddUserInfo(UserInfo userInfo);
    }
}
