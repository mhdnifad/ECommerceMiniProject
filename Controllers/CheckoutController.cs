using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ECommerceMini.Data;
using ECommerceMini.Services;
using ECommerceMini.ViewModels;

namespace ECommerceMini.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CheckoutController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;
        private readonly PaymentService _paymentService;
        private readonly UserManager<IdentityUser> _userManager;

        public CheckoutController(
            CartService cartService,
            ApplicationDbContext context,
            PaymentService paymentService,
            UserManager<IdentityUser> userManager)
        {
            _cartService = cartService;
            _context = context;
            _paymentService = paymentService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCart();

            var model = new CheckoutViewModel
            {
                CartItems = cartItems.Select(c => new CartItemModel
                {
                    ProductId = c.ProductId,
                    Name = c.Product?.Name ?? "",
                    Price = c.Product?.Price ?? 0,
                    ImageUrl = c.Product?.ImageUrl ?? "",
                    Quantity = c.Quantity
                }).ToList(),
                TotalAmount = cartItems.Sum(c => c.Product?.Price * c.Quantity ?? 0)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            return RedirectToAction("Index", "Payment");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
