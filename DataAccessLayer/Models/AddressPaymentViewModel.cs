namespace DataAccessLayer.Models
{
    public class AddressPaymentViewModel
    {
        public Guid AddressId { get; set; }
        public Guid PaymentMethodId { get; set; }
        public List<OrderProductViewModel> Products { get; set; }
    }
}
