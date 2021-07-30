using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PastryPlanet.Data;
using Microsoft.AspNetCore.Authorization;

namespace PastryPlanet.Pages.Feedback
{
    public class IndexModel : PageModel
    {
        private readonly PastryPlanetContext _context;

        public IndexModel(PastryPlanetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.ApplicationUser ApplicationUser { get; set; }
    }
}
