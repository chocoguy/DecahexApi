using Api.Data.WardMax;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.WardMax
{
    [ApiController]
    [Route("[controller]")]
    public class WardMaxController : ControllerBase
    {
        public WardMaxController() 
        { 
            
        }

        [HttpGet("Testo")]
        public object Testo()
        {
            DataPortal dp = new();

            dp.sqlTest();


            return Ok();
        }

    }
}
