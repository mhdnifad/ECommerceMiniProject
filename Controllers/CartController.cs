using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ECommerceMini.Services;
using ECommerceMini.Data;
using ECommerceMini.ViewModels;

namespace ECommerceMini.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartService _cartService;

        public CartController(ApplicationDbContext context, CartService cartService)
        {
            _context = context;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartService.GetCart();
            var model = cartItems.Select(item => new CartItemModel
            {
                ProductId = item.ProductId,
                Name = item.Product?.Name ?? "",
                Price = item.Product?.Price ?? 0,
                ImageUrl = item.Product?.ImageUrl ?? "",
                Quantity = item.Quantity
            }).ToList();

            return View(model);
        }

        public IActionResult Add(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
                _cartService.AddToCart(product);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int id)
        {
            _cartService.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int productId, int quantity)
        {
            _cartService.UpdateQuantity(productId, quantity);
            return RedirectToAction("Index");
        }
    }
}
