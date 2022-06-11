using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Performance;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal userDal;
        public UserManager(IUserDal userDal)
        {
            this.userDal = userDal;
        }


        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(userDal.GetAll(), Messages.CompanyAdded);
        }

        [SecuredOperation("admin,user.getall")]
        public IDataResult<List<UsersByCompanyDto>> GetUsersByCompanyId(int companyId)
        {
            var users = userDal.GetUsersByCompanyId(companyId);
            return new SuccessDataResult<List<UsersByCompanyDto>>(users, Messages.CompanyAdded);
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(userDal.Get(u => u.Id == id));
        }
        public IDataResult<User> GetByValue(int id)
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            return userDal.Get(u => u.Email == email);
        }

        public IDataResult<User> GetByEmailAddress(string email)
        {
            var user = userDal.Get(u => u.Email == email);
            if (user is not null)
            {
                return new SuccessDataResult<User>(user, Messages.UserHasBeenBrought);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            return userDal.GetClaims(user, companyId);
        }

        public User GtByMailConfirmValue(string value)
        {
            return userDal.Get(u => u.MailConfirmValue == value);
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





        public void Update(User entity)
        {
            var result = GetById(entity.Id);
            userDal.Update(entity);
        }

        public IResult UpdateByDto(UserUpdateDto entity)
        {
            var user = userDal.Get(u => u.Id == entity.UserId);

            if (entity.Email != user.Email)
            {
                var users = GetAll();
                foreach (var u in users.Data)
                {
                    if (u.Email == entity.Email)
                        return new ErrorResult(Messages.UserAlreadyExists);
                }
            }

            if (entity.Password != "")
            {
                byte[] PasswordHash, PasswordSalt;
                HashingHelper.CreatePasswordHash(entity.Password, out PasswordHash, out PasswordSalt);
                user.PasswordHash = PasswordHash;
                user.PasswordSalt = PasswordSalt;
            }
            user.Name = entity.Name;
            user.Email = entity.Email;
            user.IsActive = entity.IsActive;
            userDal.Update(user);
            return new SuccessResult(Messages.UserUpdated);
        }
    }
}
