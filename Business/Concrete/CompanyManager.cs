using Business.Abstract;
using Business.Const;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CompanyManager : CompanyService<Company>
    {
        private readonly ICompanyDal companyDal;
        public CompanyManager(ICompanyDal companyDal)
        {
            this.companyDal = companyDal;
        }

        
        public IResult Add(Company entity)
        {
            companyDal.Add(entity);
            return new SuccessResult(Messages.CompanyAdded);
        }

        public IResult Delete(Company entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Company> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Company>> GetAll()
        {
            return new SuccessDataResult<List<Company>>(companyDal.GetAll(), Messages.CompanyAdded);
        }

        public IResult Update(Company entity)
        {
            throw new NotImplementedException();
        }
    }
}
