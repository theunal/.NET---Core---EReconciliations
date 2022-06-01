using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface AuthService
    {
        IDataResult<User> Register(UserRegisterDto dto, string password);
        IDataResult<User> Login(UserLoginDto dto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
