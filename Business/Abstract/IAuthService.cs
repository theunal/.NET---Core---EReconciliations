using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<UserCompanyDto> Register(UserRegisterAndCompanyDto dto);
        IDataResult<User> RegisterSecond(UserRegisterSecondDto dto);
        IDataResult<User> Login(UserLoginDto dto);
        IDataResult<User> GtByMailConfirmValue(string value);
        IResult UserExists(string email);
        IResult CompanyExists(Company company);
        IDataResult<AccessToken> CreateAccessToken(User user, int companyId);

        IResult Update(User entity);



        
        IDataResult<User> GetById(int id);
        IDataResult<User> GetByEmail(string email);
        IResult SendConfirmEmail2(User user);
        IResult ForgotPassword(User user);
        IResult UpdatePassword(User entity);
        IDataResult<User> GtByMailConfirmValueForPasswordReset(string value);




        IDataResult<UserCompany> GetUserCompanyByUserId(int userId);
    }
}
