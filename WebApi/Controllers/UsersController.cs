using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpGet("getAllByCompanyId")]
        public IActionResult GetAll(int companyId)
        {
            var result = userService.GetUsersByCompanyId(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = userService.GetById(id);
            UserUpdateDto userUpdateDto = new UserUpdateDto
            {
                Name = result.Data.Name,
                Email = result.Data.Email,
                IsActive = result.Data.IsActive
            };
            if (result.Success)
            {
                return Ok(userUpdateDto);
            }
            return BadRequest();
        }

        [HttpPost("update")]
        public IActionResult Update(UserUpdateDto userUpdateDto)
        {
            var result = userService.UpdateByDto(userUpdateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}
