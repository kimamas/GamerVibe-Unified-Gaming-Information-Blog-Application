using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using BLLClassLibrary;
using BLLClassLibrary.Entity;
using Microsoft.Extensions.Configuration;
namespace DALClassLibrary
{
    public class UserRepository : IUser, IBaseDAL
    {
        public string connectionString { get; set; }
        public UserRepository()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public void AddUser(string username, string email, string passwordSalt, string passwordHash, DateTime dateOfBirth, bool isAdmin, string imageUrl)
        {
            string insertQuery = @"INSERT INTO [User] (username, dateOfBirth, email, passwordSalt, passwordHash, websiteLevelInt, admin, points,ImageUrl) 
                               VALUES (@username, @dateOfBirth, @email, @passwordSalt, @passwordHash, @websiteLevelInt, @admin, @points,@imageUrl); ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {


                    SqlCommand command = new SqlCommand(insertQuery, connection, transaction);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@passwordSalt", passwordSalt);
                    command.Parameters.AddWithValue("@passwordHash", passwordHash);
                    command.Parameters.AddWithValue("@websiteLevelInt", 1);
                    command.Parameters.AddWithValue("@admin", isAdmin);
                    command.Parameters.AddWithValue("@points", 0);
                    command.Parameters.AddWithValue("@imageUrl", imageUrl);
                    command.ExecuteNonQuery();

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
        public List<string> GetUsersEmails()
        {
            string query = "Select email From [User]; ";
            List<string> emails = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader userReader = command.ExecuteReader())
                        {
                            while (userReader.Read())
                            {
                                string email = userReader["email"].ToString();
                                emails.Add(email);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException) { }
            return emails;
        }
        public List<string> GetUsersUsernames()
        {
            string query = "Select username From [User]; ";
            List<string> users = new List<string>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader userReader = command.ExecuteReader())
                        {
                            while (userReader.Read())
                            {
                                string username = userReader["username"].ToString();
                                users.Add(username);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException) { }
            return users;
        }
        public User FindUserByUsername(string username)
        {
            string query = "SELECT u.*, wl.name " +
                           "FROM [User] u " +
                           "JOIN WebsiteLevels wl ON u.websiteLevelInt = wl.websiteLevelId " +
                           "WHERE u.username = @username;";
            User user = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        using (SqlDataReader userReader = command.ExecuteReader())
                        {
                            while (userReader.Read())
                            {
                                int userId = (int)userReader["userId"];
                                DateTime dateOfBirth = (DateTime)userReader["dateOfBirth"];
                                string email = userReader["email"].ToString();
                                string level = userReader["name"].ToString();
                                string passwordSalt = userReader["passwordSalt"].ToString();
                                string passwordHash = userReader["passwordHash"].ToString();
                                bool admin = (bool)userReader["admin"];
                                int points = (int)userReader["points"];
                                string imageUrl = userReader["ImageUrl"].ToString();
                                user = new User(userId, username, email, passwordSalt, passwordHash, level, admin, dateOfBirth, points, imageUrl);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException) { }
            return user;
        }
        public void UpdateUser(int userId, string username, string email, string passwordSalt, string passwordHash, DateTime dateOfBirth, bool isAdmin, string imageUrl)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string dropConstraintsQuery = @"
                ALTER TABLE Review NOCHECK CONSTRAINT FK_Review_User; ";

                    SqlCommand dropConstraintsCommand = new SqlCommand(dropConstraintsQuery, connection, transaction);
                    dropConstraintsCommand.ExecuteNonQuery();

                    string updateQueryReviews = "UPDATE Review SET userUsername = @username WHERE userUsername = (SELECT username FROM [User] WHERE userId = @userId);";

                    SqlCommand commandReviews = new SqlCommand(updateQueryReviews, connection, transaction);
                    commandReviews.Parameters.AddWithValue("@username", username);
                    commandReviews.Parameters.AddWithValue("@userId", userId);
                    commandReviews.ExecuteNonQuery();

                    string updateQuery = "UPDATE [User] " +
                        "SET username = @username, " +
                        "dateOfBirth = @dateOfBirth, " +
                        "email = @email, " +
                        "passwordHash = @passwordHash, " +
                        "passwordSalt = @passwordSalt, " +
                        "admin = @isAdmin " +
                        (imageUrl != null ? ", ImageUrl = @ImageUrl " : "") +
                        "WHERE userId = @userId; ";

                    SqlCommand command = new SqlCommand(updateQuery, connection, transaction);

                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@dateOfBirth", dateOfBirth);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@passwordSalt", passwordSalt);
                    command.Parameters.AddWithValue("@passwordHash", passwordHash);
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@isAdmin", isAdmin);
                    if (imageUrl != null)
                    {
                        command.Parameters.AddWithValue("@imageUrl", imageUrl);
                    }
                    command.ExecuteNonQuery();

                    string addConstraintsQuery = @"
                ALTER TABLE Review WITH CHECK CHECK CONSTRAINT FK_Review_User; ";

                    SqlCommand addConstraintsCommand = new SqlCommand(addConstraintsQuery, connection, transaction);
                    addConstraintsCommand.ExecuteNonQuery();

                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                }
            }
        }
        public void UpdateUserPassword(string email, string passwordSalt, string passwordHash)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string updateQuery = "UPDATE [User] " +
                        "SET passwordSalt = @passwordSalt, " +
                        "passwordHash = @passwordHash " +
                        "WHERE email = @email; ";

