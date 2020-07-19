using Abstraction.CommonInterfaces;
using Enitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.DataAccessInterfaces.RepositoryInterfaces
{
    public interface IAddUsersInfoInTenants :ITransactionRepository<UserInfo>
    {
       void AddUserDetails(UserInfo userInfo);
       
    }
}
