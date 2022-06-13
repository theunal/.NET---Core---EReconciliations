using Business.Abstract;
using Business.Const;
using Business.ValidationRules;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Validation;
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
        private readonly IUserCompanyService userCompanyService;
        private readonly ICompanyService companyService;
        private readonly ITokenHelper tokenHelper;
        private readonly IMailParameterService mailParameterService;
        private readonly IMailService mailService;
        private readonly IMailTemplateService mailTemplateService;
        private readonly IOperationClaimService operationClaimService;
        private readonly IUserOperationClaimService userOperationClaimService;
        public AuthManager(
            IUserService userService,
            IUserCompanyService userCompanyService,
            ICompanyService companyService,
            ITokenHelper tokenHelper,
            IMailParameterService mailParameterService,
            IMailService mailService,
            IMailTemplateService mailTemplateService,
            IOperationClaimService operationClaimService,
            IUserOperationClaimService userOperationClaimService)
        {
            this.userService = userService;
            this.userCompanyService = userCompanyService;
            this.companyService = companyService;
            this.tokenHelper = tokenHelper;
            this.mailParameterService = mailParameterService;
            this.mailService = mailService;
            this.mailTemplateService = mailTemplateService;
            this.operationClaimService = operationClaimService;
            this.userOperationClaimService = userOperationClaimService;
        }


        public IDataResult<User> GetById(int id)
        {
             return new SuccessDataResult<User>(userService.GetById(id).Data);
        }

        public IDataResult<User> GetByEmail(string email)
        {
            var user = userService.GetByEmailAddress(email);
            if (user.Success)
            {
                return new SuccessDataResult<User>(user.Data);
            }
            return new ErrorDataResult<User>(user.Data, Messages.UserNotFound);
        }

        public IDataResult<User> GtByMailConfirmValue(string value)
        {
            var user = userService.GtByMailConfirmValue(value);
            return MailConfirm(user);
        }
        public IDataResult<UserCompany> GetUserCompanyByUserId(int userId)
        {
            var userCompany = userCompanyService.GetById(userId);
            return new SuccessDataResult<UserCompany>(userCompany.Data);
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






        [ValidationAspect(typeof(UserLoginValidator))]
        public IDataResult<User> Login(UserLoginDto dto)
        {

            var userToCheck = userService.GetByEmail(dto.Email);

            if (userToCheck is null) return new ErrorDataResult<User>(Messages.UserNotFound); // kullanıcı bulunamadı

            if (userToCheck.IsActive == false) return new ErrorDataResult<User>(Messages.UserNotActive); //aktif değil

            if (userToCheck.MailConfirm == false) return new ErrorDataResult<User>(Messages.MailNotConfirmed);

            return HashingHelper.VerifyPasswordHash(dto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt) ?
                       new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin) : // giriş başarılı
                       new ErrorDataResult<User>(Messages.PasswordError); // şifre yanlış
        }


        [TransactionScopeAspect]
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
                IsActive = true,
                MailConfirm = false,
                MailConfirmDate = user.MailConfirmDate,
                MailConfirmValue = user.MailConfirmValue,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
            };

            var operationClaims = operationClaimService.GetAll();
            foreach (var operationClaim in operationClaims.Data)
            {
                if (operationClaim.Name != "admin" && !operationClaim.Name.Contains("mail") &&
                    !operationClaim.Name.Contains("Claim"))
                {
                    UserOperationClaim userOperationClaim = new UserOperationClaim
                    {
                        CompanyId = dto.Company.Id,
                        UserId = user.Id,
                        OperationClaimId = operationClaim.Id,
                        AddedAt = DateTime.Now,
                        IsActive = true
                    };
                    userOperationClaimService.Add(userOperationClaim);
                }
            }

            SendConfirmEmail(user);
            return new SuccessDataResult<UserCompanyDto>(userCompanyDto, Messages.UserRegistered);
        }



        [TransactionScopeAspect]
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

            var operationClaims = operationClaimService.GetAll();
            foreach (var operationClaim in operationClaims.Data)
            {
                if (operationClaim.Name != "admin" && !operationClaim.Name.Contains("mail") &&
                    !operationClaim.Name.Contains("Claim"))
                {
                    UserOperationClaim userOperationClaim = new UserOperationClaim
                    {
                        CompanyId = dto.CompanyId,
                        UserId = user.Id,
                        OperationClaimId = operationClaim.Id,
                        AddedAt = DateTime.Now,
                        IsActive = true
                    };
                    userOperationClaimService.Add(userOperationClaim);
                }
            }

            SendConfirmEmail(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }
















        public IResult Update(User entity)
        {
            userService.Update(entity);
            return new SuccessResult(Messages.UserMailConfirmSuccessfuly);
        }

        public IResult UpdatePassword(User entity)
        {
            userService.Update(entity);
            return new SuccessResult(Messages.PasswordUpdated);
        }


        void SendConfirmEmail(User user)
        {
           // string link = "https://localhost:7154/api/Auth/confirmUser?value=" + user.MailConfirmValue;

            string link = "http://localhost:4200/mailConfirm/" + user.MailConfirmValue;


            var mailTemplate = mailTemplateService.GetByTemplateName("string", 9028);

            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", "Kullanıcı Onay Maili");
            templateBody = templateBody.Replace("{{message}}", "Maili onaylamak için aşağıdaki butona tıklayın.");
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", "Onayla");


            var mailPamareter = mailParameterService.Get(9028);
            SendMailDto sendMailDto = new SendMailDto()
            {
                MailParameter = mailPamareter.Data,
                Email = user.Email,
                Subject = "Kullanıcı Onay Maili",
                Body = templateBody
            };

            mailService.SendMail(sendMailDto);

            user.MailConfirmDate = DateTime.Now;
            userService.Update(user);

        }

        public IResult SendConfirmEmail2(User user)
        {
            if (MailConfirm(user).Success)
            {
                if (user.MailConfirmDate.AddMinutes(1) <= DateTime.Now) // mail onayı isteğininden sonra 2dk geçmiş mi?
                {
                    SendConfirmEmail(user);
                    return new SuccessResult(Messages.MailSentSuccessfully); // mail başarıyla gönderildi
                }
                return new ErrorResult(Messages.MailConfirmNotExpired);
            }
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
        public IDataResult<AccessToken> CreateAccessToken(User user, int companyId)
        {
            var claims = userService.GetClaims(user, companyId);
            var companyName = companyService.GetById(companyId).Data.Name;
            var accessToken = tokenHelper.CreateToken(user, claims, companyId, companyName);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.SuccessfulLogin);
            // giriş tokeni 
        }

        public IResult ForgotPassword(User user)
        {
            ForgotPasswordEmail(user);
            return new SuccessResult(Messages.PasswordReset);
        }

        void ForgotPasswordEmail(User user)
        {
            string link = "http://localhost:4200/passwordReset/" + user.MailConfirmValue;
            var mailTemplate = mailTemplateService.GetByTemplateName("string", 9028);

            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", "Şifre Yenileme Maili");
            templateBody = templateBody.Replace("{{message}}", "Şifrenizi yenilemek için aşağıdaki butona tıklayın.");
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", "Onayla");

            var mailPamareter = mailParameterService.Get(9028);
            SendMailDto sendMailDto = new SendMailDto()
            {
                MailParameter = mailPamareter.Data,
                Email = user.Email,
                Subject = "Şifre Yenileme Maili",
                Body = templateBody
            };

            mailService.SendMail(sendMailDto);
            user.MailConfirmDate = DateTime.Now;
            userService.Update(user);
        }

        public IDataResult<User> GtByMailConfirmValueForPasswordReset(string value)
        {
            var user = userService.GtByMailConfirmValue(value);
            if (user is not null)
            {
                return new SuccessDataResult<User>(user, Messages.UserHasBeenBrought);
            }
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        public IDataResult<ForgotPassword> GetForgotPasswordByValue(string value)
        {
            // bunu kullanmadım sonra silcem
            return new SuccessDataResult<ForgotPassword>();
        }

        public IDataResult<ForgotPassword> AddForgotPassword(User user)
        {
            throw new NotImplementedException();
        }

        public IResult PasswordReset(User user, string value)
        {
            ForgotPasswordEmail2(user, value);
            return new SuccessResult(Messages.PasswordReset);
        }
        void ForgotPasswordEmail2(User user, string value)
        {
            string link = "http://localhost:4200/forgotPasswordLinkCheck/" + value;
            var mailTemplate = mailTemplateService.GetByTemplateName("string", 9028);

            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", "Şifre Yenileme Maili");
            templateBody = templateBody.Replace("{{titleMessage}}", "Şifrenizi yenilemek için aşağıdaki butona tıklayın.");
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", "Onayla");

            var mailPamareter = mailParameterService.Get(9028);
            SendMailDto sendMailDto = new SendMailDto()
            {
                MailParameter = mailPamareter.Data,
                Email = user.Email,
                Subject = "Şifre Yenileme Maili",
                Body = templateBody
            };

            mailService.SendMail(sendMailDto);
            user.MailConfirmDate = DateTime.Now;
            userService.Update(user);
        }

    }
}

