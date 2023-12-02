using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using BusinessAccessLayer.Services.Auth;

namespace WebAPITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public AuthController(IAuthServices authService)
        {
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterationModel model)
        {
            if(await _authService.CreateUser(model))
            {
                return Ok("User Registered Successfully!");
            }
            return BadRequest("Something Went Wrong!");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var tokenString = await _authService.GenerateTokenString(model);
            if( tokenString == null ) { return BadRequest(); }
            return Ok(tokenString);
        }
    }
}
