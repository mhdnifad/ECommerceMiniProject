using Stripe;
using Stripe.Checkout;

namespace ECommerceMini.Services
{
    public class PaymentService
    {
        public string CreateCheckoutSession(string customerEmail, List<(string name, decimal price, int quantity)> items)
        {

            var lineItems = items.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmountDecimal = item.price * 100,
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.name
                    }
                },
                Quantity = item.quantity
            }).ToList();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = lineItems,
                Mode = "payment",
                CustomerEmail = customerEmail,
                SuccessUrl = "https://localhost:5001/Checkout/Success",
                CancelUrl = "https://localhost:5001/Cart"
            };

            var service = new SessionService();
            var session = service.Create(options);

            return session.Url;
        }
    }
}
