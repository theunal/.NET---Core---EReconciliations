using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService companyService;
        public CompaniesController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }


        [HttpGet]
        public IActionResult GetList()
        {
            var companies = companyService.GetAll();
            if (companies.Success)
            {
                return Ok(companies);
            }
            return BadRequest(companies.Message);
        }

        [HttpPost]
        public IActionResult Add([FromBody] Company company)
        {
            var result = companyService.Add(company);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        
        }




    }
}
