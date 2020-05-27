using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstraction.DataAccessInterfaces.UnitOfWorkInterfaces;
using Contract;
using Microsoft.AspNetCore.Mvc;
using ent=Enitities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MultiTenancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsDetails : ControllerBase
    {
        private readonly ITenantInformationUnitOfWork _tenantInformationUnitOfWork;
        public TenantsDetails(ITenantInformationUnitOfWork tenantInformationUnitOfWork)
        {
            _tenantInformationUnitOfWork = tenantInformationUnitOfWork;
        }

        [HttpGet]
        public async Task<List<ent.TenantInformation>> GetAllTenants()
        {            
            var result = await _tenantInformationUnitOfWork.GetTenantInformations();
            return result.ToList();
        }
    }
}
