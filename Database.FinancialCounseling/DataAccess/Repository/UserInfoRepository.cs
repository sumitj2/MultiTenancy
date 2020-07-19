using Database.Abstraction.Contract.Repository;

using Database.Common;
using Database.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Database.DataAccess.Repository
{
    public class UserInfoRepository : TranRepositoryBase<UserInfo>, IuserInfoRepository
    {      
        public async void AddUserInfo(UserInfo userInfo)
        {

            try
            {
                //  var s = 2;
                //var result = await Task.Run(() => Find(x => x.SrNo == s));
                await Task.Run(() => Add(userInfo));
                this.dbContextBase.SaveChanges();
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
