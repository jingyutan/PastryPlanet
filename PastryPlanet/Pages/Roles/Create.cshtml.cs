using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PastryPlanet.Models;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace PastryPlanet.Pages.Roles
{
	[Authorize(Roles="Arthur")]
	public class CreateModel : PageModel
	{
		private readonly RoleManager<ApplicationRole> _roleManager;
		public CreateModel(RoleManager<ApplicationRole> roleManager)
		{
			_roleManager = roleManager;
		}
		public IActionResult OnGet()
		{
			return Page();
		}
		[BindProperty]
		public ApplicationRole ApplicationRole { get; set; }
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			ApplicationRole.CreatedDate = DateTime.UtcNow;
			ApplicationRole.IPAddress =
		   Request.HttpContext.Connection.RemoteIpAddress.ToString();
			IdentityResult roleRuslt = await _roleManager.CreateAsync(ApplicationRole);
			return RedirectToPage("Index");
		}
	}
}
