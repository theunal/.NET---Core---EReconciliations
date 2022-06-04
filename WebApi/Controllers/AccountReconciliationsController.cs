using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReconciliationsController : ControllerBase
    {
        private readonly IAccountReconciliationService accountReconciliationService;
        public AccountReconciliationsController(IAccountReconciliationService accountReconciliationService)
        {
            this.accountReconciliationService = accountReconciliationService;
        }


        [HttpPost("add")]
        public IActionResult Add(AccountReconciliation accountReconciliation)
        {
            var result = accountReconciliationService.Add(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(AccountReconciliation accountReconciliation)
        {
            var result = accountReconciliationService.Update(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(AccountReconciliation accountReconciliation)
        {
            var result = accountReconciliationService.Delete(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }



        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = accountReconciliationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getAllByCompanyId")]
        public IActionResult GetAll(int companyId)
        {
            var result = accountReconciliationService.GetAll(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }



        [HttpPost("addByExcel")]
        public IActionResult AddByExcel(IFormFile file, int companyId)
        {
            if (file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + ".xlsx";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "Content", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                    stream.Flush();
                }

                AccountReconciliationDto dto = new AccountReconciliationDto()
                {
                    CompanyId = companyId,
                    filePath = path
                };
                var result = accountReconciliationService.AddByExcel(dto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result.Message);
            }
            return BadRequest("Dosya seçilmedi.");
        }








    }
}
