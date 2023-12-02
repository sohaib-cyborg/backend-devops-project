namespace DataAccessLayer.Models
{
    public class OrderItems
    {
        public Guid Id { get; set; }
        public Guid TrackingId { get; set; }
        public Guid ProductId { get; set; }
        public int ItemQuantity { get; set; }
        public Tracking Tracking { get; set; }
        public Product Product { get; set; }
    }
}
