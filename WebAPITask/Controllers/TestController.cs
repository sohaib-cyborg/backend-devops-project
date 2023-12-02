using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Text;

namespace WebAPITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet("Get"),Authorize(Roles = "Admin")]
        public string Get()
        {
            return "Admin hit me";
        }
        [HttpGet("Set"),Authorize(Roles = "Customer")]
        public string Set()
        {
            return "Customer hit me";
        }
    }
}
