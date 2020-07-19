
using Database.Abstraction.Common;
using Database.Entities;
using System.Threading.Tasks;

namespace Database.Abstraction.Contract.Repository
{
   
    public interface ITenantInfoReposiory : IRepository<TenantInformation>
    {
        Task<TenantInformation> GetTenantDetailsByTenantName(string tenantName);

    }
}
