using Business.Abstract;
using Business.Const;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal userDal;
        public UserManager(IUserDal userDal)
        {
            this.userDal = userDal;
        }

        
        public IResult Add(User entity)
        {
            userDal.Add(entity);
            return new SuccessResult("User added");
        }

        public IResult Delete(User entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(userDal.GetAll(), Messages.CompanyAdded);
        }

        public User GetByEmail(string email)
        {
            return userDal.Get(u => u.Email == email);
        }
        
        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            return userDal.GetClaims(user, companyId);
        }

        public IResult Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
