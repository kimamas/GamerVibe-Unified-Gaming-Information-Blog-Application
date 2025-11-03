using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BLLClassLibrary.Entity;
using BusinessLayer.Managers;
using DALClassLibrary;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using GamesWebsite.Models;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using BLLClassLibrary.Exceptions;
using System.Data.SqlClient;


namespace WebApp.Pages
{
    public class ProfileEditModel : PageModel
    {
        private readonly UserManager userManager;
        private readonly Cloudinary cloudinary;
        public ProfileEditModel(UserManager userManager)
        {
            this.userManager = userManager;
            Account account = new Account("dwjem2zmu", "495892954549617", "rYEEgAYNX8YVzZ6z2Cw4hWs0SeY");
            cloudinary = new Cloudinary(account);
        }
        public BLLClassLibrary.Entity.User user { get; set; }
        [BindProperty]
        public new GamesWebsite.Models.User UserDetails { get; set; }
        [BindProperty]
        public IFormFile ProfileImage { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name);
                if (usernameClaim != null)
                {
                    string username = usernameClaim.Value;
                    try
                    {
                        this.user = userManager.FindUserByUsername(username);
                        if (user == null)
                        {
                            return RedirectToPage("/Index");
                        }

                        UserDetails = new GamesWebsite.Models.User
                        {
                            username = user.username,
                            email = user.email,
                            dateOfBirth = user.dateOfBirth
                        };

                        return Page();
                    }
                    catch (NullReferenceException)
                    {
                        return RedirectToPage("/Index");
                    }
                }
            }
            return RedirectToPage("/Index");
        }
        public IActionResult OnPostSubmit()
        {
            var usernameClaim = User.FindFirst(ClaimTypes.Name);
            if (usernameClaim != null)
            {
                string username = usernameClaim.Value;
                user = userManager.FindUserByUsername(username);
                try
                {
                    userManager.UpdateUser(user.userId, UserDetails.username, UserDetails.email, true, user.passwordSalt, UserDetails.password, UserDetails.dateOfBirth, user.isAdmin, null);

                    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, UserDetails.username),
                        new Claim(ClaimTypes.Role, user.Role())
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                    return RedirectToPage("/Profile");
                }
                catch (DuplicateUsernameException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Editing failed. {ex.Message}");
                    return Page();
                }

                catch (DuplicateEmailException ex)
                {
                    ModelState.AddModelError(string.Empty, $"Editing failed. {ex.Message}");
                    return Page();
                }
                catch (SqlException)
                {
                    ModelState.AddModelError(string.Empty, $"Editing failed. Try again later");
                    return Page();
                }
                catch (Exception ex)
                {
                    TempData["UserError"] = "Editing failed. Retry after some time.";
                    return Page();
                }
            }
            return RedirectToPage("/Index");

        }
        public async Task<IActionResult> OnPostChangeImageAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name);
                if (usernameClaim != null)
                {
                    string username = usernameClaim.Value;
                    user = userManager.FindUserByUsername(username);
                    if (user == null)
                    {
                        return RedirectToPage("/Index");
                    }

                    UserDetails.username = user.username;
                    UserDetails.email = user.email;
                    UserDetails.dateOfBirth = user.dateOfBirth;
                    try
                    {
                        if (ProfileImage != null && ProfileImage.Length > 0)
                        {
                            using (var stream = new MemoryStream())
                            {
                                await ProfileImage.CopyToAsync(stream);
                                stream.Seek(0, SeekOrigin.Begin);

                                var uploadParams = new ImageUploadParams()
                                {
                                    File = new FileDescription(user.username, stream),
                                    PublicId = $"Users photos/{user.username}"
                                };

                                var uploadResult = cloudinary.Upload(uploadParams);
                                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    string imageUrl = uploadResult.SecureUrl.ToString();
                                    userManager.UpdateUserAvatar(imageUrl, user.userId);
                                    user = userManager.FindUserByUsername(username);
                                }
                                else
                                {
                                    TempData["ImageFormError"] = "Image upload failed!";
                                }
                                return Page();
                            }
                        }
                        TempData["ImageFormError"] = "Please select avatar image.";
                        return Page();
                    }
                    catch (Exception ex)
                    {
                        TempData["ImageFormError"] = "Please try again later.";
                        return Page();
                    }

                }
            }
            return RedirectToPage("/Index");
        }
    }
}

