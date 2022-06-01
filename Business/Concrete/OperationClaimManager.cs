using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class OperationClaimManager : OperationClaimService<OperationClaim>
    {
        private readonly IOperationClaimDal operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            this.operationClaimDal = operationClaimDal;
        }
        public IResult Add(OperationClaim entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(OperationClaim entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<OperationClaim> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<OperationClaim>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(OperationClaim entity)
        {
            throw new NotImplementedException();
        }
    }
}
