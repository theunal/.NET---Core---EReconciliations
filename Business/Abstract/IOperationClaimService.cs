using Core.Entities.Concrete;
using Core.Utilities.Results;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<List<OperationClaim>> GetAll();
        IDataResult<OperationClaim> GetById(int id);
        IResult GetByOperationClaimName(string name);


        IResult Add(OperationClaim entity);
        IResult Update(OperationClaim entity);
        IResult Delete(OperationClaim entity);
    }
}
