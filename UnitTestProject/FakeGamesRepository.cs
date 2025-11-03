using BLLClassLibrary.Entity;
using BLLClassLibrary.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject
{
    public class FakeGamesRepository : IGames
    {
        private List<Game> games;
        private List<GameVisit> gameVisits;
        private List<string> genres;
        private List<string> platforms;
        private List<string> types;

        public FakeGamesRepository()
        {
            games = new List<Game>
            {
                new VideoGame(1, "Game", "Test Description", "Test Developer",new DateTime(2022, 1, 1), 0, 0, "video", new List<string> { "PC" },  new List<string> { "Horror" }, "http://example.com/image1.jpg"),
                new BoardGame(2, "Game1", "Test Description", "Test Developer",new DateTime(2022, 1, 1), 0, 0, new List<string> { "Dice" }, "board", "http://example.com/image1.jpg")
            };
            gameVisits = new List<GameVisit>();
            genres = new List<string> { "Action", "Adventure", "Puzzle" };
            platforms = new List<string> { "PC", "Console", "Mobile" };
            types = new List<string> { "Board Game", "Card Game", "Video Game" };
        }

        public void AddGame(string gameType, string name, string description, string developer, DateTime releaseDate, List<string> genres, List<string> platforms, List<string> types, string imageUrl)
        {
            Game game = null;
            if (gameType == "video") { game = new VideoGame(games.Count + 1, name, description, developer, releaseDate, 0, 0, gameType, platforms, genres, imageUrl); }
            else if (gameType == "board") { game = new BoardGame(games.Count + 1, name, description, developer, releaseDate, 0, 0, types, gameType, imageUrl); }
            games.Add(game);
        }

        public void AddGameToVisited(int userID, int gameID)
        {
            gameVisits.Add(new GameVisit { UserID = userID, GameID = gameID });
        }

        public void AddVisitorToTheGame(string gameName)
        {
            var game = games.FirstOrDefault(g => g.name.Equals(gameName, StringComparison.OrdinalIgnoreCase));
            if (game != null)
            {
                game.numberOfVisitors++;
            }
        }

        public void DeleteGame(int gameId)
        {
            throw new NotImplementedException();
        }

        public Game FindGameById(int gameId)
        {
            return games.FirstOrDefault(g => g.id == gameId);
        }

        public Game FindGameByName(string name)
        {
            return games.FirstOrDefault(g => g.name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Game> GamesFiltering(int filteringType)
        {
            return games;
        }

        public List<Game> GetUserVisitedGames(int userID)
        {
            return games.Where(g => gameVisits.Any(gv => gv.UserID == userID && gv.GameID == g.id)).ToList();
        }

        public List<string> ReceiveGameGenres(int gameId)
        {
            var game = FindGameById(gameId);
            return (List<string>)game?.GetDataGame(5);
        }

        public List<string> ReceiveGamePlatforms(int gameId)
        {
            var game = FindGameById(gameId);
            return (List<string>)game?.GetDataGame(6);
        }

        public List<string> ReceiveGameTypes(int gameId)
        {
            var game = FindGameById(gameId);
            return (List<string>)game?.GetDataGame(5);
        }

        public List<string> ReceiveGenres() => genres;

        public List<string> ReceivePlatforms() => platforms;

        public List<string> ReceiveTypes() => types;
        public void UpdateGameInformation(string oldName, int id, string name, string description, string developer, DateTime releaseDate, List<string> types, List<string> genres, List<string> platforms, string typeOfTheGame, string imageUrl)
        {
            var game = games.FirstOrDefault(g => g.name.Equals(oldName));
            try
            {
                game.name = name;
                game.description = description;
                game.developer = developer;
                game.releaseDate = releaseDate;
                game.gameType = typeOfTheGame;
                game.imageUrl = imageUrl;
            }
            catch (Exception ex)
            {
                throw new Exception("Game not found");
            }
        }
    }

    public class GameVisit
    {
        public int UserID { get; set; }
        public int GameID { get; set; }
    }
}
