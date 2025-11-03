using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLayer.Managers;
using BLLClassLibrary.Entity;
using UnitTestProject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestsGames
{
    [TestClass]
    public class GameManagerTests
    {
        private GameManager gameManager;
        private FakeGamesRepository fakeGamesRepository;

        [TestInitialize]
        public void Setup()
        {
            fakeGamesRepository = new FakeGamesRepository();
            gameManager = new GameManager(fakeGamesRepository);
        }

        [TestMethod]
        public void AddGame_Should_Add_VideoGame_To_Repository()
        {
            string name = "Game2";
            string description = "Test Description";
            string developer = "Test Developer";
            DateTime releaseDate = new DateTime(2022, 1, 1);
            List<string> genres = new List<string> { "Action" };
            List<string> platforms = new List<string> { "PC" };
            string imageUrl = "http://example.com/image1.jpg";

            gameManager.AddGame("video", name, description, developer, releaseDate, genres, platforms, null, imageUrl);

            Game addedGame = fakeGamesRepository.FindGameByName(name);
            Assert.IsNotNull(addedGame);
            Assert.AreEqual(name, addedGame.name);
            Assert.AreEqual(description, addedGame.description);
            Assert.AreEqual(developer, addedGame.developer);
            Assert.AreEqual(releaseDate, addedGame.releaseDate);
        }
        [TestMethod]
        public void AddGame_Should_Fail_If_Duplicate_Name()
        {
            string name = "Game1";
            string description = "Test Description";
            string developer = "Test Developer";
            DateTime releaseDate = new DateTime(2022, 1, 1);
            List<string> genres = new List<string> { "Action" };
            List<string> platforms = new List<string> { "PC" };
            List<string> types = new List<string> { "Video Game" };
            string imageUrl = "http://example.com/image1.jpg";

            try
            {
                gameManager.AddGame("video", name, description, developer, releaseDate, genres, platforms, types, imageUrl);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("A game with this name already exists.", ex.Message);
            }
        }
        [TestMethod]
        public void FindGameByName_Should_Return_Correct_Game()
        {
            string name = "Game1";

            var foundGame = gameManager.FindGameByName(name);

            Assert.IsNotNull(foundGame);
            Assert.AreEqual(name, foundGame.name);
        }

        [TestMethod]
        public void UpdateGameInformation_Should_Update_Game_Details()
        {
            string oldName = "Game1";
            string newName = "UpdatedGame1";
            string description = "Updated Description";
            string developer = "Updated Developer";
            DateTime releaseDate = new DateTime(2024, 1, 1);
            List<string> types = new List<string> { "Board Game" };
            string imageUrl = "http://example.com/image3.jpg";

            gameManager.UpdateGameInformation(oldName, 2, newName, description, developer, releaseDate, types, null, null, "board", imageUrl);

            var updatedGame = fakeGamesRepository.FindGameByName(newName);
            Assert.IsNotNull(updatedGame);
            Assert.AreEqual(newName, updatedGame.name);
            Assert.AreEqual(description, updatedGame.description);
            Assert.AreEqual(developer, updatedGame.developer);
            Assert.AreEqual(releaseDate, updatedGame.releaseDate);
            Assert.AreEqual(imageUrl, updatedGame.imageUrl);
        }
        [TestMethod]
        public void UpdateGameInformation_Should_Throw_Exception_If_Game_Not_Found()
        {
            string oldName = "NonExistentGame";
            string newName = "UpdatedGame";
            string description = "Updated Description";
            string developer = "Updated Developer";
            DateTime releaseDate = new DateTime(2024, 1, 1);
            List<string> genres = new List<string> { "Puzzle" };
            List<string> platforms = new List<string> { "Mobile" };
            List<string> types = new List<string> { "Board Game" };
            string imageUrl = "http://example.com/image3.jpg";

            try
            {
                gameManager.UpdateGameInformation(oldName, 99, newName, description, developer, releaseDate, types, genres, platforms, "board", imageUrl);
            }
           catch (Exception ex)
            {
                Assert.AreEqual("Game not found", ex.Message);
            }
        }
        [TestMethod]
        public void GameFiltering_Should_Return_Filtered_Games()
        {
            gameManager.AddGame("video", "Game2", "Description2", "Developer2", new DateTime(2020, 1, 1), new List<string> { "Action" }, new List<string> { "PC" }, null, "http://example.com/image1.jpg");
            gameManager.AddGame("video", "Game3", "Description3", "Developer3", new DateTime(2021, 1, 1), new List<string> { "Adventure" }, new List<string> { "Console" }, null, "http://example.com/image2.jpg");

            var filteredGames = gameManager.GameFiltering(0, "Game2", null, new DateTime(2020,1,1), null, null, null);
            List<string> filteredGamesString = filteredGames.Select(g => g.name).ToList();

            Assert.AreEqual(1, filteredGamesString.Count);
            Assert.AreEqual("Game2", filteredGamesString[0]);
        }

        [TestMethod]
        public void GetRecommendedGames_Should_Return_Recommended_Games()
        {
            gameManager.AddGame("video", "Game2", "Description2", "Developer2", new DateTime(2020, 1, 1), new List<string> { "Action" }, new List<string> { "PC" }, null, "http://example.com/image1.jpg");
            gameManager.AddGame("board", "Game3", "Description3", "Developer3", new DateTime(2021, 1, 1), null, null, new List<string> { "Dice" }, "http://example.com/image2.jpg");
            fakeGamesRepository.AddGameToVisited(1, 3);
            fakeGamesRepository.AddGameToVisited(1, 4);

            var visitedGames = gameManager.GetUserVisitedGames(1);

            Assert.AreEqual(4, visitedGames.RecVideoGames.Count + visitedGames.RecBoardGames.Count);
        }

        [TestMethod]
        public void AddVisitorToTheGame_Should_Increment_Visitor_Count()
        {
            string name = "Game1";

            gameManager.AddVisitorToTheGame(name);

            var game = fakeGamesRepository.FindGameByName(name);
            Assert.AreEqual(1, game.numberOfVisitors);
        }

        [TestMethod]
        public void FindGameById_Should_Return_Correct_Game()
        {

            var foundGame = gameManager.FindGameById(1);

            Assert.IsNotNull(foundGame);
            Assert.AreEqual(1, foundGame.id);
        }
        [TestMethod]
        public void FindGameById_Should_Return_Null_For_Invalid_Id()
        {
            var game = gameManager.FindGameById(999);

            Assert.IsNull(game, "Finding a game with an invalid ID should return null");
        }
        [TestMethod]
        public void AddVisitorToTheGame_Should_Not_Increment_For_NonExistent_Game()
        {
            string nonExistentGameName = "NonExistentGame";

            gameManager.AddVisitorToTheGame(nonExistentGameName);

            var game = gameManager.FindGameByName(nonExistentGameName);
            Assert.IsNull(game, "Non-existent game should not have visitors added");
        }
    }
}
