namespace DataAccessLayer.Models
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public int Amount { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}
