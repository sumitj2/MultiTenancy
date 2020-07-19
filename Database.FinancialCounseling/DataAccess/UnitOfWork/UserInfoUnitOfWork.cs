
using Database.Abstraction.Contract.Repository;
using Database.Abstraction.Contract.UnitOfWork;
using Database.Common;
using Database.Entities;
using Database.Multitenancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database.DataAccess.UnitOfWork
{
    /// <summary>
    /// The payor plan unit of work
    /// </summary>
    public class UserInfoUnitOfWork : TranUnitOfWork, IUserInfoUnitOfWork, IDisposable
    {

        private Abstraction.Common.IDbContext dbContext;


        private IContextFactory _contextFactory;
        private IuserInfoRepository _iuserInfoRepository;

        public UserInfoUnitOfWork
            (
            IContextFactory contextFactory,
            IuserInfoRepository iuserInfoRepository

            ) : base(contextFactory)
        {
            _contextFactory = contextFactory;
            _iuserInfoRepository = iuserInfoRepository;
        }


        public async Task AddUserInfo(UserInfo userInfo)
        {

            try
            {
               // this.dbContext = _contextFactory.DbContext;

                _iuserInfoRepository.dbContextBase = this.dbContext;
                _iuserInfoRepository.dbSetBase = this.dbContext.Set<UserInfo>();

             //   _iuserInfoRepository.AddUserInfo(userInfo);
                _iuserInfoRepository.Add(userInfo);
                _iuserInfoRepository.dbContextBase.SaveChanges();
               // this.dbContext.SaveChanges();

                // return res;
            }
            catch (Exception ex) 
            {

                throw;
            }

        }

        public void CreateBaseDBContext(string facilityTenant, string databaseName, string serverPath, string userName, string password)
        {
            _contextFactory.DatabaseName = databaseName;
            _contextFactory.ServerPathName = serverPath;
            _contextFactory.userName = userName;
            _contextFactory.password = password;
            this.dbContext = _contextFactory.DbContext;
        }

        //public async Task<string> GetPlanType(string facilityTenant, int registrationId, IList<string> planTypes)
        //{
        //    this.dbContext = _contextFactory.DbContext;

        //    _payorPlanRepository.dbContextBase = this.dbContext;
        //    _payorPlanRepository.dbSetBase = this.dbContext.Set<PayorPlans>();

        //    _coveragesPlanRepository.dbContextBase = this.dbContext;
        //    _coveragesPlanRepository.dbSetBase = this.dbContext.Set<Coverages>();

        //    IList<Coverages> coverageDetails = await _coveragesPlanRepository.GetCoveragesDetails(registrationId);

        //    IList<PayorPlans> payorPlanDetails = await _payorPlanRepository.GetPayorPlanDetails();

        //    var planType = (from c in coverageDetails
        //                    join p in payorPlanDetails on c.FacilityPlanCode equals p.FacilityPlanCode
        //                    where c.RegistrationId == registrationId && planTypes.Contains(p.PlanType) && c.CoverageStatus == "A"
        //                    select new { p.PlanType }).ToList();

        //    return planType.Count == 0 ? string.Empty : planType.FirstOrDefault().PlanType;
        //}

    }
}
