using Core.Utilities.Results;
using Core.Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<UserOperationClaim>> GetAll(int userId, int companyId);
        IDataResult<UserOperationClaim> GetById(int id);


        IResult Add(UserOperationClaim entity);
        IResult Update(UserOperationClaim entity);
        IResult Delete(UserOperationClaim entity);
    }
    
}
