using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using BLLClassLibrary.Entity;
using BLLClassLibrary.Intefaces;
namespace DALClassLibrary
{
    public class ReviewRepository : IBaseDAL, IReview
    {

        public string connectionString { get; set; }
        public ReviewRepository()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");

        }

        public void AddReview(string username, string gameName, string description, int rating, DateTime date)
        {
            string insertReviewQuery = @"INSERT INTO [Review] (userUsername, gameName, description, rating, dateOfPosting, updated) 
                                 VALUES (@userUsername, @gameName, @description, @rating, @dateOfPosting,@updated);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (SqlCommand command = new SqlCommand(insertReviewQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@userUsername", username);
                        command.Parameters.AddWithValue("@gameName", gameName);
                        command.Parameters.AddWithValue("@description", description);
                        command.Parameters.AddWithValue("@rating", rating);
                        command.Parameters.AddWithValue("@dateOfPosting", date);
                        command.Parameters.AddWithValue("@updated", false);
                        command.ExecuteNonQuery();
                    }

                    UpdateGameRating(connection, transaction, gameName);

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw new NotImplementedException("An SQL error occurred: " + ex.Message, ex);
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }
        }

        public List<Review> GetReviewsForTheGame(string gameName)
        {
            string query = "Select * From [Review] " +
                "WHERE gameName = @gameName; ";
            List<Review> reviews = new List<Review>();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@gameName", gameName);
                        command.ExecuteNonQuery();
                        using (SqlDataReader reviewReader = command.ExecuteReader())
                        {
                            while (reviewReader.Read())
                            {
                                int reviewID = (int)reviewReader["reviewID"];
                                string userUsername = reviewReader["userUsername"].ToString();
                                string description = reviewReader["description"].ToString();
                                double rating = (double)reviewReader["rating"];
                                DateTime dateOfPosting = (DateTime)reviewReader["dateOfPosting"];
                                bool updated = (bool)reviewReader["updated"];
                                reviews.Add(new Review(reviewID, userUsername, gameName, description, rating, dateOfPosting, updated));
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException) { }
            return reviews;
        }
        public void DeleteReview(int reviewId, string gameName)
        {
            string query = "DELETE FROM [Review] WHERE reviewID = @reviewId;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@reviewId", reviewId);
                            command.ExecuteNonQuery();

                            UpdateGameRating(connection, transaction, gameName);

                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
        public void UpdateReview(int reviewId, string description, int rating, string gameName)
        {
            string query = @"UPDATE [Review]
        SET description = @description, 
        rating = @rating, updated = 1
        WHERE reviewID = @reviewId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand command = new SqlCommand(query, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@reviewId", reviewId);
                            command.Parameters.AddWithValue("@description", description);
                            command.Parameters.AddWithValue("@rating", rating);
                            command.ExecuteNonQuery();

                            UpdateGameRating(connection, transaction, gameName);

                            transaction.Commit();
                        }
                    }
                    catch (SqlException)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
        private void UpdateGameRating(SqlConnection connection, SqlTransaction transaction, string gameName)
        {
            string updateRatingQuery = @"UPDATE [Game] 
                                 SET rating = (SELECT ISNULL(AVG(rating), 0) 
                                 FROM [Review] 
                                 WHERE gameName = @gameName)  
                                 WHERE name = @gameName;";
            try
            {
                using (SqlCommand command = new SqlCommand(updateRatingQuery, connection, transaction))
                {
                    command.Parameters.AddWithValue("@gameName", gameName);
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                transaction.Rollback();
            }
        }
    }
}
