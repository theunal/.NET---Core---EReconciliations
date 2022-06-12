using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserRelationshipService
    {
        IDataResult<List<UserRelationshipDto>> GetAllByAdminUserId(int adminUserId);
        IDataResult<UserRelationshipDto> GetByUserUserId(int userUserId);


        
        void Add(UserRelationship userRelationship);
        void Update(UserRelationship userRelationship);
        void Delete(UserRelationship userRelationship);
    }
}
