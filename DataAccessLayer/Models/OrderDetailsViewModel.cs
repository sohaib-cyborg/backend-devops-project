namespace DataAccessLayer.Models
{
    public class OrderDetailsViewModel
    {
        public Guid ProductId { get; set; } 
        public int ItemQuantity { get; set; }
        public string Name { get; set; }
        public double Total { get; set; }
    }
}
