using Core.Utilities.Results;
using Core.Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<UserOperationClaim>> GetAll();
        IDataResult<UserOperationClaim> Get(int id);


        IResult Add(UserOperationClaim entity);
        IResult Update(UserOperationClaim entity);
        IResult Delete(UserOperationClaim entity);
    }
    
}
