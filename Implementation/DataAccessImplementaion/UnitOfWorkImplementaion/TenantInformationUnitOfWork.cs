using Abstraction.CommonInterfaces;
using Abstraction.DataAccessInterfaces.RepositoryInterfaces;
using Abstraction.DataAccessInterfaces.UnitOfWorkInterfaces;
using Enitities;
using Implementation.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.DataAccessImplementaion.UnitOfWorkImplementaion
{
    public class TenantInformationUnitOfWork : GeneralCommonUnitOfWork ,ITenantInformationUnitOfWork, IDisposable
    {
        private readonly ITenantInformationRepository _tenantInformationRepository;
        public TenantInformationUnitOfWork(ITenantInformationRepository tenantInformationRepository,IDbContextBase dbContextBase ):base(dbContextBase)
        {
            _tenantInformationRepository = tenantInformationRepository;
        }

        public async Task<TenantInformation> GetTenantInformations(string tenantName)
        {
            // Method to get the UserId based on username
            return await _tenantInformationRepository.GetTenantDetails(tenantName);
        }
    }
}
