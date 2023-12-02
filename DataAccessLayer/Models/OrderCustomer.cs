namespace DataAccessLayer.Models
{
    public class OrderCustomer
    {
        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
    }
}
