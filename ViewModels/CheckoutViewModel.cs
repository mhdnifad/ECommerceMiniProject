using System.ComponentModel.DataAnnotations;
using ECommerceMini.ViewModels;

namespace ECommerceMini.ViewModels
{
    public class CheckoutViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; } = string.Empty;

        public List<CartItemModel> CartItems { get; set; } = new();
        public decimal TotalAmount { get; set; }
    }
}
