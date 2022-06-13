using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Performance;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal companyDal;
        private readonly IOperationClaimService operationClaimService;
        private readonly IUserOperationClaimService userOperationClaimService;
        public CompanyManager(ICompanyDal companyDal, IOperationClaimService operationClaimService,
            IUserOperationClaimService userOperationClaimService)
        {
            this.companyDal = companyDal;
            this.operationClaimService = operationClaimService;
            this.userOperationClaimService = userOperationClaimService;
        }

        [CacheAspect(30)]
        public IDataResult<List<Company>> GetAll()
        {
            return new SuccessDataResult<List<Company>>(companyDal.GetAll(), Messages.CompaniesHasBeenBrought);
        }

       // [CacheAspect(30)]
        public IDataResult<Company> Get(Company company)
        {
            return new SuccessDataResult<Company>(companyDal.Get(c => c.Id == company.Id));
        }

       // [CacheAspect(30)]
        public IDataResult<Company> GetById(int id)
        {
            var result = companyDal.Get(c => c.Id == id);
            if (result is not null)
            {
                return new SuccessDataResult<Company>(result, Messages.CompanyHasBeenBrought);
            }
            return new ErrorDataResult<Company>(Messages.CompanyNotFound);
        }




       // [PerformanceAspect(3)]
        //[SecuredOperation("company.update,admin")]
        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult Update(Company entity)
        {
            var result = GetById(entity.Id);
            if (result is not null)
            {
                companyDal.Update(entity);
                return new SuccessResult(Messages.CompanyUpdated);
            }
            return new ErrorResult(Messages.CompanyNotFound);
        }
        
        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult Delete(Company entity)
        {
            throw new NotImplementedException();
        }





    
        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult Add(Company entity)
        {
            companyDal.Add(entity);
            return new SuccessResult(Messages.CompanyAdded);
        }

        // burada direkt dalı çağırıp ekledik sonra düzelticem
        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult AddUserCompany(int userId, int companyId) // userCompany tablosuna user ve company id sini ekler
        { 
            companyDal.AddUserCompany(userId, companyId);
            return new SuccessResult();
        }
        
        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult AddCompanyUser(CompanyDto dto) // mevcut usera company ekler
        {   
            companyDal.Add(dto.Company);
            AddUserCompany(dto.UserId, dto.Company.Id);

            var operationClaims = operationClaimService.GetAll();
            foreach (var operationClaim in operationClaims.Data)
            {
                if (operationClaim.Name != "admin" && !operationClaim.Name.Contains("mail") &&
                    !operationClaim.Name.Contains("Claim"))
                {
                    UserOperationClaim userOperationClaim = new UserOperationClaim
                    {
                        CompanyId = dto.Company.Id,
                        UserId = dto.UserId,
                        OperationClaimId = operationClaim.Id,
                        AddedAt = DateTime.Now,
                        IsActive = true
                    };
                    userOperationClaimService.Add(userOperationClaim);
                }
            }
            
            return new SuccessResult(Messages.CompanyAdded);
        }

        public IDataResult<List<Company>> GetAllCompanyAdminUserId(int adminUserId)
        {
            var result = companyDal.GetAllCompanyAdminUserId(adminUserId);
            return new SuccessDataResult<List<Company>>(result, Messages.CompaniesHasBeenBrought);
        }
    }
}
