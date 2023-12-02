using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using BusinessAccessLayer.Services.Orders;

namespace WebAPITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("Create"), Authorize(Roles = "Customer")]
        public async Task<IActionResult> placeOrder(string userId,AddressPaymentViewModel model)
        {
            string success = await _orderService.CreateOrder(userId, model);
            if (success!=null)
            {
                return Ok(success);
            }
            return BadRequest();
        }
        [HttpGet("ShowAllOrders"),Authorize(Roles = "Admin")]
        public async Task<IActionResult> ShowOrders()
        {
            var orders = await _orderService.GetAllOrders();
            if (orders == null) {
            return BadRequest();}
            return Ok(orders);
        }
        [HttpGet("ShowUserOrders"),Authorize]
        public async Task<IActionResult> userOrders(string userId)
        {
            var orders = await _orderService.ShowOrders(userId);
            if(orders == null)
            {
                return BadRequest();
            }
            return Ok(orders);
        }
        [HttpGet("OrderDetails"),Authorize]
        public async Task<IActionResult> orderDetails(Guid trackingId)
        {
            var details = await _orderService.ShowOrderDetails(trackingId);
            if (details == null)
            {
                return BadRequest();
            }
            return Ok(details);
        }
        [HttpGet("GetOrder"),Authorize]
        public async Task<IActionResult> orderTracking(Guid OrderId)
        {
            var tracking = await _orderService.FindByOrderId(OrderId);
            if (tracking == null) {
                return BadRequest();
            }
            return Ok(tracking);
        }
    }
}
