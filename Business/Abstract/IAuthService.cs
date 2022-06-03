using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserCompanyDto> Register(UserRegisterDto dto, string password, Company company);
        IDataResult<User> RegisterSecond(UserRegisterDto dto, string password, int companyId);
        IDataResult<User> Login(UserLoginDto dto);
        IDataResult<User> GtByMailConfirmValue(string value);
        IResult UserExists(string email);
        IResult CompanyExists(Company company);
        IDataResult<AccessToken> CreateAccessToken(User user, int companyId);

        IResult Update(User entity);



        
        IDataResult<User> GetById(int id);
        IResult SendConfirmEmail(User user);
        IDataResult<UserCompany> GetUserCompanyByUserId(int userId);
    }
}
