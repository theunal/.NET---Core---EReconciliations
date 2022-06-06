using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
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
                    return Ok(result.Message);
                }
                return BadRequest(result.Message);
            }
            return BadRequest(user.Message);
        }

        [HttpGet("confirmUserAgain")]
        public IActionResult ConfirmUserAgain(int id)
        {
            var user = authService.GetById(id).Data;
            var result = authService.SendConfirmEmail(user);

            
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

    }
}
