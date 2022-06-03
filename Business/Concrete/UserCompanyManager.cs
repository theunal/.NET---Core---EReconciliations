using Business.Abstract;
using Business.Const;
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

        public IDataResult<UserCompany> GetById(int id)
        {
            var result = userCompanyDal.Get(u => u.UserId == id);
            if (result is null)
            {
                return new ErrorDataResult<UserCompany>(Messages.UserCompanyNotFound);
            }
            return new SuccessDataResult<UserCompany>(result);
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
