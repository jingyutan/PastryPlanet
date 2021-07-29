using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public CreateModel(PastryPlanet.Data.PastryPlanetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            //for testing error management
            //throw new Exception("Test Error");
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; }

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

            // Once a record is added, create an audit record
            if (await _context.SaveChangesAsync() > 0)
            {
                // Create an auditrecord object
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Add Product Record";
                auditrecord.DateTimeStamp = DateTime.Now;
                auditrecord.KeyProductID = Product.ID;
                // Get current logged-in user
                var userID = User.Identity.Name.ToString();
                auditrecord.Username = userID;

                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
