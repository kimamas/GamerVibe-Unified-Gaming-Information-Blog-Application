using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq.Expressions;
using System.Transactions;
using System.Data.Common;
using System.Xml.Linq;
using BLLClassLibrary.Entity;
using BLLClassLibrary.Intefaces;
using System.Collections;
using Microsoft.Extensions.Configuration;
namespace DALClassLibrary
{
    public class GameRepository : IGames, IBaseDAL
    {
        public string connectionString { get; set; }
        public GameRepository()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }
        public void AddGame(string gameType, string name, string description, string developer, DateTime releaseDate, List<string> genres, List<string> platforms, List<string> types, string imageUrl)
        {
            string insertQuery = @"INSERT INTO Game (name, description, developer, releaseDate, rating, numberOfVisitors, gameType, ImageUrl) 
                               VALUES (@Name, @Description, @Developer, @ReleaseDate, @Rating, @NumberOfVisitors, @GameType, @imageUrl);SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {

                    SqlCommand command = new SqlCommand(insertQuery, connection, transaction);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Description", description);
                    command.Parameters.AddWithValue("@Developer", developer);
                    command.Parameters.AddWithValue("@ReleaseDate", releaseDate);
                    command.Parameters.AddWithValue("@Rating", 0);
                    command.Parameters.AddWithValue("@NumberOfVisitors", 0);
                    command.Parameters.AddWithValue("@GameType", gameType);
                    command.Parameters.AddWithValue("@imageUrl", imageUrl);

                    int gameId = Convert.ToInt32(command.ExecuteScalar());

                    if (gameType == "video")
                    {
                        InsertGameGenresInTable(connection, transaction, genres, gameId);
                        InsertGamePlatformsInTable(connection, transaction, platforms, gameId);
                    }
                    else if (gameType == "board") { InsertGameTypesInTable(connection, transaction, types, gameId); }

                    transaction.Commit();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }
        public List<string> ReceiveGenres()
        {
            List<string> genres = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT name FROM VideoGamesGenre";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string genreName = reader["name"].ToString();
                                genres.Add(genreName);
                            }
                        }
                    }
                }
            }
            catch (Exception) { }
            return genres;
        }

        public List<string> ReceivePlatforms()
        {
            List<string> platforms = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT name FROM VideoGamesPlatform";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string platformName = reader["name"].ToString();
                                platforms.Add(platformName);
                            }
                        }
                    }
                }
            }
            catch (SqlException) { }
            return platforms;
        }
        public List<string> ReceiveTypes()
        {
            List<string> types = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT name FROM BoardGamesType";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string typeName = reader["name"].ToString();
                                types.Add(typeName);
                            }
                        }
                    }
                }
            }
            catch (SqlException) { }
            return types;
        }

        public Game FindGameByName(string name)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Game " +
                        "WHERE name = @gameName; ";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@gameName", name);

                        using (SqlDataReader gameReader = command.ExecuteReader())
                        {
                            while (gameReader.Read())
                            {
                                int gameId = (int)gameReader["gameId"];
                                string gameName = gameReader["name"].ToString();
                                string description = gameReader["description"].ToString();
                                string developer = gameReader["developer"].ToString();
                                DateTime releaseDate = (DateTime)gameReader["releaseDate"];
                                double rating = (double)gameReader["rating"];
                                int numberOfVisitors = (int)gameReader["numberOfVisitors"];
                                string gameType = gameReader["gameType"].ToString();
                                string imageUrl = gameReader["ImageUrl"].ToString();
                                connection.Close();
                                if (gameType == "video")
                                {
                                    Game game = new VideoGame(gameId, name, description, developer, releaseDate, rating, numberOfVisitors, gameType, ReceiveGamePlatforms(gameId), ReceiveGameGenres(gameId), imageUrl);
                                    return game;
                                }
                                else
                                {
                                    Game game = new BoardGame(gameId, name, description, developer, releaseDate, rating, numberOfVisitors, ReceiveGameTypes(gameId), gameType, imageUrl);
                                    return game;
                                }
                            }
                        }
                    }
                }

            }
            catch (SqlException) { }
            return null;
        }
        public List<string> ReceiveGameGenres(int gameId)
        {
            List<string> genres = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT genres.name AS genreName " +
                            "FROM VideoGamesGenre genres " +
                            "INNER JOIN VideoGameGenres gameGenres ON genres.genreId = gameGenres.genreId " +
                            "WHERE gameGenres.gameId = @gameId; ";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@gameId", gameId);

                        using (SqlDataReader genrereader = command.ExecuteReader())
                        {
                            while (genrereader.Read())
                            {
                                string genreName = genrereader["genreName"].ToString();
                                if (!string.IsNullOrEmpty(genreName))
                                { genres.Add(genreName); }
                            }
                        }
                    }

                }

            }
            catch (SqlException) { }
            return genres;
        }
        public List<string> ReceiveGamePlatforms(int gameId)
        {
            List<string> platforms = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT platforms.name AS platformName " +
                         "FROM VideoGamesPlatform platforms " +
                         "INNER JOIN VideoGamePlatforms gamePlatforms ON platforms.platformId = gamePlatforms.platformId " +
                         "WHERE gamePlatforms.gameId = @gameId; ";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@gameId", gameId);

                        using (SqlDataReader platformReader = command.ExecuteReader())
                        {
                            while (platformReader.Read())
                            {
                                string platformName = platformReader["platformName"].ToString();
                                if (!string.IsNullOrEmpty(platformName))
                                { platforms.Add(platformName); }
                            }
                        }
                    }
                }
            }
            catch (SqlException) { }
            return platforms;
        }
        public List<string> ReceiveGameTypes(int gameId)
        {
            List<string> types = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT gameTypes.name AS TypeName " +
                         "FROM BoardGamesTypes types " +
                         "INNER JOIN BoardGamesType gameTypes ON types.typeId = gameTypes.typeId " +
                         "WHERE types.gameId = @gameId; ";
                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@gameId", gameId);

                        using (SqlDataReader platformReader = command.ExecuteReader())
                        {
                            while (platformReader.Read())
                            {
                                string platformName = platformReader["TypeName"].ToString();
                                if (!string.IsNullOrEmpty(platformName))
                                { types.Add(platformName); }

                            }
                        }
                    }
                }
            }
            catch (SqlException) { }
            return types;
        }
        public void UpdateGameInformation(string oldName, int id, string name, string description, string developer, DateTime releaseDate, List<string> types, List<string> genres, List<string> platforms, string typeOfTheGame, string imageUrl)
        {
            using (
                SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string dropConstraintsQuery = @"
                ALTER TABLE Review NOCHECK CONSTRAINT FK_Review_Game; ";

                    SqlCommand dropConstraintsCommand = new SqlCommand(dropConstraintsQuery, connection, transaction);
                    dropConstraintsCommand.ExecuteNonQuery();

                    string updateGameQuery = "UPDATE Game " +
                        "SET name = @Name, " +
                        "description = @Description, " +
                        "developer = @Developer, " +
                        "releaseDate = @ReleaseDate " +
                        (imageUrl != null ? ", ImageUrl = @ImageUrl " : "") +
                        "WHERE gameId = @GameId; ";

                    SqlCommand updateGameCommand = new SqlCommand(updateGameQuery, connection, transaction);

                    updateGameCommand.Parameters.AddWithValue("@GameId", id);
                    updateGameCommand.Parameters.AddWithValue("@Name", name);
                    updateGameCommand.Parameters.AddWithValue("@Description", description);
                    updateGameCommand.Parameters.AddWithValue("@Developer", developer);
                    updateGameCommand.Parameters.AddWithValue("@ReleaseDate", releaseDate);
                    if (imageUrl != null)
                    {
                        updateGameCommand.Parameters.AddWithValue("@ImageUrl", imageUrl);
                    }
                    updateGameCommand.ExecuteNonQuery();

                    string updateReviewsQuery = "UPDATE Review " +
                "SET gameName = @Name " +
                "WHERE gameName = @oldName; ";

                    SqlCommand updateReviewsCommand = new SqlCommand(updateReviewsQuery, connection, transaction);
                    updateReviewsCommand.Parameters.AddWithValue("@oldName", oldName);
                    updateReviewsCommand.Parameters.AddWithValue("@Name", name);
                    updateReviewsCommand.ExecuteNonQuery();

                    if (typeOfTheGame == "video")
                    {
                        DeleteGenresWithSameGameId(connection, transaction, id);
                        DeletePlatformsWithSameGameId(connection, transaction, id);

                        InsertGameGenresInTable(connection, transaction, genres, id);
                        InsertGamePlatformsInTable(connection, transaction, platforms, id);
                    }
                    else
                    {
                        DeleteTypesWithSameGameId(connection, transaction, id);

                        InsertGameTypesInTable(connection, transaction, types, id);
                    }

                    string addConstraintsQuery = @"
                ALTER TABLE Review WITH CHECK CHECK CONSTRAINT FK_Review_Game; ";

                    SqlCommand addConstraintsCommand = new SqlCommand(addConstraintsQuery, connection, transaction);
                    addConstraintsCommand.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                }
            }
        }
        private void DeleteGenresWithSameGameId(SqlConnection connection, SqlTransaction transaction, int id)
        {
            try
            {
                string deleteQuery = "DELETE FROM VideoGameGenres WHERE gameId = @GameId;";

                SqlCommand command = new SqlCommand(deleteQuery, connection, transaction);
                command.Parameters.AddWithValue("@GameId", id);
                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                transaction.Rollback();
            }

        }
        private void DeletePlatformsWithSameGameId(SqlConnection connection, SqlTransaction transaction, int id)
        {
            try
            {
                string deleteQuery = "DELETE FROM VideoGamePlatforms WHERE gameId = @GameId;";

                SqlCommand command = new SqlCommand(deleteQuery, connection, transaction);
                command.Parameters.AddWithValue("@GameId", id);
                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                transaction.Rollback();
            }
        }
        private void DeleteTypesWithSameGameId(SqlConnection connection, SqlTransaction transaction, int id)
        {
            try
            {
                string deleteQuery = "DELETE FROM BoardGamesTypes WHERE gameId = @GameId;";

                SqlCommand command = new SqlCommand(deleteQuery, connection, transaction);
                command.Parameters.AddWithValue("@GameId", id);
                command.ExecuteNonQuery();
            }
            catch (SqlException)
            {
                transaction.Rollback();
            }
        }
        private void InsertGameGenresInTable(SqlConnection connection, SqlTransaction transaction, List<string> genres, int gameId)
        {
            try
            {
                foreach (string genre in genres)
                {
                    SqlCommand command = new SqlCommand("SELECT genreId FROM VideoGamesGenre WHERE name = @GenreName", connection, transaction);
                    command.Parameters.AddWithValue("@GenreName", genre);
                    command.CommandTimeout = 60;
                    SqlDataReader reader = command.ExecuteReader();
                    int typeId = 0;
                    if (reader.Read())
                    {
                        typeId = (int)reader["genreId"];
                    }
                    reader.Close();
                    command = new SqlCommand("INSERT INTO VideoGameGenres (gameId, genreId) VALUES (@GameId, @GenreId);", connection, transaction);
                    command.Parameters.AddWithValue("@GameId", gameId);
                    command.Parameters.AddWithValue("@GenreId", typeId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                transaction.Rollback();
            }
        }
        private void InsertGamePlatformsInTable(SqlConnection connection, SqlTransaction transaction, List<string> platforms, int gameId)
        {
            try
            {
                foreach (string platform in platforms)
                {
                    SqlCommand command = new SqlCommand("SELECT platformId FROM VideoGamesPlatform WHERE name = @PlatformName", connection, transaction);
                    command.Parameters.AddWithValue("@PlatformName", platform);

                    SqlDataReader reader = command.ExecuteReader();
                    int typeId = 0;
                    if (reader.Read())
                    {
                        typeId = (int)reader["platformId"];
                    }
                    reader.Close();
                    command = new SqlCommand("INSERT INTO VideoGamePlatforms (gameId, platformId) VALUES (@GameId, @PlatformId);", connection, transaction);
                    command.Parameters.AddWithValue("@GameId", gameId);
                    command.Parameters.AddWithValue("@PlatformId", typeId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                transaction.Rollback();
            }


        }
        private void InsertGameTypesInTable(SqlConnection connection, SqlTransaction transaction, List<string> types, int gameId)
        {
            try
            {
                foreach (string type in types)
                {
                    SqlCommand command = new SqlCommand("SELECT typeId FROM BoardGamesType WHERE name = @TypeName", connection, transaction);
                    command.Parameters.AddWithValue("@TypeName", type);

                    SqlDataReader reader = command.ExecuteReader();
                    int typeId = 0;
                    if (reader.Read())
                    {
                        typeId = (int)reader["typeId"];
                    }
                    reader.Close();
                    command = new SqlCommand("INSERT INTO BoardGamesTypes (gameId, typeId) VALUES (@GameId, @TypeId);", connection, transaction);
                    command.Parameters.AddWithValue("@GameId", gameId);
                    command.Parameters.AddWithValue("@TypeId", typeId);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                transaction.Rollback();
            }

        }


        public List<Game> GamesFiltering(int filteringType)
        {
            List<Game> games = new List<Game>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(SelectFilteringForGames(filteringType), connection))
                    {
                        using (SqlDataReader gameReader = command.ExecuteReader())
                        {
                            while (gameReader.Read())
                            {
                                int gameId = (int)gameReader["gameId"];
                                string gameName = gameReader["name"].ToString();
                                string description = gameReader["description"].ToString();
                                string developer = gameReader["developer"].ToString();
                                DateTime releaseDate = (DateTime)gameReader["releaseDate"];
                                double rating = (double)gameReader["rating"];
                                int numberOfVisitors = (int)gameReader["numberOfVisitors"];
                                string gameType = gameReader["gameType"].ToString();
                                string imageUrl = gameReader["ImageUrl"].ToString();

                                if (gameType == "video")
                                {
                                    Game game = new VideoGame(gameId, gameName, description, developer, releaseDate, rating, numberOfVisitors, gameType, ReceiveGamePlatforms(gameId), ReceiveGameGenres(gameId), imageUrl);
                                    games.Add(game);
                                }
                                else
                                {
                                    Game game = new BoardGame(gameId, gameName, description, developer, releaseDate, rating, numberOfVisitors, ReceiveGameTypes(gameId), gameType, imageUrl);
                                    games.Add(game);
                                }
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException) { }
            return games;
        }
        private string SelectFilteringForGames(int filteringType)
        {
            string query;
            switch (filteringType)
            {
                case 0:
                    return query = "SELECT * FROM Game ";
                case 1:
                    return query = "SELECT * FROM Game " +
                        "ORDER BY name ";
                case 2:
                    return query = "SELECT * FROM Game " +
                        "ORDER BY name DESC ";
                case 3:
                    return query = "SELECT * FROM Game " +
                        "ORDER BY rating DESC ";
                case 4:
                    return query = "SELECT * FROM Game " +
                        "ORDER BY numberOfVisitors DESC ";
                case 5:
                    return query = "SELECT * FROM Game " +
                        "WHERE gameType = 'video' " +
                        "ORDER BY name DESC ";
                case 6:
                    return query = "SELECT * FROM Game " +
                        "WHERE gameType = 'board' " +
                        "ORDER BY name DESC ";
                default:
                    return query = "SELECT * FROM Game ";
            }
        }
        public void AddVisitorToTheGame(string gameName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string updateQuery = "UPDATE Game " +
                        "SET numberOfVisitors = numberOfVisitors + 1 " +
                        "WHERE name = @gameName; ";

                    SqlCommand command = new SqlCommand(updateQuery, connection, transaction);

                    command.Parameters.AddWithValue("@gameName", gameName);
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                }
            }
        }
        public List<Game> GetUserVisitedGames(int userID)
        {
            List<Game> games = new List<Game>();
            string query = @"SELECT g.gameId, g.name, g.description, g.developer, g.releaseDate, g.rating, g.numberOfVisitors, g.gameType, g.ImageUrl
                FROM [dbi536230_gamesweb].[dbo].[Game] g
                JOIN [dbi536230_gamesweb].[dbo].[UserVisitedGames] uv ON g.gameId = uv.gameID
                JOIN [dbi536230_gamesweb].[dbo].[User] u ON uv.userID = u.userId
                WHERE u.userId = @UserId;";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userID);
                        command.ExecuteNonQuery();
                        using (SqlDataReader gameReader = command.ExecuteReader())
                        {
                            while (gameReader.Read())
                            {
                                int gameId = (int)gameReader["gameId"];
                                string gameName = gameReader["name"].ToString();
                                string description = gameReader["description"].ToString();
                                string developer = gameReader["developer"].ToString();
                                DateTime releaseDate = (DateTime)gameReader["releaseDate"];
                                double rating = (double)gameReader["rating"];
                                int numberOfVisitors = (int)gameReader["numberOfVisitors"];
                                string gameType = (string)gameReader["gameType"].ToString();
                                string imageUrl = gameReader["ImageUrl"].ToString();

                                if (gameType == "video")
                                {
                                    Game game = new VideoGame(gameId, gameName, description, developer, releaseDate, rating, numberOfVisitors, gameType, ReceiveGamePlatforms(gameId), ReceiveGameGenres(gameId), imageUrl);
                                    games.Add(game);
                                }
                                else
                                {
                                    Game game = new BoardGame(gameId, gameName, description, developer, releaseDate, rating, numberOfVisitors, ReceiveGameTypes(gameId), gameType, imageUrl);
                                    games.Add(game);
                                }
                            }
                        }
                    }
                    connection.Close();
                }

            }
            catch (SqlException) { }
            return games;
        }

        public void AddGameToVisited(int userID, int gameID)
        {
            string insertQuery = @"INSERT INTO UserVisitedGames (userID, gameID) VALUES (@userId,@gameId); ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    SqlCommand command = new SqlCommand(insertQuery, connection);
                    command.Parameters.AddWithValue("@userId", userID);
                    command.Parameters.AddWithValue("@gameId", gameID);
                    command.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                }
            }
        }
        public Game FindGameById(int gameId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string selectQuery = "SELECT * FROM Game " +
                        "WHERE gameId = @gameId; ";

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        command.Parameters.AddWithValue("@gameId", gameId);

                        using (SqlDataReader gameReader = command.ExecuteReader())
                        {
                            while (gameReader.Read())
                            {
                                string name = gameReader["name"].ToString();
                                string description = gameReader["description"].ToString();
                                string developer = gameReader["developer"].ToString();
                                DateTime releaseDate = (DateTime)gameReader["releaseDate"];
                                double rating = (double)gameReader["rating"];
                                int numberOfVisitors = (int)gameReader["numberOfVisitors"];
                                string gameType = (string)gameReader["gameType"].ToString();
                                string imageUrl = gameReader["ImageUrl"].ToString();

                                connection.Close();
                                if (gameType == "video")
                                {
                                    Game game = new VideoGame(gameId, name, description, developer, releaseDate, rating, numberOfVisitors, gameType, ReceiveGamePlatforms(gameId), ReceiveGameGenres(gameId), imageUrl);
                                    return game;
                                }
                                else
                                {
                                    Game game = new BoardGame(gameId, name, description, developer, releaseDate, rating, numberOfVisitors, ReceiveGameTypes(gameId), gameType, imageUrl);
                                    return game;
                                }
                            }
                        }
                    }
                }

            }
            catch (SqlException) { }
            return null;
        }
        public void DeleteGame(int gameId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM VideoGamePlatforms WHERE gameId = @gameId", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@gameId", gameId);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM VideoGameGenres WHERE gameId = @gameId", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@gameId", gameId);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM BoardGamesTypes WHERE gameId = @gameId", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@gameId", gameId);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM UserVisitedGames WHERE gameId = @gameId", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@gameId", gameId);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Review WHERE gameName = (SELECT name FROM Game WHERE gameId = @gameId)", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@gameId", gameId);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Game WHERE gameId = @gameId", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@gameId", gameId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}

