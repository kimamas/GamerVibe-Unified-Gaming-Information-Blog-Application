using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BusinessLayer.Managers;
using DALClassLibrary;
using BLLClassLibrary.Entity;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Data.SqlClient;

namespace WebApp.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager userManager;
        private readonly Cloudinary cloudinary;
        public ProfileModel(UserManager userManager)
        {
            this.userManager = userManager;
            Account account = new Account("dwjem2zmu", "495892954549617", "rYEEgAYNX8YVzZ6z2Cw4hWs0SeY");
            cloudinary = new Cloudinary(account);
        }
        [BindProperty]
        public IFormFile ProfileImage { get; set; }
        public User user { get; private set; }
        public List<(string Name, int Points)> levels { get; private set; }
        public double ProgressWidth { get; private set; }
        public int maxPoints { get; private set; }
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
                        user = userManager.FindUserByUsername(username);

                        levels = userManager.GetLevels();

                        maxPoints = levels.Last().Points;
                        ProgressWidth = (double)user.points / maxPoints * 100.0;

                        return Page();
                    }
                    catch (NullReferenceException)
                    {
                        return RedirectToPage("/Index");
                    }
                }
                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPostAsync()
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

                    levels = userManager.GetLevels();
                    maxPoints = levels.Last().Points;
                    ProgressWidth = (double)user.points / maxPoints * 100.0;
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
            return RedirectToPage("Index");
        }
        //public async Task<IActionResult> OnPostDeleteUserAsync(int userId)
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        var usernameClaim = User.FindFirst(ClaimTypes.Name);
        //        if (usernameClaim != null)
        //        {
        //            string username = usernameClaim.Value;
        //            user = userManager.FindUserByUsername(username);
        //            if (user == null)
        //            {
        //                return RedirectToPage("/Index");
        //            }

        //            try
        //            {
        //                userManager.DeleteUser(userId, username);
        //                return RedirectToPage("/Index");
        //            }
        //            catch (SqlException)
        //            {
        //                TempData["ImageFormError"] = "An error occurred while deleting the user.";
        //                return Page();
        //            }
        //            catch (Exception)
        //            {
        //                TempData["ImageFormError"] = "An unexpected error occurred.";
        //                return Page();
        //            }
        //        }
        //    }
        //    return RedirectToPage("/Index");
        //}
    }
}
