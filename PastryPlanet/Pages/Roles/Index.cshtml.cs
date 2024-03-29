﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PastryPlanet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
namespace PastryPlanet.Pages.Roles
{
	[Authorize(Roles = "Arthur")]
	public class IndexModel : PageModel
	{
		private readonly RoleManager<ApplicationRole> _roleManager;
		public IndexModel(RoleManager<ApplicationRole> roleManager)
		{
			_roleManager = roleManager;
		}
		public List<ApplicationRole> ApplicationRole { get; set; }
		public async Task OnGetAsync()
		{
			// Get a list of roles
			ApplicationRole = await _roleManager.Roles.ToListAsync();
		}
	}
}