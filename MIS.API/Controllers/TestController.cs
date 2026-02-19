// Generate a test controllers to test the API
using Microsoft.AspNetCore.Mvc;
namespace MIS.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World!");
        }
    }
}