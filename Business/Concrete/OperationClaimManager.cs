using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Core.Aspects.Performance;
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
            if (result is not null)
            {
                return new SuccessDataResult<OperationClaim>(result, Messages.OperationClaimHasBeenBrought);
            }
            return new ErrorDataResult<OperationClaim>(Messages.OperationClaimNotFound);
        }
        
        public IResult GetByOperationClaimName(string name)
        {
            var result = operationClaimDal.Get(x => x.Name == name);
            if (result is not null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }







        //[PerformanceAspect(3)]
       // [SecuredOperation("admin")]
        public IResult Add(OperationClaim entity)
        {
            var result = GetByOperationClaimName(entity.Name);
            if (result.Success == false)
            {
                operationClaimDal.Add(entity);
                return new SuccessResult(Messages.OperationClaimAdded);
            }
            return new ErrorDataResult<OperationClaim>(Messages.OperationClaimAlreadyExists);
        }
        
        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        public IResult Update(OperationClaim entity)
        {
            var result = GetById(entity.Id);
            if (result.Success)
            {
                operationClaimDal.Update(entity);
                return new SuccessResult(Messages.OperationClaimUpdated);
            }
            return new ErrorResult(Messages.OperationClaimNotFound);
        
        }

        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        public IResult Delete(OperationClaim entity)
        {
            var result = GetById(entity.Id);
            if (result.Success)
            {
                operationClaimDal.Delete(entity);
                return new SuccessResult(Messages.OperationClaimDeleted);
            }
            return new ErrorResult(Messages.OperationClaimNotFound);
        }

    }
}
