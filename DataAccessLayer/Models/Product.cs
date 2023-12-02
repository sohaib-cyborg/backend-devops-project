namespace DataAccessLayer.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string? Image { get; set; }
        public byte isAvailable { get; set; } = 1;
    }
}
