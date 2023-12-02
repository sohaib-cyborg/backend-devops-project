using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using BusinessAccessLayer.Services.PaymentMethods;

namespace WebAPITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;

        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> createPayment(string userId,AddPaymentMethodViewModel model)
        {
            bool success = await _paymentMethodService.AddPayment(model, userId);
            if (success)
            {
                return Ok(success); 
            }
            return BadRequest();
        }
        [HttpGet("GetUserPaymentMethod")]
        public async Task<IActionResult> GetUserPaymentMethods(string userId)
        {
            List<showPaymentMethodViewModel> model = await _paymentMethodService.FindByUserId(userId);
            if(model == null)
            {
                return BadRequest();
            }
            return Ok(model);
        }
        [HttpGet("GetPaymentMethod")]
        public async Task<IActionResult> GetPaymentMethods(Guid PaymentMethodId)
        {
            var paymentMethod = await _paymentMethodService.FindById(PaymentMethodId);
            if (paymentMethod == null)
            {
                return BadRequest();
            }
            return Ok(paymentMethod);
        }
        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePaymentMethod(Guid PaymentMethodId,UpdateCustomerPaymentViewModel model)
        {
            bool success = await _paymentMethodService.Update(model, PaymentMethodId);
            if(success)
            {
                return Ok(success);
            }
            return BadRequest();
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePaymentMethod(Guid id)
        {
            bool success = await _paymentMethodService.Delete(id);
            if(success) {
            return Ok(success);}
            return BadRequest();
        }
    }
}
