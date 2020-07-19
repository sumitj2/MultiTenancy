using Abstraction.CommonInterfaces;
using Abstraction.DataAccessInterfaces.RepositoryInterfaces;
using Enitities;
using Implementation.Common;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.DataAccessImplementaion.RepositoryImplementation
{
    public class TenantInformationRepository : GeneralCommonRepository<TenantInformation>, ITenantInformationRepository
    {
        public TenantInformationRepository(GeneralCommonDBContext dbContext) :base(dbContext)
        {
            
        }
        //public TenantInformationRepository(IDbContextBase dbContext) : base(dbContext)
        //{

        //}

        public async Task<TenantInformation> GetTenantDetails(string tenantName)
        {
          // var tenantInformation = await Task.Run(() =>GetAll());

           var tenantInformation = await Task.Run(() => Find(x => x.TenantName == tenantName).FirstOrDefault());
            
            return tenantInformation;
        }
    }
}
