using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PastryPlanet.Models;

namespace PastryPlanet.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly PastryPlanet.Data.PastryPlanetContext _context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            PastryPlanet.Data.PastryPlanetContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        //public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Phone number")]
            [RegularExpression(@"^[0-9]{8}$", ErrorMessage = "please enter 8 digit phone number")]
            public string PhoneNumber { get; set; }

            [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Please enter only letters.")]
            [Display(Name = "Full name")]
            public string FullName { get; set; }

            [RegularExpression("^[a-zA-Z0-9@.]*$", ErrorMessage = "Please enter valid username.")]
            public string UserName { get; set; }
        }

        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var fullName = (await _userManager.GetUserAsync(User)).FullName;
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            //Username = userName;

            Input = new InputModel
            {
                UserName = userName,
                PhoneNumber = phoneNumber,
                FullName = fullName
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var userName = await _userManager.GetUserNameAsync(user);
            if (Input.UserName != userName)
            {
                var setUsernameResult = await _userManager.SetUserNameAsync(user, Input.UserName);
                if (!setUsernameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set Username.";
                    return RedirectToPage();
                }
            }
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }
            var fullName = (await _userManager.GetUserAsync(User)).FullName;
            if (Input.FullName != fullName)
            {
                user.FullName = Input.FullName;
                var setFullNameResult = await _userManager.UpdateAsync(user);
                if (!setFullNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set full name.";
                    return RedirectToPage();
                }

                // change full name attempt - create an audit record
                var email = user.Email;
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Change full name";
                auditrecord.DateTimeStamp = DateTime.Now;

                auditrecord.Username = email;
                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
