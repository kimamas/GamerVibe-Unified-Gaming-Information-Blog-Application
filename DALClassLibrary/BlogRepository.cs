using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml.Linq;
using BLLClassLibrary.Entity;
using BLLClassLibrary.Intefaces;
using Microsoft.Extensions.Configuration;
namespace DALClassLibrary
{
    public class BlogRepository : IBaseDAL, IBlog
    {
        public string connectionString { get; set; }
        public BlogRepository()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            connectionString = config.GetConnectionString("DefaultConnection");
        }

        public void AddBlog(string name, string description, string gameName, string userUsername, string category)
        {
            string getCategoryIDQuery = @"SELECT categoryID FROM BlogCategories WHERE name = @name";

            string insertQuery = @"INSERT INTO Blog (name, description, gameName, userUsername, categoryID) 
                       VALUES (@name, @description, @gameName, @userUsername, @categoryID);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlCommand getCategoryIDCommand = new SqlCommand(getCategoryIDQuery, connection, transaction);
                    getCategoryIDCommand.Parameters.AddWithValue("@name", category);

                    object result = getCategoryIDCommand.ExecuteScalar();
                    if (result == null)
                    {
                        throw new Exception("Category not found.");
                    }
                    int categoryID = (int)result;

                    SqlCommand command = new SqlCommand(insertQuery, connection, transaction);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@gameName", gameName);
                    command.Parameters.AddWithValue("@userUsername", userUsername);
                    command.Parameters.AddWithValue("@categoryID", categoryID);
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

        public List<string> GetBlogCategories()
        {
            string query = "SELECT name FROM BlogCategories";
            List<string> categories = new List<string>();
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
                                string category = userReader["name"].ToString();

                                categories.Add(category);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (SqlException) { }
            return categories;
        }

        public List<Blog> GetBlogs()
        {
            List<Blog> blogs = new List<Blog>();
            string getBlogsQuery = @"SELECT b.blogID, b.name, b.description, b.gameName, b.userUsername, b.categoryID, b.createDate, c.name AS categoryName
                FROM Blog b JOIN BlogCategories c ON b.categoryID = c.categoryID ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(getBlogsQuery, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["name"].ToString();
                                string description = reader["description"].ToString();
                                string gameName = (string)reader["gameName"];
                                string userUsername = (string)reader["userUsername"];
                                int blogId = (int)reader["blogID"];
                                string categoryName = reader["categoryName"].ToString();
                                DateTime created = (DateTime)reader["createDate"];

                                blogs.Add(new Blog(blogId, gameName, userUsername, categoryName, name, description, created));
                            }
                        }
                    }
                    connection.Close();
                }
                catch (SqlException) { }
            }
            return blogs;
        }
        public void UpdateBlog(int blogId, string name, string description, string gameName, string userUsername, string category)
        {
            string getCategoryIDQuery = @"SELECT categoryID FROM BlogCategories WHERE name = @name";

            string updateQuery = @"UPDATE Blog 
                           SET name = @name, description = @description, gameName = @gameName, userUsername = @userUsername, categoryID = @categoryID
                           WHERE blogID = @blogID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    SqlCommand getCategoryIDCommand = new SqlCommand(getCategoryIDQuery, connection, transaction);
                    getCategoryIDCommand.Parameters.AddWithValue("@name", category);

                    object result = getCategoryIDCommand.ExecuteScalar();
                    if (result == null)
                    {
                        throw new Exception("Category not found.");
                    }
                    int categoryID = (int)result;

                    SqlCommand command = new SqlCommand(updateQuery, connection, transaction);
                    command.Parameters.AddWithValue("@blogID", blogId);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@description", description);
                    command.Parameters.AddWithValue("@gameName", gameName);
                    command.Parameters.AddWithValue("@userUsername", userUsername);
                    command.Parameters.AddWithValue("@categoryID", categoryID);

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
        public Blog GetBlogById(int BlogId)
        {
            Blog blog = null;
            string getBlogsQuery = @"SELECT b.blogID, b.name, b.description, b.gameName, b.userUsername, b.categoryID, b.createDate, c.name AS categoryName
                FROM Blog b JOIN BlogCategories c ON b.categoryID = c.categoryID WHERE blogID = @blogID ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getBlogsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@blogID", BlogId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string name = reader["name"].ToString();
                                string description = reader["description"].ToString();
                                string gameName = (string)reader["gameName"];
                                string userUsername = (string)reader["userUsername"];
                                int blogId = (int)reader["blogID"];
                                string categoryName = reader["categoryName"].ToString();
                                DateTime created = (DateTime)reader["createDate"];

                                blog = new Blog(blogId, gameName, userUsername, categoryName, name, description, created);
                            }
                        }
                    }
                    connection.Close();
                }
                catch (SqlException) { }
            }
            return blog;
        }
        public void AddComment(int blogId, string username, string commentText, int? parentCommentId)
        {
            string insertQuery = null;
            if (parentCommentId != null)
            {
                insertQuery = @"INSERT INTO Comment (blogId, username, commentText, parentCommentId) 
                       VALUES (@blogId, @username, @commentText, @parentCommentId);";
            }
            else
            {
                insertQuery = @"INSERT INTO Comment (blogId, username, commentText, parentCommentId) 
                       VALUES (@blogId, @username, @commentText, NULL);";
            }


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand(insertQuery, connection, transaction);
                    command.Parameters.AddWithValue("@blogId", blogId);
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@commentText", commentText);
                    if (parentCommentId != null) command.Parameters.AddWithValue("@parentCommentId", parentCommentId);
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
        public void UpdateComment(int commentId, string commentText)
        {
            string updateQuery = "UPDATE [Comment] " +
                        "SET commentText = @commentText, " +
                        "updated = @updated " +
                        "WHERE commentId = @commentId; ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlCommand command = new SqlCommand(updateQuery, connection, transaction);
                    command.Parameters.AddWithValue("@commentId", commentId);
                    command.Parameters.AddWithValue("@commentText", commentText);
                    command.Parameters.AddWithValue("@updated", true);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException) { transaction.Rollback(); }
            }

        }
        public List<Comment> GetCommentsForBlog(int blogId)
        {
            List<Comment> comments = new List<Comment>();
            string getCommentsQuery = @"SELECT *
                FROM Comment Where blogId = @blogId ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(getCommentsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@blogId", blogId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int commentId = (int)reader["commentId"];
                                string username = reader["username"].ToString();
                                string commentText = reader["commentText"].ToString();
                                DateTime created = (DateTime)reader["createDate"];
                                int? parentCommentId = reader["parentCommentId"] != DBNull.Value ? (int?)reader["parentCommentId"] : null;
                                bool updated = (bool)reader["updated"];

                                comments.Add(new Comment(commentId, blogId, username, commentText, created, parentCommentId, updated));
                            }
                        }
                    }
                    connection.Close();
                }
                catch (SqlException) { }
            }
            return comments;
        }
        public void DeleteComment(int commentId) {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Likes WHERE CommentId = @commentId", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@commentId", commentId);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Comment WHERE commentId = @commentId", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@commentId", commentId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
        public void DeleteBlog(int blogId) {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Likes WHERE BlogId = @BlogId", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@BlogId", blogId);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Comment WHERE blogId = @blogId", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@blogId", blogId);
                            cmd.ExecuteNonQuery();
                        }

                        using (SqlCommand cmd = new SqlCommand("DELETE FROM Blog WHERE blogId = @blogId", connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@blogId", blogId);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }
        public void AddLike(int? blogId, int userId, int? commentId)
        {
            var query = "INSERT INTO Likes (BlogId, UserId, CommentId) VALUES (@BlogId, @UserId, @CommentId)";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    var command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@BlogId", (object)blogId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@CommentId", (object)commentId ?? DBNull.Value);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch (SqlException)
                {
                    transaction.Rollback();
                }

            }
        }
        public void RemoveLike(int? blogId, int userId, int? commentId)
        {
            var query = "DELETE FROM Likes WHERE UserId = @UserId AND (BlogId = @BlogId OR CommentId = @CommentId)";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    var command = new SqlCommand(query, connection, transaction);
                    command.Parameters.AddWithValue("@BlogId", (object)blogId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@CommentId", (object)commentId ?? DBNull.Value);

                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (SqlException)
                {
                    transaction.Rollback();
                }
            }
        }
        public bool HasLike(int? blogId, int userId, int? commentId)
        {
            var query = "SELECT COUNT(*) FROM Likes WHERE UserId = @UserId AND (BlogId = @BlogId OR CommentId = @CommentId)";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BlogId", (object)blogId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@CommentId", (object)commentId ?? DBNull.Value);
                    return (int)command.ExecuteScalar() > 0;
                }
                catch (SqlException) { return false; }
            }
        }
        public int GetLikesCountForBlog(int blogId)
        {
            var query = "SELECT COUNT(*) FROM Likes WHERE BlogId = @BlogId";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@BlogId", blogId);
                    return (int)command.ExecuteScalar();
                }
                catch (SqlException) { return 0; }
            }
        }
        public Dictionary<int, int> GetLikesCountForComments()
        {
            var query = @"
        SELECT CommentId, COUNT(*) AS LikeCount 
        FROM Likes 
        WHERE BlogId IS NULL
        GROUP BY CommentId";

            var likesCount = new Dictionary<int, int>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    var command = new SqlCommand(query, connection);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int commentId = reader.GetInt32(0);
                            int likeCount = reader.GetInt32(1);
                            likesCount[commentId] = likeCount;
                        }
                    }
                }
                catch (SqlException)
                {
                }
            }
            return likesCount;
        }
    }
}
