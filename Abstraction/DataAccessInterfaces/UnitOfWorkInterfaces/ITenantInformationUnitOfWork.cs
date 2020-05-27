using Enitities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstraction.DataAccessInterfaces.UnitOfWorkInterfaces
{
    public interface ITenantInformationUnitOfWork
    {
        Task<List<TenantInformation>> GetTenantInformations();
    }
}
