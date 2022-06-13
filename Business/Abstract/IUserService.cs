using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<List<UsersByCompanyDto>> GetUsersByCompanyId(int companyId);
        IDataResult<User> GetByEmailAddress(string email);



        IResult Add(User entity);

        IResult Delete(User entity);

        

        void Update(User entity);
        IResult UpdateByDto(UserUpdateDto dto);

        List<OperationClaim> GetClaims(User user, int companyId);
        User GetByEmail(string email);
        User GtByMailConfirmValue(string value);
        IDataResult<User> GetByValue(int id);
        IDataResult<User> GetById(int id);

        IDataResult<List<AdminCompaniesDto>> GetAdminCompanies(int adminUserId, int userUserId); 
        // adminin sahip oldugu şirketleri kullanıcıların şirketleri ile karşılaştırmak için
        // eğer kullanıcıda şirket varsa yeniden aynı şirket eklenmiyecek

    }
}
