using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Context;

namespace DataAccessLayer.Models
{
    public class PaymentMethod
    {
        public Guid PaymentMethodId { get; set; }
        public string CardNumber { get; set; }
        public string Provider { get; set; }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
