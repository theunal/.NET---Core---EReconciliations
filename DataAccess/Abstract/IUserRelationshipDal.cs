using Core.DataAccess;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Abstract
{
    public interface IUserRelationshipDal : IEntityRepository<UserRelationship>
    {
        List<UserRelationshipDto> GetAllDto(int adminUserId);

        UserRelationshipDto GetByUserUserId(int userUserId);
    }
}
