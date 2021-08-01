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
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

namespace PastryPlanet.Pages.Products
{
    
    public class DetailsModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public DetailsModel(PastryPlanet.Data.PastryPlanetContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public Product Product { get; set; }
        public ProductInput Input { get; set; }
        public CartItem CartItem { get; set; }
        public cart cart { get; set; }
        public ApplicationUser user { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.ID == id);
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (_signInManager.IsSignedIn(User))
            {
                cart = await _context.Cart.FirstOrDefaultAsync(c => c.UserID == user.Id);
            }

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, ProductInput Input)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            user = await _userManager.GetUserAsync(User);            
            //get cart by user id 
            cart = await _context.Cart.FirstOrDefaultAsync(c => c.UserID == user.Id);
            Product = await _context.Product.FirstOrDefaultAsync(m => m.ID == id);

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Input.Quantity == 0)
            {
                return Page();
            }
                
            CartItem CartItem = new CartItem
            {
                CartID = cart.ID,
                ProductID = Product.ID,
                Quantity = Input.Quantity
            };       
            if (await GetCartItemByProductIdForUserAsync(user.Id, id) != null)
            {
                CartItem existingCartItem = await GetCartItemByProductIdForUserAsync(user.Id, id);
                existingCartItem.Quantity += Input.Quantity;
                await UpdateCartItemsAsync(existingCartItem);
            }
            else
            {
                await CreateCartItemAsync(CartItem);
            }
            
            return Redirect("/Cart");
        }



        public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(string userId)
        {
            cart = await _context.Cart.FirstOrDefaultAsync(cart => cart.UserID == userId);
            return _context.CartItem.Where(cartItem => cartItem.CartID == cart.ID).Include(x => x.Product);
        }
        public async Task<CartItem> GetCartItemByProductIdForUserAsync(string userId, int productId)
        {
            var cartItems = await GetCartItemsByUserIdAsync(userId);
            return cartItems.FirstOrDefault(cart => cart.ProductID == productId);
        }
        public async Task UpdateCartItemsAsync(CartItem cartItem)
        {
            _context.CartItem.Update(cartItem);
            await _context.SaveChangesAsync();
        }
        public async Task CreateCartItemAsync(CartItem cartItems)
        {
            await _context.CartItem.AddAsync(cartItems);
            await _context.SaveChangesAsync();
        }

    }
    public class ProductInput
    {
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter numbers only.")]
        public int Quantity { get; set; } = 1;
    }
}
