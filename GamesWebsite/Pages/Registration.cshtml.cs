using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Text;
using BusinessLayer.Managers;
using System.Security.Cryptography;
using GamesWebsite.Models;
using Microsoft.AspNetCore.Identity;
using DALClassLibrary;
using BLLClassLibrary.Exceptions;
using System.Data.SqlClient;

namespace WebApp.Pages
{
    public class RegistrationModel : PageModel
    {
        private readonly UserManager userManager;
        public RegistrationModel(UserManager userManager)
        {
            this.userManager = userManager;
        }
        [BindProperty]
        public User User { get; set; }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                userManager.AddUser(User.username, User.email, User.password, User.dateOfBirth, null, GenerateRandomAvatar(User.username));

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.username),
                new Claim(ClaimTypes.Role, User.Role())
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));
            }
            catch (DuplicateUsernameException ex)
            {
                ModelState.AddModelError(string.Empty, $"Registration failed. {ex.Message}");
                return Page();
            }

            catch (DuplicateEmailException ex)
            {
                ModelState.AddModelError(string.Empty, $"Registration failed. {ex.Message}");
                return Page();
            }
            catch (SqlException)
            {
                ModelState.AddModelError(string.Empty, $"Registration failed. Try again later");
                return Page();
            }
            return RedirectToPage("/Index", new { Name = User.username });
        }
        private string GenerateRandomAvatar(string username)
        {
            string[] sets = { "set1", "set2", "set3", "set4", "set5" };
            Random random = new Random();
            string set = sets[random.Next(sets.Length)];
            return $"https://robohash.org/{username}?set={set}";
        }
    }
}
