using BusinessLayer.Managers;
using DALClassLibrary;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using BLLClassLibrary.Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using BLLClassLibrary.Managers;
using static System.Net.Mime.MediaTypeNames;
namespace GamesWebsite.Pages
{
    public class GameDetailsModel : PageModel
    {
        private readonly GameManager gameManager;
        private readonly ReviewManager reviewManager;
        private readonly UserManager userManager;
        public GameDetailsModel(GameManager gameManager, ReviewManager reviewManager, UserManager userManager)
        {
            this.reviewManager = reviewManager;
            this.gameManager = gameManager;
            this.userManager = userManager;
            genres = new List<string>();
            platforms = new List<string>();
            types = new List<string>();
            reviews = new List<Review>();
            reviews_users = new List<User>();
        }
        public List<User> reviews_users { get; private set; }
        public Game game { get; private set; }
        public List<Review> reviews { get; private set; }
        public List<string> genres { get; private set; }
        public List<string> platforms { get; private set; }
        public List<string> types { get; private set; }
        public string description { get; private set; }
        public int rating { get; private set; }
        public string gameName { get; private set; }
        public bool canAddReview { get; private set; } = true;
        public void OnGet(string gameName, string addVisitor)
        {
            if (addVisitor == "true") { gameManager.AddVisitorToTheGame(gameName); }

            game = gameManager.FindGameByName(gameName);
            if (game.gameType == "video")
            {
                genres = (List<string>)game.GetDataGame(4);
                platforms = (List<string>)game.GetDataGame(5);
            }
            else { types = (List<string>)game.GetDataGame(4); }

            reviews = reviewManager.GetReviewsForTheGame(gameName);
            try
            {
                foreach (Review review in reviews)
                {
                    reviews_users.Add(userManager.FindUserByUsername(review.Username));
                }
            }
            catch (NullReferenceException) { }

            if (User.Identity.IsAuthenticated)
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name);
                if (usernameClaim != null)
                {
                    string username = usernameClaim.Value;
                    if (reviews.Count > 0 && reviews.Any(review => review.Username == username))
                    {
                        canAddReview = false;
                    }
                    gameManager.AddGameToVisited(userManager.FindUserByUsername(username).userId, gameManager.FindGameByName(gameName).id);
                }
            }
        }
        public async Task<IActionResult> OnPostAddReviewAsync(int rating, string description)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name);
                if (usernameClaim != null)
                {
                    string username = usernameClaim.Value;
                    string gameName = Request.Form["gameName"];
                    if (!string.IsNullOrEmpty(gameName) && !string.IsNullOrEmpty(description))
                    {
                        reviewManager.AddReview(username, gameName, description, rating, DateTime.Today);
                        userManager.AddPointsToTheUser(3, username);
                        return RedirectToPage("/GameDetails", new { gameName, addVisitor = "false" });
                    }
                    return Page();
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostUpdateReviewAsync(int reviewId, string description, int rating, string gameName)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name);
                if (usernameClaim != null)
                {
                    reviewManager.UpdateReview(reviewId, description, rating, gameName);
                    return RedirectToPage("/GameDetails", new { gameName, addVisitor = "false" });
                }
            }
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteReviewAsync(int reviewId, string gameName)
        {
            if (User.Identity.IsAuthenticated)
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name);
                if (usernameClaim != null)
                {
                    reviewManager.DeleteReview(reviewId, gameName);
                    return RedirectToPage("/GameDetails", new { gameName, addVisitor = "false" });
                }
            }
            return Page();
        }
    }
}
