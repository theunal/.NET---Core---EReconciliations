using Business.Abstract;
using Entities;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.Excel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaBsReconciliationDetailsController : ControllerBase
    {
        private readonly IBaBsReconciliationDetailService baBsReconciliationDetailService;
        public BaBsReconciliationDetailsController(IBaBsReconciliationDetailService baBsReconciliationDetailService)
        {
            this.baBsReconciliationDetailService = baBsReconciliationDetailService;
        }


        [HttpPost("add")]
        public IActionResult Add(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            var result = baBsReconciliationDetailService.Add(baBsReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("update")]
        public IActionResult Update(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            var result = baBsReconciliationDetailService.Update(baBsReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("delete")]
        public IActionResult Delete(BaBsReconciliationDetail baBsReconciliationDetail)
        {
            var result = baBsReconciliationDetailService.Delete(baBsReconciliationDetail);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }



        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            var result = baBsReconciliationDetailService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("getAllByCompanyId")]
        public IActionResult GetAll(int babsReconciliationId)
        {
            var result = baBsReconciliationDetailService.GetAll(babsReconciliationId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }



        [HttpPost("addByExcel")]
        public IActionResult AddByExcel(IFormFile file, int babsReconciliationId)
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

                BaBsReconciliationDetailExcelDto dto = new BaBsReconciliationDetailExcelDto()
                {
                    BabsReconciliationId = babsReconciliationId,
                    FilePath = path
                };
                var result = baBsReconciliationDetailService.AddByExcel(dto);
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
