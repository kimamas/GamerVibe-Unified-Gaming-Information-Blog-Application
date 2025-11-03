using BusinessLayer.Managers;
using DALClassLibrary;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace GamesWebsite.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager userManager;
        private readonly GameManager gameManager;
        public IndexModel(UserManager userManager, GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.userManager = userManager;
        }
        [BindProperty(SupportsGet = true)]
        public string Name { get; set; }
        public List<BLLClassLibrary.Entity.Game> Games { get; set; }
        public List<BLLClassLibrary.Entity.Game> RecommendedVideoGames { get; set; }
        public List<BLLClassLibrary.Entity.Game> RecommendedBoardGames { get; set; }

        public void OnGet()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = "Gamer";
            }
            Games = gameManager.GetGames().OrderByDescending(g => g.numberOfVisitors).ToList();
            if (User.Identity.IsAuthenticated)
            {
                var usernameClaim = User.FindFirst(ClaimTypes.Name);
                if (usernameClaim != null)
                {
                    string username = usernameClaim.Value;

                    var recommendedGames = gameManager.GetUserVisitedGames(userManager.FindUserByUsername(username).userId);
                    RecommendedVideoGames = recommendedGames.RecVideoGames;
                    RecommendedBoardGames = recommendedGames.RecBoardGames;
                }
            }
        }
    }
}
