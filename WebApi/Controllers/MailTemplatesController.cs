using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailTemplatesController : ControllerBase
    {
        private readonly IMailTemplateService mailTemplateService;
        public MailTemplatesController(IMailTemplateService mailTemplateService)
        {
            this.mailTemplateService = mailTemplateService;
        }


        [HttpPost]
        public IActionResult Add(MailTemplate mailTemplate)
        {
            var result = mailTemplateService.Add(mailTemplate);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        
    }
}
