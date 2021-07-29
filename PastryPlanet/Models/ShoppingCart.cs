using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PastryPlanet.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;




namespace PastryPlanet.Models
{
    public class ShoppingCart
    {
// --------------------- Section for properties -----------------------------------------
        private readonly PastryPlanetContext _pastryPlanetContext;
        private ShoppingCart (PastryPlanetContext pastryPlanetContext)
        {
            _pastryPlanetContext = pastryPlanetContext;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }
// ------------------------- Section for methods -----------------------------------------------------

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            var context = services.GetService<PastryPlanetContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        // ------ Method to add to cart --------------------------------------
        public void AddToCart(Product product, int amount)
        {
            var shoppingCartItem =
                _pastryPlanetContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Product.ID == product.ID && s.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                _pastryPlanetContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _pastryPlanetContext.SaveChanges();
        }

        //---------------- Method to Remove from cart ---------------------------------------
        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem =
                _pastryPlanetContext.ShoppingCartItems.SingleOrDefault(
                    s => s.Product.ID == product.ID && s.ShoppingCartId == ShoppingCartId);
            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _pastryPlanetContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _pastryPlanetContext.SaveChanges();
            return localAmount;
        }

        // ------------ Method to return all shopping cart items ----------------------------
        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??
                (ShoppingCartItems =
                _pastryPlanetContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Product)
                .ToList());
        }

        // ------------- Method to clear cart items --------------------
        public void CleartCart()
        {
            var cartItems = _pastryPlanetContext
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _pastryPlanetContext.ShoppingCartItems.RemoveRange(cartItems);
            _pastryPlanetContext.SaveChanges();
        }

        // -------------- Method to get shopping cart total price -----------------------
        public double GetShoppingCartTotal()
        {
            var total = _pastryPlanetContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return total;
        }
    }
}
