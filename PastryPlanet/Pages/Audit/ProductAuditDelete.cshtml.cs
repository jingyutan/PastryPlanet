using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PastryPlanet.Data;
using PastryPlanet.Models;

namespace PastryPlanet.Pages.Audit
{
    public class ProductAuditDeleteModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;

        public ProductAuditDeleteModel(PastryPlanet.Data.PastryPlanetContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ProductAudit = await _context.ProductAudit.FindAsync(id);

            if (ProductAudit != null)
            {
                _context.ProductAudit.Remove(ProductAudit);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ProductAudit");
        }
    }
}
