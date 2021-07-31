using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PastryPlanet.Data;
using PastryPlanet.Models;

namespace PastryPlanet.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public IndexModel(PastryPlanet.Data.PastryPlanetContext context,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<CartItem> CartItems { get; set; }
        public cart cart { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            CartItems = await GetCartItemsByUserIdAsync(user.Id);

            return Page();

        }
        public async Task<IActionResult> OnPostUpdateAsync(int id)
        {
            int updatedQuantity = Convert.ToInt32(Request.Form["Quantity"]);
            ApplicationUser user = await _userManager.GetUserAsync(User);
            CartItem cartItem = await GetCartItemByProductIdForUserAsync(user.Id, id);


            cartItem.Quantity = updatedQuantity;
            await UpdateCartItemsAsync(cartItem);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            await RemoveCartItemsAsync(user.Id, id);

            return RedirectToPage();
        }



        public async Task RemoveCartItemsAsync(string userId, int productId)
        {
            CartItem cartItem = await GetCartItemByProductIdForUserAsync(userId, productId);
            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task<CartItem> GetCartItemByProductIdForUserAsync(string userId, int productId)
        {
            var cartItems = await GetCartItemsByUserIdAsync(userId);
            return cartItems.FirstOrDefault(cart => cart.ProductID == productId);
        }

        public async Task<IList<CartItem>> GetCartItemsByUserIdAsync(string userId)
        {
            cart = await _context.Cart.FirstOrDefaultAsync(cart => cart.UserID == userId);
            if (cart == null)
            {
                throw new Exception("Test Error: user does not have cart\nplease delete this existing" +
                    "user and register it again");
            }
            return CartItems = await _context.CartItem
                .Where(c => c.CartID == cart.ID)
                .Include(c => c.Product).ToListAsync();
        }
        public async Task UpdateCartItemsAsync(CartItem cartItem)
        {
            _context.CartItem.Update(cartItem);
            await _context.SaveChangesAsync();
        }
    }
}
