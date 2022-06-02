using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserCompanyManager : IUserCompanyService
    {
        private readonly IUserCompanyDal userCompanyDal;
        public UserCompanyManager(IUserCompanyDal userCompanyDal)
        {
            this.userCompanyDal = userCompanyDal;
        }

        
        public IResult Add(UserCompany entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(UserCompany entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<UserCompany> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<UserCompany>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(UserCompany entity)
        {
            throw new NotImplementedException();
        }
    }
}
