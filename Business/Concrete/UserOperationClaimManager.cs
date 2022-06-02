using Business.Abstract;
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

        
        public IResult Add(UserOperationClaim entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(UserOperationClaim entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<UserOperationClaim> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<UserOperationClaim>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(UserOperationClaim entity)
        {
            throw new NotImplementedException();
        }
    }

}
