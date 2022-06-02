using Business.Abstract;
using Business.Const;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService userService;
        private readonly ICompanyService companyService;
        private readonly ITokenHelper tokenHelper;
        private readonly IMailParameterService mailParameterService;
        private readonly IMailService mailService;
        public AuthManager(
            IUserService userService,
            ICompanyService companyService,
            ITokenHelper tokenHelper,
            IMailParameterService mailParameterService,
            IMailService mailService)
        {
            this.userService = userService;
            this.companyService = companyService;
            this.tokenHelper = tokenHelper;
            this.mailParameterService = mailParameterService;
            this.mailService = mailService;
        }

     

        public IDataResult<AccessToken> CreateAccessToken(User user, int companyId)
        {
            var claims = userService.GetClaims(user, companyId);
            var accessToken = tokenHelper.CreateToken(user, claims, companyId);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }

        public IDataResult<User> Login(UserLoginDto dto)
        {
            var userToCheck = userService.GetByEmail(dto.Email);
            if (userToCheck is null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
            
            if (!HashingHelper.VerifyPasswordHash(dto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public IDataResult<UserCompanyDto> Register(UserRegisterDto dto, string password, Company company)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                AddedAt = DateTime.Now,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            
            userService.Add(user);
            companyService.Add(company);
            companyService.AddUserCompany(user.Id, company.Id);
            UserCompanyDto userCompanyDto = new UserCompanyDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                AddedAt = user.AddedAt,
                CompanyId = company.Id,
                IsActive = user.IsActive,
                MailConfirm = user.MailConfirm,
                MailConfirmDate = user.MailConfirmDate,
                MailConfirmValue = user.MailConfirmValue,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
            };
            
            var mailPamareter = mailParameterService.Get(2002);
            SendMailDto sendMailDto = new SendMailDto()
            {
                MailParameter = mailPamareter.Data,
                Email = dto.Email,
                Subject = "Kullanıcı Onay Maili",
                Body = "Maili onaylamak için aşağıdaki butona tıklayın."       
            };

            mailService.SendMail(sendMailDto);

            return new SuccessDataResult<UserCompanyDto>(userCompanyDto, Messages.UserRegistered);
        }

        public IDataResult<User> RegisterSecond(UserRegisterDto dto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                AddedAt = DateTime.Now,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IResult UserExists(string email)
        {
            if (userService.GetByEmail(email) is not null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public IResult CompanyExists(Company company)
        {
            var result = companyService.Get(company);

            if (result.Success is false)
            {
                return new ErrorResult(Messages.CompanyAlreadyExists);
            }
            return new SuccessResult();
        }

    }
}

