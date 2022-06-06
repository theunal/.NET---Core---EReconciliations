using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal companyDal;
        public CompanyManager(ICompanyDal companyDal)
        {
            this.companyDal = companyDal;
        }

        [CacheAspect(30)]
        public IDataResult<List<Company>> GetAll()
        {
            return new SuccessDataResult<List<Company>>(companyDal.GetAll(), Messages.CompaniesHasBeenBrought);
        }
        [CacheAspect(30)]
        public IDataResult<Company> Get(Company company)
        {
            return new SuccessDataResult<Company>(companyDal.Get(c => c.Id == company.Id));
        }

        [CacheAspect(30)]
        public IDataResult<Company> GetById(int id)
        {
            var result = companyDal.Get(c => c.Id == id);
            if (result is not null)
            {
                return new SuccessDataResult<Company>(result, Messages.CompanyHasBeenBrought);
            }
            return new ErrorDataResult<Company>(Messages.CompanyNotFound);
        }




        [PerformanceAspect(3)]
        [SecuredOperation("company.update,admin")]
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





        [TransactionScopeAspect]
        [CacheRemoveAspect("ICompanyService.Get")]
        public IResult Add(Company entity)
        {
            companyDal.Add(entity);
            return new SuccessResult(Messages.CompanyAdded);
        }

        [TransactionScopeAspect] // burada direkt dalı çağırıp ekledik sonra düzelticem
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
            return new SuccessResult(Messages.CompanyAdded);
        }


    }
}
