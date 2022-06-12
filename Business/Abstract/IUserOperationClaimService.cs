using Core.Utilities.Results;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<UserOperationClaim>> GetAll(int userId, int companyId);
        IDataResult<List<UserOperationClaimDto>> GetAllDto(int userId, int companyId);
        IDataResult<UserOperationClaim> GetById(int id);




        IDataResult<UserOperationClaim> GetUserOperationClaimByUserIdOperationClaimIdCompanyId
            (int userId, int operationClaimId, int companyId);
        void UserOperationClaimUpdate (UserOperationClaimUpdateDto userOperationClaimUpdateDto);





        IResult Add(UserOperationClaim entity);
        IResult Update(UserOperationClaim entity);
        IResult Delete(UserOperationClaim entity);




        void DeleteByUserIdAndCompanyId(int userId, int companyId);
    }
    
}
