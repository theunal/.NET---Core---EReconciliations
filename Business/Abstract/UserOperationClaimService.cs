

using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface UserOperationClaimService<T>
    {
        IDataResult<List<T>> GetAll();
        IDataResult<T> Get(int id);


        IResult Add(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
    }
    
}
