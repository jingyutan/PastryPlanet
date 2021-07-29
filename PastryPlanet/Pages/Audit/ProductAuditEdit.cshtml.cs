using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PastryPlanet.Data;
using PastryPlanet.Models;

namespace PastryPlanet.Pages.Audit
{
    public class ProductAuditEditModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;

        public ProductAuditEditModel(PastryPlanet.Data.PastryPlanetContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductAudit ProductAudit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductAudit = await _context.ProductAudit.FirstOrDefaultAsync(m => m.ID == id);

            if (ProductAudit == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(ProductAudit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAuditExists(ProductAudit.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ProductAudit");
        }

        private bool ProductAuditExists(int id)
        {
            return _context.ProductAudit.Any(e => e.ID == id);
        }
    }
}
