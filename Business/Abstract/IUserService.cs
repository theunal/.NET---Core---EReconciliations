using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> Get(int id);


        IResult Add(User entity);

        IResult Delete(User entity);

        

        void Update(User entity);
        List<OperationClaim> GetClaims(User user, int companyId);
        User GetByEmail(string email);
        User GtByMailConfirmValue(string value);
        IDataResult<User> GetByValue(int id);

    }
}
