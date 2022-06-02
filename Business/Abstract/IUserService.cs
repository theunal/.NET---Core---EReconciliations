using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IUserService<T>
    {
        IDataResult<List<T>> GetAll();
        IDataResult<T> Get(int id);


        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);



        List<OperationClaim> GetClaims(T user, int companyId);
        User GetByEmail(string email);

    }
}
