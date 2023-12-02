using DataAccessLayer.Models;

namespace BusinessAccessLayer.Services.Orders
{
    public interface IOrderService
    {
        Task<List<OrderViewModel>> ShowOrders(string id);
        Task<List<OrderDetailsViewModel>> ShowOrderDetails(Guid id);
        Task<string> CreateOrder(string id, AddressPaymentViewModel model);
        Task<List<OrderCustomer>> GetAllOrders();
        Task<List<OrderViewModel>> FindByOrderId(Guid id);
    }
}
