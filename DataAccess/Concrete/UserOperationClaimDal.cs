using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Dtos;

namespace DataAccess.Concrete
{
    public class UserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, DataContext>, IUserOperationClaimDal
    {
        public List<UserOperationClaimDto> GetAllDto(int userId, int companyId)
        {
            using var context = new DataContext();
            var result = from userOperationClaim in context.UserOperationClaims.Where(u => u.UserId == userId && u.CompanyId == companyId)
                         join operationClaim in context.OperationClaims
                         on userOperationClaim.OperationClaimId equals operationClaim.Id


                         select new UserOperationClaimDto
                         {
                             Id = userOperationClaim.Id, // operation claim id
                             UserId = userId,
                             CompanyId = companyId,
                             OperationClaimId = operationClaim.Id,

                             OperationClaimName = operationClaim.Name,
                             OperationClaimDescription = operationClaim.Description,
                             IsActive = userOperationClaim.IsActive
                         };
            return result.ToList();
        }

    }
}
