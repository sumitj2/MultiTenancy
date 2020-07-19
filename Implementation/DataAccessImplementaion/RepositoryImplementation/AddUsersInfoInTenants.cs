using Abstraction.DataAccessInterfaces.RepositoryInterfaces;
using Enitities;
using Implementation.Common;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.DataAccessImplementaion.RepositoryImplementation
{
    public class AddUsersInfoInTenants :TransactionRepository<UserInfo>, IAddUsersInfoInTenants
    {
        public async void AddUserDetails(UserInfo userInfo)
        {
            try
            {
                await Task.Run(() => Add(userInfo));
               
            }
            catch (Exception ex)
            {
                               
            }
            
        }

       
    }
}
