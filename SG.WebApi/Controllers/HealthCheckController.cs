using Microsoft.AspNetCore.Mvc;

namespace SG.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public string HealthCheck()
        {
            return "System is running well.";
        }
    }
}
