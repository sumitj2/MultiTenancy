using Enitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.DataAccessInterfaces.RepositoryInterfaces
{
    public interface ITenantInformationRepository
    {
        Task<List<TenantInformation>> GetTenantDetails();
    }
}
