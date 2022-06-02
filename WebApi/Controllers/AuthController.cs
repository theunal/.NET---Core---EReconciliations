using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
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


        [HttpPost("register")]
        public ActionResult Register(UserRegisterDto dto)
        {
            var userExists = authService.UserExists(dto.Email);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
               // return BadRequest(userExists); bu şekilde de çalışabilir
            }
            
            var user = authService.Register(dto, dto.Password);
            var result = authService.CreateAccessToken(user.Data, 0);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public ActionResult Login(UserLoginDto dto)
        {
            var userToLogin = authService.Login(dto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }

            var result = authService.CreateAccessToken(userToLogin.Data, 0);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }


    }
}
