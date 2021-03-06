using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserOperationClaimsController : ControllerBase
    {
        private readonly IUserOperationClaimService userOperationClaimService;
        public UserOperationClaimsController(IUserOperationClaimService userOperationClaimService)
        {
            this.userOperationClaimService = userOperationClaimService;
        }




        [HttpGet("getAll")]
        public IActionResult GetAll(int userId, int companyId)
        {
            var result = userOperationClaimService.GetAll(userId, companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getAllDto")]
        public IActionResult GetAllDto(int userId, int companyId)
        {
            var result = userOperationClaimService.GetAllDto(userId, companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = userOperationClaimService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getUserOperationClaimByUserIdOperationClaimIdCompanyId")]
        public IActionResult GetUserOperationClaimByUserIdOperationClaimIdCompanyId
            (int userId, int operationClaimId, int companyId)
        {
            var result = userOperationClaimService.
                GetUserOperationClaimByUserIdOperationClaimIdCompanyId(userId, operationClaimId, companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }




        [HttpPost("add")]
        public IActionResult Add(UserOperationClaim userOperationClaim)
        {
            var result = userOperationClaimService.Add(userOperationClaim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(UserOperationClaim userOperationClaim)
        {
            var result = userOperationClaimService.Update(userOperationClaim);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("userOperationClaimUpdate")]
        public IActionResult UserOperationClaimUpdate(UserOperationClaimUpdateDto dto)
        {
            userOperationClaimService.UserOperationClaimUpdate(dto);
            return Ok();
        }

    }
}
