using Business.Abstract;
using Business.Const;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            this.userOperationClaimDal = userOperationClaimDal;
        }




        // [SecuredOperation("Admin")]
        public IDataResult<List<UserOperationClaim>> GetAll(int userId, int companyId)
        {
            var result = userOperationClaimDal.GetAll(u => u.UserId == userId && u.CompanyId == companyId);
            return new SuccessDataResult<List<UserOperationClaim>>(result, Messages.UserOperationClaimsHasBeenBrought);
        }

        // [SecuredOperation("Admin")]
        public IDataResult<UserOperationClaim> GetById(int id)
        {
            var result = userOperationClaimDal.Get(x => x.Id == id);
            return new SuccessDataResult<UserOperationClaim>(result, Messages.UserOperationClaimHasBeenBrought);
        }








        // [SecuredOperation("Admin")]
        public IResult Add(UserOperationClaim entity)
        {
            userOperationClaimDal.Add(entity);
            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        // [SecuredOperation("Admin")]
        public IResult Update(UserOperationClaim entity)
        {
            userOperationClaimDal.Update(entity);
            return new SuccessResult(Messages.UserOperationClaimUpdated);
        }

        // [SecuredOperation("Admin")]
        public IResult Delete(UserOperationClaim entity)
        {
            userOperationClaimDal.Delete(entity);
            return new SuccessResult(Messages.UserOperationClaimDeleted);
        }

     
    }

}
