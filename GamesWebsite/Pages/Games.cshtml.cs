using GamesWebsite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BLLClassLibrary;
using DALClassLibrary;
using BusinessLayer.Managers;
using BLLClassLibrary.Entity;
using System.Security.AccessControl;

namespace GamesWebsite.Pages
{
    public class GamesModel : PageModel
    {
        private readonly GameManager gameManager;
        public GamesModel(GameManager gameManager)
        {
            this.gameManager = gameManager;
            Genres = gameManager.AddGenres();
            Platforms = gameManager.AddPlatforms();
            Types = gameManager.AddTypes();
        }
        [BindProperty(SupportsGet = true)]
        public string SelectedFilterType { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SelectedGameType { get; set; } = null;
        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 12;
        public int TotalPages { get; set; }

        public List<Game> Games { get; set; }
        public List<string> Genres { get; private set; }
        public List<string> Platforms { get; private set; }
        public List<string> Types { get; private set; }
        public void OnGet(
            [FromQuery] string gameType = null,
            [FromQuery] string filterType = null,
            [FromQuery] string name = null,
            [FromQuery] string developer = null,
            [FromQuery] int? releaseDate = null,
            [FromQuery] string genres = null,
            [FromQuery] string platforms = null,
            [FromQuery] string types = null)
        {
            SelectedFilterType = filterType;
            SelectedGameType = gameType;

            List<string> genreList = !string.IsNullOrEmpty(genres) ? new List<string>(genres.Split(',')) : null;
            List<string> platformList = !string.IsNullOrEmpty(platforms) ? new List<string>(platforms.Split(',')) : null;
            List<string> typeList = !string.IsNullOrEmpty(types) ? new List<string>(types.Split(',')) : null;

            DateTime releaseYear = releaseDate.HasValue ? new DateTime(releaseDate.Value, 1, 1) : DateTime.Parse("01.01.2000");

            if (!string.IsNullOrEmpty(filterType))
            {
                Games = gameManager.GameFiltering(int.Parse(filterType), name, developer, releaseYear, genreList, platformList, typeList);
            }
            else
            {
                Games = gameManager.GetGames();
            }

            if (!string.IsNullOrEmpty(gameType))
            {
                Games = Games.FindAll(g => g.gameType.Equals(gameType, StringComparison.OrdinalIgnoreCase));
            }
            TotalPages = (int)Math.Ceiling(Games.Count / (double)PageSize);
            Games = Games.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
        public bool IsSelected(string value)
        {
            return value == SelectedFilterType;
        }
    }
}