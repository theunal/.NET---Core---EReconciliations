using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailParametersController : ControllerBase
    {
        private readonly IMailParameterService mailParameterService;
        public MailParametersController(IMailParameterService mailParameterService)
        {
            this.mailParameterService = mailParameterService;
        }


        [HttpGet("getMailParameters")]
        public IActionResult GetMailParameters(int companyId)
        {
            var result = mailParameterService.Get(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        [HttpPost("updateMailParameter")]
        public IActionResult AddMailParameter(MailParameter mailParameter)
        {
            var result = mailParameterService.Update(mailParameter);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        
    }
}
