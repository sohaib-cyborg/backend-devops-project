using DataAccessLayer.Context;

namespace DataAccessLayer.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public Guid PaymentId { get; set; }
        public Guid AddressId { get; set; }
        public Guid PaymentMethodId { get; set; }  
        public string CompleteOrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ApplicationUser User { get; set; }
        public Payment Payment { get; set; }
        public ICollection<Tracking> TrackingList { get; set; }
    }
}
