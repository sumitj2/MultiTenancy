
using Database.Abstraction.Contract.Repository;
using Database.Common;
using Database.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Database.DataAccess.Repository
{
    
    public class TenantInfoReposiory : GeneralCommonRepositoryBase<TenantInformation>, ITenantInfoReposiory
    {
        
        public TenantInfoReposiory(IDbContextCore context ):base(context)
        {
        }

        public async Task<TenantInformation> GetTenantDetailsByTenantName(string tenantName)
        {
            // var tenantInformation = await Task.Run(() =>GetAll());

            var tenantInformation = await Task.Run(() => Find(x => x.TenantName == tenantName).FirstOrDefault());

            return tenantInformation;
        }



    }
}
