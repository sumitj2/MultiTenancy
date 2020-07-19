using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database.Abstraction.Contract.UnitOfWork;
using Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MultiTenancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddInTransactionTenantsTableController : ControllerBase
    {
        private readonly ITenantInfoUnitOfWork _tenantInformationUnitOfWork;

        private readonly IUserInfoUnitOfWork _addUserInfoInTenantsUnitOfWork;
        public AddInTransactionTenantsTableController(ITenantInfoUnitOfWork tenantInformationUnitOfWork, IUserInfoUnitOfWork addUserInfoInTenantsUnitOfWork)
        {
            _tenantInformationUnitOfWork = tenantInformationUnitOfWork;
            _addUserInfoInTenantsUnitOfWork = addUserInfoInTenantsUnitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> SaveInTransactionTenants([FromHeader] string tenantName, UserInfo userInfo)
        {
            var tenantDetails = await _tenantInformationUnitOfWork.GetTenantInformationsByTenantName(tenantName);

            _addUserInfoInTenantsUnitOfWork.CreateBaseDBContext(tenantDetails.TenantName, tenantDetails.InitialCatalog, tenantDetails.DataSource, tenantDetails.UserId, tenantDetails.Password);
            // _addUserInfoInTenantsUnitOfWork.CreateBaseDBContext("Maharashtra", "MaharashtraDB", "LAPTOP-6IH46700\\SQLEXPRESS", "","");

            await _addUserInfoInTenantsUnitOfWork.AddUserInfo(userInfo);
            return Ok();


        }
    }
}
