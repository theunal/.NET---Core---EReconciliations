using Business.Abstract;
using Business.Const;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal companyDal;
        public CompanyManager(ICompanyDal companyDal)
        {
            this.companyDal = companyDal;
        }

        
        public IDataResult<List<Company>> GetAll()
        {
            return new SuccessDataResult<List<Company>>(companyDal.GetAll(), Messages.CompanyAdded);
        }
        public IDataResult<Company> Get(Company company)
        {
            return new SuccessDataResult<Company>(companyDal.Get(c => c.Id == company.Id));
        }



        
      
        public IResult Add(Company entity)
        {
            companyDal.Add(entity);
            return new SuccessResult(Messages.CompanyAdded);
        }
        public IResult Update(Company entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(Company entity)
        {
            throw new NotImplementedException();
        }

        public IResult AddUserCompany(int userId, int companyId)
        {
            companyDal.AddUserCompany(userId, companyId);
            return new SuccessResult();
        }
    }
}
