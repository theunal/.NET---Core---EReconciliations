using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
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

        [HttpGet("getCompanyList")]
        public IActionResult GetList()
        {
            var companies = companyService.GetAll();
            if (companies.Success)
            {
                return Ok(companies);
            }
            return BadRequest(companies.Message);
        }

        [HttpPost("addCompanyToUser")]
        public IActionResult Add(CompanyDto dto)
        {
            var result = companyService.AddCompanyUser(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpGet("getCompany")]
        public IActionResult GetCompany(int id)
        {
            var result = companyService.GetById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("updateCompany")]
        public IActionResult UpdateCompany(Company company)
        {
            var result = companyService.Update(company);
            return result.Success ? Ok(result) : BadRequest(result.Message);
        }




    }
}
