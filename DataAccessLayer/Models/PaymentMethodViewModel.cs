namespace DataAccessLayer.Models
{
    public class PaymentMethodViewModel
    {
        public Guid PaymentMethodId { get; set; }
        public string Provider { get; set; }
        public string CardNumber { get; set; }
    }
}
