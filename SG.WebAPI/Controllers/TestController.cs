using Microsoft.AspNetCore.Mvc;

namespace SG.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Index()
        {
            return "Welcome to SyncGuardian WebApi";
        }
    }
}
