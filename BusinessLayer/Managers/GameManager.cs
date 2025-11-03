using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using BLLClassLibrary.Entity;
using BLLClassLibrary.Exceptions;
using BLLClassLibrary.Intefaces;

namespace BusinessLayer.Managers
{
    public class GameManager
    {
        private readonly IGames gamesRepository;
        public GameManager(IGames gamesRepository)
        {
            this.gamesRepository = gamesRepository;
        }

        public void AddGame(string gameType, string name, string description, string developer, DateTime releaseDate, List<string> genres, List<string> platforms, List<string> types, string imageUrl)
        {
            if (gamesRepository.FindGameByName(name) != null)
            {
                throw new DuplicateGameNameException();
            }
            gamesRepository.AddGame(gameType, name, description, developer, releaseDate, genres, platforms, types, imageUrl);
        }
        public Game FindGameByName(string name) => gamesRepository.FindGameByName(name);
        public List<string> AddGenres()=>gamesRepository.ReceiveGenres();
        public List<string> AddPlatforms() => gamesRepository.ReceivePlatforms();
        public List<string> AddTypes() => gamesRepository.ReceiveTypes();

        public void UpdateGameInformation(string oldName, int id, string name, string description, string developer, DateTime release, List<string> types, List<string> genres, List<string> platforms, string typeOfTheGame, string imageUrl)
        {
            if (name != oldName && gamesRepository.FindGameByName(name) != null)
            {
                throw new DuplicateGameNameException();
            }
            gamesRepository.UpdateGameInformation(oldName, id, name, description, developer, release, types, genres, platforms, typeOfTheGame, imageUrl);
        }
        public List<Game> GameFiltering(int filteringType, string name, string developer, DateTime releaseYear, List<string> genres, List<string> platforms, List<string> types)
        {
            List<Game> games = gamesRepository.GamesFiltering(filteringType);

            games = games.Where(g =>
                (string.IsNullOrEmpty(name) || g.GetDataGame(0)?.ToString().Contains(name, StringComparison.OrdinalIgnoreCase) == true) &&
                (string.IsNullOrEmpty(developer) || g.GetDataGame(2)?.ToString().Contains(developer, StringComparison.OrdinalIgnoreCase) == true) &&
                (releaseYear == DateTime.Parse("01.01.2000") || ((DateTime?)g.GetDataGame(3))?.Year == releaseYear.Year) &&
                (g.gameType == "video" ?
                    (genres == null || genres.All(genre => (g.GetDataGame(4) as List<string>)?.Contains(genre) == true)) &&
                    (platforms == null || platforms.All(platform => (g.GetDataGame(5) as List<string>)?.Contains(platform) == true)) :
                    true) &&
                (g.gameType == "board" ?
                    (types == null || types.All(type => (g.GetDataGame(4) as List<string>)?.Contains(type) == true)) :
                    true)
            ).ToList();

            return games;
        }
        public List<Game> GetGames() => gamesRepository.GamesFiltering(0);

        public void AddVisitorToTheGame(string gameName) => gamesRepository.AddVisitorToTheGame(gameName);
        public (List<Game> RecVideoGames, List<Game> RecBoardGames) GetUserVisitedGames(int userID)
        {
            List<Game> allViewiedGames = gamesRepository.GetUserVisitedGames(userID);

            Dictionary<string, int> genres = new Dictionary<string, int>();
            Dictionary<string, int> types = new Dictionary<string, int>();
            foreach (Game game in allViewiedGames)
            {
                foreach (string data in game.GetDataGame(4) as List<string>)
                {
                    if (game.gameType == "video")
                    {
                        if (!genres.ContainsKey(data)) { genres[data] = 1; }
                        else { genres[data]++; };
                    }
                    else
                    {
                        if (!types.ContainsKey(data)) { types[data] = 1; }
                        else { types[data]++; };
                    }
                }
            }

            var topGenres = genres.OrderByDescending(g => g.Value).Take(3).ToList();

            var topTypes = types.OrderByDescending(t => t.Value).Take(3).ToList();

            List<Game> allGames = gamesRepository.GamesFiltering(0);

            var filteredVideoGames = allGames.Where(g => g.gameType == "video" &&
            topGenres.All(genre => (g.GetDataGame(4) as List<string>)?.Contains(genre.Key) == true)).OrderByDescending(g => g.numberOfVisitors).ToList();

            var filteredBoardGames = allGames.Where(g => g.gameType == "board" &&
            topTypes.All(type => (g.GetDataGame(4) as List<string>)?.Contains(type.Key) == true)).OrderByDescending(g => g.numberOfVisitors).ToList();



            while (filteredVideoGames.Count < 6 && topGenres.Count > 0)
            {
                topGenres.RemoveAt(topGenres.Count - 1);
                filteredVideoGames.AddRange(allGames.Where(g => g.gameType == "video" &&
            topGenres.All(genre => (g.GetDataGame(4) as List<string>)?.Contains(genre.Key) == true) &&
            !filteredVideoGames.Contains(g)).OrderByDescending(g => g.numberOfVisitors).ToList());
            }

            while (filteredBoardGames.Count < 6 && topTypes.Count > 0)
            {
                topTypes.RemoveAt(topTypes.Count - 1);
                filteredBoardGames.AddRange(allGames.Where(g => g.gameType == "board" &&
            topTypes.All(type => (g.GetDataGame(4) as List<string>)?.Contains(type.Key) == true) &&
            !filteredBoardGames.Contains(g)).OrderByDescending(g => g.numberOfVisitors).ToList());
            }

            if (filteredVideoGames.Count < 9)
            {
                filteredVideoGames.AddRange(allGames.Where(g => g.gameType == "video" && !filteredVideoGames.Contains(g))
                    .OrderByDescending(g => g.numberOfVisitors)
                    .Take(9 - filteredVideoGames.Count)
                    .ToList());
            }

            if (filteredBoardGames.Count < 9)
            {
                filteredBoardGames.AddRange(allGames.Where(g => g.gameType == "board" && !filteredBoardGames.Contains(g))
                    .OrderByDescending(g => g.numberOfVisitors)
                    .Take(9 - filteredBoardGames.Count)
                    .ToList());
            }


            return (filteredVideoGames, filteredBoardGames);
        }
        public void AddGameToVisited(int userId, int gameId) => gamesRepository.AddGameToVisited(userId, gameId);
        public Game FindGameById(int gameId) => gamesRepository.FindGameById(gameId);
        public void DeleteGame(int gameId) => gamesRepository.DeleteGame(gameId);
    }
}
