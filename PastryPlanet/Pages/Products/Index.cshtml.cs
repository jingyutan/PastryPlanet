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

namespace PastryPlanet.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly PastryPlanet.Data.PastryPlanetContext _context;

        public IndexModel(PastryPlanet.Data.PastryPlanetContext context)
        {
            _context = context;
        }

        public IList<Product> Product { get;set; }

        public async Task OnGetAsync()
        {
            Product = await _context.Product.ToListAsync();
        }
    }
}
