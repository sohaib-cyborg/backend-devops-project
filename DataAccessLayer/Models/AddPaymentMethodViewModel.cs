using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class AddPaymentMethodViewModel
    {
        [Required]
        public string CardNumber { get; set; } = string.Empty;
        [Required] public string Provider { get; set; } = string.Empty;
    }
}
