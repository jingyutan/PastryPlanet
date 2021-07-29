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
    public class IndexModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;

        public IndexModel(PastryPlanet.Data.PastryPlanetContext context)
        {
            _context = context;
        }

        public IList<AuditRecord> AuditRecord { get;set; }

        public async Task OnGetAsync()
        {
            AuditRecord = await _context.AuditRecords.ToListAsync();
        }
    }
}