                    SqlCommand command = new SqlCommand(updateQuery, connection, transaction);

                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@passwordSalt", passwordSalt);
                    command.Parameters.AddWithValue("@passwordHash", passwordHash);
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                }
            }

        }
        public void AddPointsToTheUser(int points, string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string updatePointsQuery = "UPDATE [User] " +
                        "SET points = points + @points " +
                        "WHERE username = @username; ";

                    SqlCommand updatePointsCommand = new SqlCommand(updatePointsQuery, connection, transaction);
                    updatePointsCommand.Parameters.AddWithValue("@username", username);
                    updatePointsCommand.Parameters.AddWithValue("@points", points);
                    updatePointsCommand.ExecuteNonQuery();


                    string getUserPointsQuery = "SELECT points FROM [User] WHERE username = @username; ";
                    SqlCommand getUserPointsCommand = new SqlCommand(getUserPointsQuery, connection, transaction);
                    getUserPointsCommand.Parameters.AddWithValue("@username", username);
                    int updatedPoints = (int)getUserPointsCommand.ExecuteScalar();


                    string getNextLevelQuery = @"
                SELECT TOP 1 websiteLevelId 
                FROM [WebsiteLevels] 
                WHERE points <= @updatedPoints 
                ORDER BY points DESC;";

                    SqlCommand getNextLevelCommand = new SqlCommand(getNextLevelQuery, connection, transaction);
                    getNextLevelCommand.Parameters.AddWithValue("@updatedPoints", updatedPoints);
                    int? nextLevelId = (int?)getNextLevelCommand.ExecuteScalar();

                    if (nextLevelId.HasValue)
                    {

                        string updateUserLevelQuery = "UPDATE [User] " +
                            "SET websiteLevelInt = @nextLevelId " +
                            "WHERE username = @username; ";

                        SqlCommand updateUserLevelCommand = new SqlCommand(updateUserLevelQuery, connection, transaction);
                        updateUserLevelCommand.Parameters.AddWithValue("@nextLevelId", nextLevelId.Value);
                        updateUserLevelCommand.Parameters.AddWithValue("@username", username);
                        updateUserLevelCommand.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                }
            }
        }

        public List<(string Name, int Points)> GetLevels()
        {
            List<(string Name, int Points)> levels = new List<(string Name, int Points)>();

            string query = "Select name, points From [WebsiteLevels]; ";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader userReader = command.ExecuteReader())
                        {
                            while (userReader.Read())
                            {
                                string levelName = userReader["name"].ToString();
                                int levelPoints = (int)userReader["points"];
                                levels.Add((levelName, levelPoints));
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException) { }
            return levels;
        }
        public List<User> GetUsers()
        {
            string query = "SELECT u.*, wl.name " +
                               "FROM [User] u " +
                               "JOIN WebsiteLevels wl ON u.websiteLevelInt = wl.websiteLevelId;";
            List<User> users = new List<User>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader userReader = command.ExecuteReader())
                        {
                            while (userReader.Read())
                            {
                                int userId = (int)userReader["userId"];
                                DateTime dateOfBirth = (DateTime)userReader["dateOfBirth"];
                                string email = userReader["email"].ToString();
                                string username = userReader["username"].ToString();
                                string level = userReader["name"].ToString();
                                string passwordSalt = userReader["passwordSalt"].ToString();
                                string passwordHash = userReader["passwordHash"].ToString();
                                bool admin = (bool)userReader["admin"];
                                int points = (int)userReader["points"];
                                string imageUrl = userReader["ImageUrl"].ToString();
                                users.Add(new User(userId, username, email, passwordSalt, passwordHash, level, admin, dateOfBirth, points, imageUrl));
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException) { }
            return users;
        }
        public User GetUserById(int userId)
        {
            string query = "SELECT u.*, wl.name " +
                               "FROM [User] u " +
                               "JOIN WebsiteLevels wl ON u.websiteLevelInt = wl.websiteLevelId " +
                               "WHERE userId = @userId ;";
            User user = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        using (SqlDataReader userReader = command.ExecuteReader())
                        {
                            while (userReader.Read())
                            {
                                DateTime dateOfBirth = (DateTime)userReader["dateOfBirth"];
                                string email = userReader["email"].ToString();
                                string username = userReader["username"].ToString();
                                string level = userReader["name"].ToString();
                                string passwordSalt = userReader["passwordSalt"].ToString();
                                string passwordHash = userReader["passwordHash"].ToString();
                                bool admin = (bool)userReader["admin"];
                                int points = (int)userReader["points"];
                                string imageUrl = userReader["ImageUrl"].ToString();
                                user = new User(userId, username, email, passwordSalt, passwordHash, level, admin, dateOfBirth, points, imageUrl);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException) { }
            return user;
        }
        public void UpdateUserAvatar(string imageUrl, int userId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string updateQuery = "UPDATE [User] " +
                        "SET ImageUrl = @imageUrl " +
                        "WHERE userId = @userId; ";

                    SqlCommand command = new SqlCommand(updateQuery, connection, transaction);

                    command.Parameters.AddWithValue("@imageUrl", imageUrl);
                    command.Parameters.AddWithValue("@userId", userId);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                    connection.Close();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                }
            }
        }
        //public void DeleteUser(int userId, string username)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (SqlTransaction transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                using (SqlCommand cmd = new SqlCommand("DELETE FROM UserVisitedGames WHERE userId = @userId", connection, transaction))
        //                {
        //                    cmd.Parameters.AddWithValue("@userId", userId);
        //                    cmd.ExecuteNonQuery();
        //                }

        //                using (SqlCommand cmd = new SqlCommand("DELETE FROM Review WHERE userUsername = (SELECT username FROM [User] WHERE userId = @userId)", connection, transaction))
        //                {
        //                    cmd.Parameters.AddWithValue("@userId", userId);
        //                    cmd.ExecuteNonQuery();
        //                }

        //                using (SqlCommand cmd = new SqlCommand("DELETE FROM Likes WHERE UserId = @UserId", connection, transaction))
        //                {
        //                    cmd.Parameters.AddWithValue("@UserId", userId);
        //                    cmd.ExecuteNonQuery();
        //                }

        //                using (SqlCommand cmd = new SqlCommand("DELETE FROM [Blog] WHERE userUsername = @userUsername", connection, transaction))
        //                {
        //                    cmd.Parameters.AddWithValue("@userUsername", username);
        //                    cmd.ExecuteNonQuery();
        //                }
        //                using (SqlCommand cmd = new SqlCommand("DELETE FROM Comment WHERE username = @username", connection, transaction))
        //                {
        //                    cmd.Parameters.AddWithValue("@username", username);
        //                    cmd.ExecuteNonQuery();
        //                }



        //                using (SqlCommand cmd = new SqlCommand("DELETE FROM [User] WHERE userId = @userId", connection, transaction))
        //                {
        //                    cmd.Parameters.AddWithValue("@userId", userId);
        //                    cmd.ExecuteNonQuery();
        //                }

        //                transaction.Commit();
        //            }
        //            catch (SqlException)
        //            {
        //                transaction.Rollback();
        //            }
        //        }
        //    }
        //}
    }
}
