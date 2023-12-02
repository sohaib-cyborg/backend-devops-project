using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Context;
using DataAccessLayer.Models;

namespace DataAccessLayer.DAL.OrderRepo
{
    public class OrderRepository : IOrderRepo
    {
        private readonly AuthAPIDbContext _db;


        public OrderRepository()
        {
            _db = SingletonDbContext.GetDbContext();
        }
        public async Task<string> CreateOrder(string id, AddressPaymentViewModel model)
        {
            try
            {
                double sum = 0;
                foreach (var product in model.Products)
                {
                    sum += product.Quantity * product.Price;
                }

                var payment = new Payment()
                {
                    PaymentId = Guid.NewGuid(),
                    Amount = Convert.ToInt32(sum),
                    PaymentStatus = "Post Paid",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                };
                await _db.Payment.AddAsync(payment);

                var Order = new Order()
                {
                    OrderId = Guid.NewGuid(),
                    UserId = id,
                    PaymentId = payment.PaymentId,
                    AddressId = model.AddressId,
                    PaymentMethodId = model.PaymentMethodId,
                    CompleteOrderStatus = "Fully Shipped",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                await _db.Order.AddAsync(Order);

                var Tracking = new Tracking()
                {
                    TrackingId = Guid.NewGuid(),
                    OrderId = Order.OrderId,
                    ShippingDate = DateTime.UtcNow,
                    Status = "Shipped",
                    Total = Convert.ToString(sum)
                };
                await _db.Tracking.AddAsync(Tracking);

                foreach (var item in model.Products)
                {
                    var OrderItem = new OrderItems()
                    {
                        Id = Guid.NewGuid(),
                        TrackingId = Tracking.TrackingId,
                        ProductId = item.ProductId,
                        ItemQuantity = item.Quantity
                    };
                    await _db.OrderItems.AddAsync(OrderItem);
                }

                await _db.SaveChangesAsync();
                return Convert.ToString(Order.OrderId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<OrderViewModel>> FindByOrderId(Guid id)
        {
            var orderWithTracking = await _db.Order
                                .Include(o => o.TrackingList)
                                .Where(o => o.OrderId == id)
                                .Select(o => new OrderViewModel
                                {
                                    OrderId = o.OrderId,
                                    CompleteOrderStatus = o.CompleteOrderStatus,
                                    TrackingList = o.TrackingList.Select(t => new TrackingViewModel
                                    {
                                        TrackingId = t.TrackingId,
                                        ShippingDate = t.ShippingDate,
                                        Status = t.Status,
                                        Total = t.Total
                                    }).ToList()
                                })
                                .ToListAsync();
            return orderWithTracking;
        }

        public async Task<List<OrderCustomer>> GetAllOrders()
        {
            var orders = await _db.Order.Select(o => new OrderCustomer
            {
                OrderId = o.OrderId,
                UserId = o.UserId,
                Name = o.User.Name
            }).AsNoTracking().ToListAsync();
            return orders;
        }

        public async Task<List<OrderDetailsViewModel>> ShowOrderDetails(Guid id)
        {
            var orderDetails = from OrderItems in _db.OrderItems
                               join Product in _db.Product
                               on OrderItems.ProductId equals Product.ProductId
                               where OrderItems.TrackingId == id
                               select new OrderDetailsViewModel()
                               {
                                   ProductId = Product.ProductId,
                                   ItemQuantity = OrderItems.ItemQuantity,
                                   Name = Product.Name,
                                   Total = Product.Price * OrderItems.ItemQuantity
                               };
            var results = await orderDetails.ToListAsync();
            return results;
        }

        public async Task<List<OrderViewModel>> ShowOrders(string id)
        {
            var orderWithTracking = await _db.Order
                                .Include(o => o.TrackingList)
                                .Where(o => o.UserId == id)
                                .Select(o => new OrderViewModel
                                {
                                    OrderId = o.OrderId,
                                    CompleteOrderStatus = o.CompleteOrderStatus,
                                    TrackingList = o.TrackingList.Select(t => new TrackingViewModel
                                    {
                                        TrackingId = t.TrackingId,
                                        ShippingDate = t.ShippingDate,
                                        Status = t.Status,
                                        Total = t.Total
                                    }).ToList()
                                })
                                .ToListAsync();
            return orderWithTracking;
        }
    }
}
