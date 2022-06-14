using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurenciesController : ControllerBase        
    {
        private readonly ICurrencyService currencyService;
        public CurenciesController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }



        [HttpGet("getAllCurrencies")]
        public IActionResult getAllCurrencies()
        {
            var result = currencyService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
        
    }
}
