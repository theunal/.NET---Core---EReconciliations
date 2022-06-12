using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class UserRelationshipManager : IUserRelationshipService
    {
        private readonly IUserRelationshipDal userRelationshipDal;
        public UserRelationshipManager(IUserRelationshipDal userRelationshipDal)
        {
            this.userRelationshipDal = userRelationshipDal;
        }


        
        public IDataResult<List<UserRelationshipDto>> GetAllByAdminUserId(int adminUserId)
        {
            var result = userRelationshipDal.GetAllDto(adminUserId);
            return new SuccessDataResult<List<UserRelationshipDto>>(result);
        }

        public IDataResult<UserRelationshipDto> GetByUserUserId(int userUserId)
        {
            var result = userRelationshipDal.GetByUserUserId(userUserId);
            return new SuccessDataResult<UserRelationshipDto>(result);
        }








        
        public void Add(UserRelationship userRelationship)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserRelationship userRelationship)
        {
            throw new NotImplementedException();
        }
        public void Update(UserRelationship userRelationship)
        {
            throw new NotImplementedException();
        }
    }
}
