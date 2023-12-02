using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using BusinessAccessLayer.Services.Addresses;

namespace WebAPITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressServices _addressServices;

        public AddressController(IAddressServices addressServices)
        {
            _addressServices = addressServices;
        }
        [HttpGet("Index"),Authorize]
        public async Task<IActionResult> Index(string userId)
        {
            var address = await _addressServices.GetAddressById(userId);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }
        [HttpGet("GetAddress"),Authorize]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var address = await _addressServices.GetAddress(id);
            if (address == null)
            {
                return NotFound();
            }
            return Ok(address);
        }
        [HttpPost("Add"),Authorize(Roles = "Customer")]
        public async Task<IActionResult> Add(string userId,AddAddressViewModel request)
        {
            bool success = await _addressServices.CreateAddress(userId,request);
            if(success)
            {
                return Ok(success);
            }
            return BadRequest();
           
        }
        [HttpPut("Update"),Authorize(Roles = "Customer")]
        public async Task<IActionResult> Update(Guid AddressId,UpdateAddressViewModel request)
        {
            bool success = await _addressServices.Update(AddressId, request);
            if(success) {
                return Ok(success);
            }
            return BadRequest();
        }
        [HttpDelete("Delete"),Authorize(Roles = "Customer")]
        public async Task<IActionResult> Delete(Guid AddressId)
        {
            bool success = await _addressServices.Delete(AddressId);
            if(success)
            {
                return Ok(success);
            }
            return BadRequest();
        }
    }
}
