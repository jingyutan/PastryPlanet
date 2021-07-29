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
    public class DetailsModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;

        public DetailsModel(PastryPlanet.Data.PastryPlanetContext context)
        {
            _context = context;
        }

        public AuditRecord AuditRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AuditRecord = await _context.AuditRecords.FirstOrDefaultAsync(m => m.Audit_ID == id);

            if (AuditRecord == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
