using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PastryPlanet.Data;
using PastryPlanet.Models;
using Microsoft.AspNetCore.Authorization;

namespace PastryPlanet.Pages.Audit
{
    [Authorize(Roles = "Admin")] //Comment this out if you want to troubleshoot
    public class ProductAuditDetailsModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;

        public ProductAuditDetailsModel(PastryPlanet.Data.PastryPlanetContext context)
        {
            _context = context;
        }

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
    }
}
