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
        IDataResult<User> RegisterSecond(UserRegisterDto dto, string password);
        IDataResult<User> Login(UserLoginDto dto);
        IResult UserExists(string email);
        IResult CompanyExists(Company company);
        IDataResult<AccessToken> CreateAccessToken(User user, int companyId);
    }
}
