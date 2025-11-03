using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using BusinessLayer.Managers;
using DALClassLibrary;
using WebApp.Models;
namespace WebApp.Pages
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager userManager;
        public ForgotPasswordModel(UserManager userManager) { this.userManager = userManager; }
        [BindProperty]
        public UserForgotPass user { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            if (user.password1 == user.password2)
            {
                if (userManager.GetUsersEmails().Contains(user.email))
                {
                    userManager.UpdateUserPassword(user.email, user.password1);
                    return RedirectToPage("/Login");

                }
                ModelState.AddModelError(string.Empty, "This email is not valid");
                return Page();
            }

            ModelState.AddModelError(string.Empty, "Passwords are different");
            return Page();
        }
    }
}
