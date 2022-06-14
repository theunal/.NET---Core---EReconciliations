using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.Excel;
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

        [HttpGet("getAllDto")]
        public IActionResult GetAllDto(int companyId)
         {
            var result = accountReconciliationService.GetAllDto(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("sendReconciliationMail")]
        public IActionResult SendReconciliationMail(AccountReconciliationDto dto)
        {
            var result = accountReconciliationService.SendReconciliationMail(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        
        [HttpGet("getByCode")] // guidi gönderiyoruz ama onaylandıktan sonra db de true ya çevirmedim düzelticem
        public IActionResult getByCode(string code)
        {
            var result = accountReconciliationService.GetByCode(code);
            if (result.Success)
            {
                return Ok(result.Message);
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

      













        [HttpPost("add")]
        public IActionResult Add(AccountReconciliation accountReconciliation)
        {
            var result = accountReconciliationService.Add(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(AccountReconciliation accountReconciliation)
        {
            var result = accountReconciliationService.Update(accountReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = accountReconciliationService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
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

                AccountReconciliationExcelDto dto = new AccountReconciliationExcelDto()
                {
                    CompanyId = companyId,
                    FilePath = path
                };
                var result = accountReconciliationService.AddByExcel(dto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);  
            }
            return BadRequest("Dosya seçilmedi.");
        }
    }
}
