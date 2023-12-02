namespace DataAccessLayer.Models
{
    public class showPaymentMethodViewModel
    {
        public string UserId { get; set; } =string.Empty;
        public Guid PaymentMethodId { get;set; }
        public string Provider { get; set; } = string.Empty;
    }
}
