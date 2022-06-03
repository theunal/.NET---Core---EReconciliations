using Business.Abstract;
using Business.Const;
using Business.ValidationRules;
using Core.Aspects.Validation;
using Core.CrossCuttingConcerns.Validaiton;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService userService;
        private readonly IUserCompanyService userCompanyService;
        private readonly ICompanyService companyService;
        private readonly ITokenHelper tokenHelper;
        private readonly IMailParameterService mailParameterService;
        private readonly IMailService mailService;
        private readonly IMailTemplateService mailTemplateService;
        public AuthManager(
            IUserService userService,
            IUserCompanyService userCompanyService,
            ICompanyService companyService,
            ITokenHelper tokenHelper,
            IMailParameterService mailParameterService,
            IMailService mailService,
            IMailTemplateService mailTemplateService)
        {
            this.userService = userService;
            this.userCompanyService = userCompanyService;
            this.companyService = companyService;
            this.tokenHelper = tokenHelper;
            this.mailParameterService = mailParameterService;
            this.mailService = mailService;
            this.mailTemplateService = mailTemplateService;
        }



        public IDataResult<AccessToken> CreateAccessToken(User user, int companyId)
        {
            var claims = userService.GetClaims(user, companyId);
            var accessToken = tokenHelper.CreateToken(user, claims, companyId);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }








        [ValidationAspect(typeof(UserLoginValidator))]
        public IDataResult<User> Login(UserLoginDto dto)
        {
            ///* validation */
            //ValidationTool.Validate(new UserLoginValidator(), dto);
            ///* validation */

            var userToCheck = userService.GetByEmail(dto.Email);

            if (userToCheck is null) return new ErrorDataResult<User>(Messages.UserNotFound); // kullanıcı bulunamadı

            if (userToCheck.IsActive == false) return new ErrorDataResult<User>(Messages.UserNotActive); //aktif değil

            return HashingHelper.VerifyPasswordHash(dto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt) ?
                       new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin) : // giriş başarılı
                       new ErrorDataResult<User>(Messages.PasswordError); // şifre yanlış
        }


        [ValidationAspect(typeof(UserRegisterAndCompanyValidator))]
        public IDataResult<UserCompanyDto> Register(UserRegisterAndCompanyDto dto)
        { 
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(dto.UserRegisterDto.Password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Name = dto.UserRegisterDto.Name,
                Email = dto.UserRegisterDto.Email,
                AddedAt = DateTime.Now,
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = Guid.NewGuid().ToString(),
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            userService.Add(user);
            companyService.Add(dto.Company);
            companyService.AddUserCompany(user.Id, dto.Company.Id);

            UserCompanyDto userCompanyDto = new UserCompanyDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                AddedAt = user.AddedAt,
                CompanyId = dto.Company.Id,
                IsActive = user.IsActive,
                MailConfirm = false,
                MailConfirmDate = user.MailConfirmDate,
                MailConfirmValue = user.MailConfirmValue,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
            };

            SendConfirmEmail(user);

            return new SuccessDataResult<UserCompanyDto>(userCompanyDto, Messages.UserRegistered);
        }

        IResult SendConfirmEmail(User user)
        {
            string link = "https://localhost:7154/api/Auth/confirmUser?value=" + user.MailConfirmValue;
            string linkDescription = "Onayla";

            var mailTemplate = mailTemplateService.GetByTemplateName("string", 2002);

            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", "Kullanıcı Onay Maili");
            templateBody = templateBody.Replace("{{message}}", "Maili onaylamak için aşağıdaki butona tıklayın.");
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", linkDescription);


            var mailPamareter = mailParameterService.Get(2002);
            SendMailDto sendMailDto = new SendMailDto()
            {
                MailParameter = mailPamareter.Data,
                Email = user.Email,
                Subject = "Kullanıcı Onay Maili",
                Body = templateBody
            };


            user.MailConfirmDate = DateTime.Now;
            userService.Update(user);
            return mailService.SendMail(sendMailDto);
        }

        [ValidationAspect(typeof(UserRegisterSecondValidator))]
        public IDataResult<User> RegisterSecond(UserRegisterSecondDto dto)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(dto.Password, out passwordHash, out passwordSalt);

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
            companyService.AddUserCompany(user.Id, dto.CompanyId);
            
            SendConfirmEmail(user);
            
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



        public IResult Update(User entity)
        {
            userService.Update(entity);
            return new SuccessResult(Messages.UserMailConfirmSuccessfuly);
        }

        public IDataResult<User> GetById(int id)
        {
            return new SuccessDataResult<User>(userService.GetById(id).Data);
        }










        IResult IAuthService.SendConfirmEmail(User user)
        {
            if (user.MailConfirmDate.AddMinutes(1) <= DateTime.Now) // mail onayı isteğininden sonra 2dk geçmiş mi?
            {
                if (MailConfirm(user).Success)
                {
                    return SendConfirmEmail(user);
                }
                return MailConfirm(user); // mail confirm success değilse mesajını göster (kullanıcı bulunamadı vb.)
            }
            return new ErrorResult(Messages.MailConfirmNotExpired);


        }
        public IDataResult<User> GtByMailConfirmValue(string value)
        {
            var user = userService.GtByMailConfirmValue(value);
            return MailConfirm(user);
        }

        IDataResult<User> MailConfirm(User user)
        {
            if (user is null)
                return new ErrorDataResult<User>(user, Messages.UserNotFound); // kullanıcı bulunamadı
            else if (user.MailConfirm == true)
                return new ErrorDataResult<User>(user, Messages.MailHasAlreadyBeenConfirmed);// mail zaten gönderildi

            return new SuccessDataResult<User>(user);
        }

        public IDataResult<UserCompany> GetUserCompanyByUserId(int userId)
        {
            var userCompany = userCompanyService.GetById(userId);
            return new SuccessDataResult<UserCompany>(userCompany.Data);
        }
    }
}

