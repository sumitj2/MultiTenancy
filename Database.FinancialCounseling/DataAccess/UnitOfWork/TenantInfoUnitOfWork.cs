using Database.Abstraction.Contract.Repository;
using Database.Abstraction.Contract.UnitOfWork;
using Database.Common;
using Database.Entities;
using System;
using System.Threading.Tasks;



namespace Database.DataAccess.UnitOfWork
{
    
    public class TenantInfoUnitOfWork : GeneralCommonUnitOfWork, ITenantInfoUnitOfWork, IDisposable
    {

        private readonly ITenantInfoReposiory _tenantInfoReposiory;

       
        public TenantInfoUnitOfWork(ITenantInfoReposiory tenantInfoReposiory, IDbContextCore dbContextCore) 
            : base(dbContextCore)
        {
            _tenantInfoReposiory = tenantInfoReposiory;
        }

        public async Task<TenantInformation> GetTenantInformationsByTenantName(string tenantName)
        {
            // Method to get the UserId based on username
            return await _tenantInfoReposiory.GetTenantDetailsByTenantName(tenantName);
        }



    }
}
