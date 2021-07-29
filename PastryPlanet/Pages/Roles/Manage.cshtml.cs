using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PastryPlanet.Data;
using PastryPlanet.Models;

namespace PastryPlanet.Pages.Roles
{
    public class ManageModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;

        public ManageModel(PastryPlanet.Data.PastryPlanetContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ApplicationRole ApplicationRole { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Roles.Add(ApplicationRole);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
