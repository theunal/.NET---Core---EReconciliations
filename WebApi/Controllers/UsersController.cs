using Business.Abstract;
using Entities.Concrete;
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
        private readonly IUserRelationshipService userRelationshipService;
        private readonly IUserOperationClaimService userOperationClaimService;
        private readonly IUserCompanyService userCompanyService;
        private readonly ICompanyService companyService;
        private readonly IUserThemeService userThemeService;
        public UsersController(IUserService userService, IUserRelationshipService userRelationshipService, IUserOperationClaimService userOperationClaimService, IUserCompanyService userCompanyService, ICompanyService companyService, IUserThemeService userThemeService)
        {
            this.userService = userService;
            this.userRelationshipService = userRelationshipService;
            this.userOperationClaimService = userOperationClaimService;
            this.userCompanyService = userCompanyService;
            this.companyService = companyService;
            this.userThemeService = userThemeService;
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

        [HttpGet("getAllUserRelationshipByAdminUserId")]
        public IActionResult GetAllUserRelationshipByAdminUserId(int adminUserId)
        {
            var result = userRelationshipService.GetAllByAdminUserId(adminUserId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getAllCompanyAdminUserId")]
        public IActionResult GetAllCompanyAdminUserId(int adminUserId)
        {
            var result = companyService.GetAllCompanyAdminUserId(adminUserId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }



        // değiştircem
        [HttpGet("getUserRelationshipByUserUserId")]
        public IActionResult GetUserRelationshipByUserUserId(int userUserId)
        {
            var result = userRelationshipService.GetByUserUserId(userUserId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
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

        [HttpGet("getByValue")]
        public IActionResult GetByValue(string value)
        {
            var result = userService.GtByMailConfirmValue(value);
            if (result is not null)
            {
                return Ok(result.Id);
            }
            return BadRequest("Kullanıcı Bulunamadı");
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



        [HttpPost("deleteByUserIdAndCompanyId")]
        public IActionResult DeleteByUserIdAndCompanyId(DeleteRelationshipBetweenUserAndCompanyDto dto)
        {
            // operation claimleri siliyor sonra user company ilişkisini siliyor
            userOperationClaimService.DeleteByUserIdAndCompanyId(dto.UserId, dto.CompanyId);
            var result = userCompanyService.DeleteByUserIdAndCompanyId(dto.UserId, dto.CompanyId);
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }




        [HttpGet("getAdminCompanies")]
        public IActionResult GetAdminCompanies(int adminUserId, int userUserId)
        {
            var result = userService.GetAdminCompanies(adminUserId, userUserId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }








        [HttpGet("getUserTheme")]
        public IActionResult GetUserTheme(int userId)
        {
            var result = userThemeService.GetUserThemeByUserId(userId);
            if (result.Success) return Ok(result);
            
            var createdUserTheme = userThemeService.CreateDefaultUserTheme(userId);
            if (createdUserTheme.Success) return Ok(createdUserTheme);

            return BadRequest();
        }  
        
        
        [HttpPost("changeUserTheme")]
        public IActionResult ChangeUserTheme(UserTheme userTheme)
        {
            var result = userThemeService.Update(userTheme);
            return Ok(result);
        }

    }
}
