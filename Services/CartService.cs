using ECommerceMini.Models;
using Newtonsoft.Json;

namespace ECommerceMini.Services
{
    public class CartService
    {
        private const string CartKey = "Cart";

        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<CartItem> GetCart()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var cartJson = session?.GetString(CartKey);

            return string.IsNullOrEmpty(cartJson)
                ? new List<CartItem>()
                : JsonConvert.DeserializeObject<List<CartItem>>(cartJson) ?? new List<CartItem>();
        }

        public void SaveCart(List<CartItem> cart)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var cartJson = JsonConvert.SerializeObject(cart);
            session?.SetString(CartKey, cartJson);
        }

        public void ClearCart()
        {
            _httpContextAccessor.HttpContext?.Session.Remove(CartKey);
        }

        public void AddToCart(Product product)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == product.Id);

            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = 1
                });
            }

            SaveCart(cart);
        }

        public void RemoveFromCart(int productId)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }
        }

        public void UpdateQuantity(int productId, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);

            if (item != null)
            {
                item.Quantity = quantity;
                SaveCart(cart);
            }
        }
    }
}
