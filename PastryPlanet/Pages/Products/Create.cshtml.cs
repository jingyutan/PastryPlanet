using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PastryPlanet.Data;
using PastryPlanet.Models;

namespace PastryPlanet.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;
        private readonly SignInManager<ApplicationUser> _SignInManager;

        public CreateModel(PastryPlanet.Data.PastryPlanetContext context,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _SignInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            //for testing error management
            //throw new Exception("Test Error");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }
        public ProductAudit prodaudit { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Product.Add(Product);
            //await _context.SaveChangesAsync();

            if (_SignInManager.IsSignedIn(User))
            {
                if (await _context.SaveChangesAsync() > 0)
                {
                    // Once a record is added, create an audit record
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
                    await _context.SaveChangesAsync();

                    // Create an auditrecord object
                    var auditrecord = new AuditRecord();
                    auditrecord.AuditActionType = "Add Product Record";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.KeyProductID = prod.ID;
                    // Get current logged-in user
                    var userID = User.Identity.Name.ToString();
                    auditrecord.Username = userID;

                    _context.AuditRecords.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }
            }
            

            return RedirectToPage("./Index");
        }
    }
}
