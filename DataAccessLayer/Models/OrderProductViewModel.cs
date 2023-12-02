namespace DataAccessLayer.Models
{
    public class OrderProductViewModel
    {
        public Guid ProductId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
