using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface UserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<User> Get(int id);


        IResult Add(User entity);
        IResult Update(User entity);
        IResult Delete(User entity);



        List<OperationClaim> GetClaims(User user, int companyId);
        User GetByEmail(string email);

    }
}
