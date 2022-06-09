using Business.Abstract;
using Core.Utilities.Security.Hashing;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IForgotPasswordService forgotPasswordService;
        public AuthController(IAuthService authService, IForgotPasswordService forgotPasswordService)
        {
            this.authService = authService;
            this.forgotPasswordService = forgotPasswordService;
        }


        [HttpPost("login")]
        public ActionResult Login(UserLoginDto dto)
        {
            var userToLogin = authService.Login(dto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var userCompany = authService.GetUserCompanyByUserId(userToLogin.Data.Id).Data;

            var result = authService.CreateAccessToken(userToLogin.Data, userCompany.CompanyId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public ActionResult Register(UserRegisterAndCompanyDto dto)
        {
            var userExists = authService.UserExists(dto.UserRegisterDto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var companyExists = authService.CompanyExists(dto.Company);
            if (!companyExists.Success)
            {
                return BadRequest(companyExists.Message);
            }



            var registerResult = authService.Register(dto);

            var result = authService.CreateAccessToken(registerResult.Data, registerResult.Data.CompanyId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("registerSecond")]
        public ActionResult RegisterSecond(UserRegisterSecondDto dto)
        {
            // öncesinde mevcut bir şirket bulun
            var userExists = authService.UserExists(dto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
                // return BadRequest(userExists); bu şekilde de çalışabilir
            }

            var user = authService.RegisterSecond(dto);
            var result = authService.CreateAccessToken(user.Data, dto.CompanyId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("confirmUser")]
        public IActionResult ConfirmUser(string value)
        {
            var user = authService.GtByMailConfirmValue(value);
            if (user.Success)
            {
                user.Data.MailConfirm = true;
                user.Data.MailConfirmDate = DateTime.Now;
                var result = authService.Update(user.Data);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(user);
        }



        [HttpGet("confirmUserAgain")]
        public IActionResult ConfirmUserAgain(string email)
        {
            var get = authService.GetByEmail(email); // kullanıcı var mı?
            if (get.Success)
            {
                var result = authService.SendConfirmEmail2(get.Data);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest(get);
        }














        /* ilk yaptıgım şifremi unuttum kısmı */
        //[HttpPost("forgotPassword")]
        //public IActionResult ForgotPassword(string email)
        //{
        //    var get = authService.GetByEmail(email); //  kullanıcı yı getir
        //    if (get.Success)
        //    {
        //        var result = authService.ForgotPassword(get.Data);
        //        if (result.Success)
        //        {
        //            return Ok(result);
        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest(get);
        //}

        //[HttpGet("passwordReset")]
        //public IActionResult PasswordReset(string value, string password)
        //{
        //    var user = authService.GtByMailConfirmValueForPasswordReset(value);
        //    if (user.Success)
        //    {
        //        byte[] passwordHash, passwordSalt;
        //        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        //        user.Data.PasswordHash = passwordHash;
        //        user.Data.PasswordSalt = passwordSalt;

        //        var result = authService.UpdatePassword(user.Data);
        //        if (result.Success)
        //        {
        //            return Ok(result);
        //        }
        //        return BadRequest(result);
        //    }
        //    return BadRequest(user);
        //}
        /* ilk yaptıgım şifremi unuttum kısmı */



        /* ikinci şifremi unuttum */
        [HttpPost("forgotPassword")]
        public IActionResult ForgotPassword2(string email)
        {
            var user = authService.GetByEmail(email); //  kullanıcı yı getir

            if (!user.Success)
            {
                return BadRequest(user);
            }

            var list = forgotPasswordService.GetAllById(user.Data.Id).Data;
            foreach (var item in list)
            {
                item.IsActive = false;
                forgotPasswordService.Update(item);
            }

            var forgotPassword = forgotPasswordService.Add(user.Data).Data;

            var result = authService.PasswordReset(user.Data, forgotPassword.Value);

            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("forgotPasswordLinkCheck")]
        public IActionResult ForgotPasswordLinkCheck(string value)
        {
            var result = forgotPasswordService.GetForgotPasswordByValue(value);
            if (result.Data is null) return BadRequest("Şifre Sıfırlama linki Geçersiz.");

            var date = DateTime.Now.AddHours(-1);
            var now = DateTime.Now;

            if (result.Data.SendDate <= now && result.Data.SendDate >= date)
                return Ok(true);

            return BadRequest("Şifre Sıfırlama linki Geçersiz.");
        }

        [HttpGet("passwordReset2")]
        public IActionResult PasswordReset2(string value, string password)
        {
            var result = forgotPasswordService.GetForgotPasswordByValue(value);
           
            if (result.Success)
            {
                result.Data.IsActive = false; // kullanıcı email linkine tıkladıysa artık linki pasif oldu
                forgotPasswordService.Update(result.Data);



                var user = authService.GetById(result.Data.UserId).Data;
                
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                var updated = authService.UpdatePassword(user);
                if (updated.Success)
                {
                    return Ok(updated);
                }
                return BadRequest(updated);
            }
            return BadRequest("Şifre sıfırlama linkinin süresi dolmuş.");
        }
        /* ikinci şifremi unuttum */





    }
}
