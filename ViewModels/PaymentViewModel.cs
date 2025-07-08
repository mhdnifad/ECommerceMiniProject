using System.ComponentModel.DataAnnotations;

namespace ECommerceMini.ViewModels
{
    public class PaymentViewModel
    {
        [Required(ErrorMessage = "Please select a payment method.")]
        public string PaymentMethod { get; set; } = string.Empty;

        
        public string? CardholderName { get; set; }

        [StringLength(16, MinimumLength = 13, ErrorMessage = "Card number must be between 13 and 16 digits.")]
        public string? CardNumber { get; set; }

        [StringLength(2, MinimumLength = 1, ErrorMessage = "Invalid month.")]
        public string? ExpiryMonth { get; set; }

        [StringLength(2, MinimumLength = 1, ErrorMessage = "Invalid year.")]
        public string? ExpiryYear { get; set; }

        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV must be 3 or 4 digits.")]
        public string? CVV { get; set; }

       
        [Display(Name = "GPay UPI ID")]
        public string? UpiId { get; set; }

        public string? Address { get; set; }

        [Required]
        [Range(0.01, 1000000, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }
    }
}
