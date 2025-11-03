using GamesWebsite.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Xml;
using Microsoft.AspNetCore.Authentication;
using System.Text;
using System.Security.Cryptography;
using BusinessLayer.Managers;
using DALClassLibrary;

namespace GamesWebsite.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserManager userManager;
        public LoginModel(UserManager userManager)
        {
            this.userManager = userManager;
        }
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = userManager.FindUserByUsername(Username);
            try
            {
                if (userManager.VerifyPassword(Password, user.passwordSalt, user.passwordHash))
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.username),
                        new Claim(ClaimTypes.Role, user.Role())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    return RedirectToPage("/Index", new { Name = user.username });
                }
            }
            catch (NullReferenceException)
            {
                ModelState.AddModelError(string.Empty, "Login failed. Please enter correct username/password");
                return Page();
            }

            ModelState.AddModelError(string.Empty, "Login failed. Please enter correct username/password");
            return Page();
        }
    }

}
