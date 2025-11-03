using BLLClassLibrary.Entity;
using BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLClassLibrary.Intefaces
{
    public interface IGames
    {
        public void AddGame(string gameType, string name, string description, string developer, DateTime releaseDate, List<string> genres, List<string> platforms, List<string> types, string imageUrl);
        public List<string> ReceiveGenres();
        public List<string> ReceivePlatforms();
        public List<string> ReceiveTypes();
        public Game FindGameByName(string name);
        public List<string> ReceiveGameGenres(int gameId);
        public List<string> ReceiveGamePlatforms(int gameId);
        public List<string> ReceiveGameTypes(int gameId);
        public void UpdateGameInformation(string oldName, int id, string name, string description, string developer, DateTime releaseDate, List<string> types, List<string> genres, List<string> platforms, string typeOfTheGame, string imageUrl);
        public List<Game> GamesFiltering(int filteringType);
        public void AddVisitorToTheGame(string gameName);
        public List<Game> GetUserVisitedGames(int userID);
        public void AddGameToVisited(int userID, int gameID);
        public Game FindGameById(int gameId);
        public void DeleteGame(int gameId);
    }
}