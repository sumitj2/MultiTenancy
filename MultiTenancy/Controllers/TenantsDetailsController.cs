using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Abstraction.Contract.UnitOfWork;
using Database.Entities;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MultiTenancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsDetails : ControllerBase
    {
        private readonly ITenantInfoUnitOfWork _tenantInformationUnitOfWork;
        public TenantsDetails(ITenantInfoUnitOfWork tenantInformationUnitOfWork)
        {
            _tenantInformationUnitOfWork = tenantInformationUnitOfWork;
        }

        [HttpGet]
        public async Task<TenantInformation> GetAllTenants(string tenantName)
        {            
            var result = await _tenantInformationUnitOfWork.GetTenantInformationsByTenantName(tenantName);
            return result;
        }
    }
}
