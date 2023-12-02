using DataAccessLayer.Models;

namespace DataAccessLayer.DAL.OrderRepo
{
    public interface IOrderRepo
    {
        Task<List<OrderViewModel>> ShowOrders(string id);
        Task<List<OrderDetailsViewModel>> ShowOrderDetails(Guid id);
        Task<string> CreateOrder(string id, AddressPaymentViewModel model);
        Task<List<OrderCustomer>> GetAllOrders();
        Task<List<OrderViewModel>> FindByOrderId(Guid id);
    }
}
