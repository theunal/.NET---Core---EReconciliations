using Business.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaBsReconciliationsController : ControllerBase
    {
        private readonly IBaBsReconciliationService baBsReconciliationService;
        public BaBsReconciliationsController(IBaBsReconciliationService baBsReconciliationService)
        {
            this.baBsReconciliationService = baBsReconciliationService;
        }

        [HttpGet("getAllByCompanyId")]
        public IActionResult GetAll(int companyId)
        {
            var result = baBsReconciliationService.GetAll(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        
        [HttpGet("getAllDto")]
        public IActionResult GetAllDto(int companyId)
        {
            var result = baBsReconciliationService.GetAllDto(companyId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        

        [HttpPost("sendReconciliationMail")]
        public IActionResult SendReconciliationMail(BaBsReconciliationDto dto)
        {
            var result = baBsReconciliationService.SendReconciliationMail(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        
        [HttpGet("getByCode")] // guidi gönderiyoruz ama onaylandıktan sonra db de true ya çevirmedim düzelticem
        public IActionResult getByCode(string code)
        {
            var result = baBsReconciliationService.GetByCode(code);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = baBsReconciliationService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }




        


        [HttpPost("add")]
        public IActionResult Add(BaBsReconciliation baBsReconciliation)
        {
            var result = baBsReconciliationService.Add(baBsReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(BaBsReconciliation baBsReconciliation)
        {
            var result = baBsReconciliationService.Update(baBsReconciliation);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(BaBsReconciliation baBsReconciliation)
        {
            var result = baBsReconciliationService.Delete(baBsReconciliation);
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

                BaBsReconciliationExcelDto dto = new BaBsReconciliationExcelDto()
                {
                    CompanyId = companyId,
                    FilePath = path
                };
                var result = baBsReconciliationService.AddByExcel(dto);
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
