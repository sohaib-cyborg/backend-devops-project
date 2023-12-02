using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.DAL.OrderRepo;
using DataAccessLayer.Models;

namespace BusinessAccessLayer.Services.Orders
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _order;

        public OrderService(IOrderRepo order)
        {
            this._order = order;
        }

        public async Task<string> CreateOrder(string id, AddressPaymentViewModel model)
        {
            var orderId = await _order.CreateOrder(id, model);
            
            return orderId;
        }

        public async Task<List<OrderViewModel>> FindByOrderId(Guid id)
        {
            var order = await _order.FindByOrderId(id);
            return order;
        }

        public async Task<List<OrderCustomer>> GetAllOrders()
        {
            var orders = await _order.GetAllOrders();
            return orders;
        }

        public async Task<List<OrderDetailsViewModel>> ShowOrderDetails(Guid id)
        {
           var orderDetails =await _order.ShowOrderDetails(id);
            return orderDetails;
        }

        public async Task<List<OrderViewModel>> ShowOrders(string id)
        {
            var orders = await _order.ShowOrders(id);
            return orders;
        }
    }
}
