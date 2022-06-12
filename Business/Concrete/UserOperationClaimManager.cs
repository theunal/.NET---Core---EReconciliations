using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            this.userOperationClaimDal = userOperationClaimDal;
        }




        [SecuredOperation("userOperationClaim.getall,admin")]
        public IDataResult<List<UserOperationClaim>> GetAll(int userId, int companyId)
        {
            var result = userOperationClaimDal.GetAll(u => u.UserId == userId && u.CompanyId == companyId);
            return new SuccessDataResult<List<UserOperationClaim>>(result, Messages.UserOperationClaimsHasBeenBrought);
        }

        public IDataResult<List<UserOperationClaimDto>> GetAllDto(int userId, int companyId)
        {
            var result = userOperationClaimDal.GetAllDto(userId, companyId);
            return new SuccessDataResult<List<UserOperationClaimDto>>(result);
        }

        //[SecuredOperation("userOperationClaim.get,admin")]
        public IDataResult<UserOperationClaim> GetById(int id)
        {
            var result = userOperationClaimDal.Get(x => x.Id == id);
            if (result is not null)
            {
                return new SuccessDataResult<UserOperationClaim>(result, Messages.UserOperationClaimsHasBeenBrought);
            }
            return new ErrorDataResult<UserOperationClaim>(Messages.UserOperationClaimsNotFound);
        }

        public IDataResult<UserOperationClaim> GetUserOperationClaimByUserIdOperationClaimIdCompanyId
            (int userId, int operationClaimId, int companyId)
        {
            var result = userOperationClaimDal.Get
                (u => u.UserId == userId && u.OperationClaimId == operationClaimId && u.CompanyId == companyId);
            return new SuccessDataResult<UserOperationClaim>(result);
        }
        public void UserOperationClaimUpdate(UserOperationClaimUpdateDto dto)
        {
            var result = userOperationClaimDal.Get
                (u => u.UserId == dto.UserId && u.OperationClaimId == dto.OperationClaimId && u.CompanyId == dto.CompanyId);
            result.IsActive = result.IsActive ? false : true;
            Update(result);
        }






        // [SecuredOperation("userOperationClaim.add,admin")]
        public IResult Add(UserOperationClaim entity)
        {
            userOperationClaimDal.Add(entity);
            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        [SecuredOperation("userOperationClaim.update,admin")]
        public IResult Update(UserOperationClaim entity)
        {
            var result = GetById(entity.Id);
            if (result.Success)
            {
                userOperationClaimDal.Update(entity);
                return new SuccessResult(Messages.UserOperationClaimUpdated);
            }
            return result;
        }

        [SecuredOperation("userOperationClaim.delete,admin")]
        public IResult Delete(UserOperationClaim entity)
        {
            var result = GetById(entity.Id);
            if (result.Success)
            {
                userOperationClaimDal.Delete(entity);
                return new SuccessResult(Messages.UserOperationClaimDeleted);
            }
            return result;

        }

     
    }

}
