using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user, int companyId);
        List<UsersByCompanyDto> GetUsersByCompanyId(int companyId);
    }
}
