namespace DataAccessLayer.Models
{
    public class Tracking
    {
        public Guid TrackingId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime ShippingDate { get; set; }
        public string Status { get; set; }
        public string Total { get; set; }
        public Order Order { get; set; }

    }
}
