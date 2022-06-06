using Business.Abstract;
using Business.Const;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            this.operationClaimDal = operationClaimDal;
        }




        // [SecuredOperation("Admin")]
        public IDataResult<List<OperationClaim>> GetAll()
        {
            var result = operationClaimDal.GetAll();
            return new SuccessDataResult<List<OperationClaim>>(result, Messages.OperationClaimsHasBeenBrought);
        }

        // [SecuredOperation("Admin")]
        public IDataResult<OperationClaim> GetById(int id)
        {
            var result = operationClaimDal.Get(x => x.Id == id);
            return new SuccessDataResult<OperationClaim>(result, Messages.OperationClaimHasBeenBrought);
        }








        // [SecuredOperation("Admin")]
        public IResult Add(OperationClaim entity)
        {
            operationClaimDal.Add(entity);
            return new SuccessResult(Messages.OperationClaimAdded);
        }

        // [SecuredOperation("Admin")]
        public IResult Update(OperationClaim entity)
        {
            operationClaimDal.Update(entity);
            return new SuccessResult(Messages.OperationClaimUpdated);
        }

        // [SecuredOperation("Admin")]
        public IResult Delete(OperationClaim entity)
        {
            operationClaimDal.Delete(entity);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

     
        

    }
}
