using Business.Abstract;
using Entities;
using Entities.Concrete;
using Entities.Dtos.Excel;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrentAccountsController : ControllerBase
    {
        private readonly ICurrentAccountService currentAccountService;
        public CurrentAccountsController(ICurrentAccountService currentAccountService)
        {
            this.currentAccountService = currentAccountService;
        }


        [HttpGet("getAllByCompanyId")]
        public IActionResult GetAll(int companyId)
        {
            var result = currentAccountService.GetAll(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }


        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = currentAccountService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }



        [HttpPost("add")]
        public IActionResult Add(CurrentAccount currencyAccount)
        {
            var result = currentAccountService.Add(currencyAccount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(CurrentAccount currencyAccount)
        {
            var result = currentAccountService.Update(currencyAccount);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(int id)
        {
            var result = currentAccountService.Delete(id);
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

                CurrencyAccountExcelDto dto = new CurrencyAccountExcelDto()
                {
                    FilePath = path,
                    CompanyId = companyId
                };
                var result = currentAccountService.AddByExcel(dto);
                if (result.Success)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Dosya seçilmedi.");
        }


        [HttpGet("getByCode")]
        public IActionResult GetByCode(string code)
        {
            var result = currentAccountService.GetByCode(code);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }




    }
}