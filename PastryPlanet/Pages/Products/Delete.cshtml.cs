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

namespace PastryPlanet.Pages.Products
{
    [Authorize(Roles="Admin")]
    public class DeleteModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;
        private readonly SignInManager<ApplicationUser> _SignInManager;

        public DeleteModel(PastryPlanet.Data.PastryPlanetContext context,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _SignInManager = signInManager;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FirstOrDefaultAsync(m => m.ID == id);

            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Product = await _context.Product.FindAsync(id);

            if (Product != null)
            {
                _context.Product.Remove(Product);
                //await _context.SaveChangesAsync();

                if (_SignInManager.IsSignedIn(User))
                {
                    var prod = new ProductAudit
                    {
                        Name = Product.Name,
                        Description = Product.Description,
                        Price = Product.Price,
                        QuantityInStock = Product.QuantityInStock,
                        Image = Product.Image,
                        Productid = Product.ID,
                        DateTimeStamp = DateTime.Now,
                        Username = User.Identity.Name.ToString()
                    };
                    _context.ProductAudit.Add(prod);
                    // Once a record is deleted, create an audit record
                    if (await _context.SaveChangesAsync() > 0)
                    {
                        var auditrecord = new AuditRecord();
                        auditrecord.AuditActionType = "Delete Product Record";
                        auditrecord.DateTimeStamp = DateTime.Now;
                        auditrecord.KeyProductID = prod.ID;
                        var userID = User.Identity.Name.ToString();
                        auditrecord.Username = userID;
                        _context.AuditRecords.Add(auditrecord);
                        await _context.SaveChangesAsync();
                    }
                }
                
            }
            return RedirectToPage("./Index");
        }
    }
}
