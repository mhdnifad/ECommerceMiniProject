using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ECommerceMini.Models;
using ECommerceMini.Services;
using ECommerceMini.ViewModels;
using ECommerceMini.Data;

namespace ECommerceMini.Controllers
{
    [Authorize(Roles = "Customer")]
    public class PaymentController : Controller
    {
        private readonly CartService _cartService;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PaymentController(CartService cartService, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _cartService = cartService;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = _cartService.GetCart();

            var model = new PaymentViewModel
            {
                Amount = cart.Sum(i => i.Product?.Price * i.Quantity ?? 0)
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PaymentViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (model.PaymentMethod == "Card")
            {
                if (string.IsNullOrWhiteSpace(model.CardholderName) ||
                    string.IsNullOrWhiteSpace(model.CardNumber) ||
                    string.IsNullOrWhiteSpace(model.ExpiryMonth) ||
                    string.IsNullOrWhiteSpace(model.ExpiryYear) ||
                    string.IsNullOrWhiteSpace(model.CVV))
                {
                    ModelState.AddModelError("", "Please complete all card payment fields.");
                    return View(model);
                }
            }
            else if (model.PaymentMethod == "GPay")
            {
                if (string.IsNullOrWhiteSpace(model.UpiId))
                {
                    ModelState.AddModelError("UpiId", "Please enter a valid UPI ID.");
                    return View(model);
                }
            }

            var cartItems = _cartService.GetCart();
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return RedirectToAction("Login", "Account");

            var order = new Order
            {
                CustomerEmail = user.Email!,
                OrderDate = DateTime.Now,
                TotalAmount = model.Amount,
                Status = "Pending",
                Items = cartItems.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Product?.Price ?? 0
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.SaveChanges();
            _cartService.ClearCart();

            return RedirectToAction("Success", "Checkout");
        }
    }
}
