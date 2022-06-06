using Business.Abstract;
using Entities;
using Entities.Concrete;
using Entities.Dtos.Excel;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountReconciliationDetailsController : ControllerBase
    {
        private readonly IAccountReconciliationDetailService accountReconciliationDetailService;
        public AccountReconciliationDetailsController(IAccountReconciliationDetailService accountReconciliationDetailService)
        {
            this.accountReconciliationDetailService = accountReconciliationDetailService;
        }
       
        
        
        [HttpPost("add")]
        public IActionResult Add(AccountReconciliationDetail accountReconciliationDetail)
        {
            var result = accountReconciliationDetailService.Add(accountReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(AccountReconciliationDetail accountReconciliationDetail)
        {
            var result = accountReconciliationDetailService.Update(accountReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(AccountReconciliationDetail accountReconciliationDetail)
        {
            var result = accountReconciliationDetailService.Delete(accountReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }



        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = accountReconciliationDetailService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getAllByAccountReconciliationId")]
        public IActionResult GetAll(int accountReconciliationId)
        {
            var result = accountReconciliationDetailService.GetAll(accountReconciliationId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }



        [HttpPost("addByExcel")]
        public IActionResult AddByExcel(IFormFile file, int accountReconciliationId)
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

                AccountReconciliationDetailExcelDto dto = new AccountReconciliationDetailExcelDto()
                {
                    AccountReconciliationId = accountReconciliationId,
                    FilePath = path
                };
                var result = accountReconciliationDetailService.AddByExcel(dto);
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
