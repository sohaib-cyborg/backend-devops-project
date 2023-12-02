using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using DataAccessLayer.Models;
using BusinessAccessLayer.Services.Auth;

namespace WebAPITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IAuthServices _authService;

        public CustomerController(IAuthServices authService)
        {
            _authService = authService;
        }
        [HttpGet("Index"),Authorize(Roles = "Admin")]
        public async Task<IActionResult> index()
        {
            var customers = await _authService.GetCustomers();
            if (customers == null)
            {
                return NotFound();
            }
            return Ok(customers);
        }
        [HttpPut("Update"),Authorize(Roles ="Customer")]
        public async Task<IActionResult> Update(string UserId,UpdateCustomerViewModel model)
        {
            bool success = await _authService.UpdateCustomer(model,UserId);
            return Ok(success);
        }
        [HttpGet("GetCustomer"),Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetCustomer(string UserId)
        {
            var customer = await _authService.GetCustomerById(UserId);
            if (customer==null)
            {
                return NotFound();
            }
            return Ok(customer);
        }
    }
}
