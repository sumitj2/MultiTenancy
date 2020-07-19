using Database.Entities;
using System.Threading.Tasks;

namespace Database.Abstraction.Contract.UnitOfWork
{
    public interface ITenantInfoUnitOfWork
    {
        Task<TenantInformation> GetTenantInformationsByTenantName(string tenantName);
    }
}
