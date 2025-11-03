using BLLClassLibrary.Entity;
using BLLClassLibrary.Exceptions;
using BLLClassLibrary.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject.Repositories
{
    public class FakeBlogRepository : IBlog
    {
        private List<Blog> blogs = new List<Blog>();
        private List<Comment> comments = new List<Comment>();
        private List<string> categories = new List<string> { "Category1", "Category2" };
        private List<Like> likes = new List<Like>();

        public void AddBlog(string name, string description, string gameName, string userUsername, string category)
        {
            if (blogs.Any(b => b.Name == name))
                throw new DuplicateBlogNameException();

            Blog blog = new Blog(blogs.Count + 1, gameName, userUsername, category, name , description, DateTime.Now);

            blogs.Add(blog);
        }

        public void UpdateBlog(int blogId, string name, string description, string gameName, string userUsername, string category)
        {
            var blog = blogs.FirstOrDefault(b => b.BlogId == blogId);
            if (blog == null)
                throw new Exception("Blog not found.");
            if (blogs.Any(b => b.Name == name && b.BlogId != blogId))
                throw new DuplicateBlogNameException();

            blog.Name = name;
            blog.Description = description;
            blog.GameName = gameName;
            blog.UserUsername = userUsername;
            blog.Category = category;
        }

        public void DeleteBlog(int blogId)
        {
            blogs.RemoveAll(b => b.BlogId == blogId);
            comments.RemoveAll(c => c.BlogId == blogId);
            likes.RemoveAll(l => l.BlogId == blogId);
        }

        public Blog GetBlogById(int blogId)
        {
            return blogs.FirstOrDefault(b => b.BlogId == blogId);
        }

        public List<Blog> GetBlogs()
        {
            return blogs;
        }

        public List<string> GetBlogCategories()
        {
            return categories;
        }

        public void AddComment(int blogId, string username, string commentText, int? parentCommentId)
        {
            Comment comment = new Comment(comments.Count + 1, blogId, username, commentText, DateTime.Now, parentCommentId, false);
            comments.Add(comment);
        }

        public void UpdateComment(int commentId, string commentText)
        {
            var comment = comments.FirstOrDefault(c => c.CommentId == commentId);
            if (comment == null)
                throw new Exception("Comment not found.");

            comment.CommentText = commentText;
            comment.Updated = true;
        }

        public void DeleteComment(int commentId)
        {
            comments.RemoveAll(c => c.CommentId == commentId);
            likes.RemoveAll(l => l.CommentId == commentId);
        }

        public List<Comment> GetCommentsForBlog(int blogId)
        {
            return comments.Where(c => c.BlogId == blogId).ToList();
        }

        public void AddLike(int? blogId, int userId, int? commentId)
        {
            likes.Add(new Like { BlogId = blogId, UserId = userId, CommentId = commentId });
        }

        public void RemoveLike(int? blogId, int userId, int? commentId)
        {
            likes.RemoveAll(l => l.BlogId == blogId && l.UserId == userId && l.CommentId == commentId);
        }

        public bool HasLike(int? blogId, int userId, int? commentId)
        {
            return likes.Any(l => l.BlogId == blogId && l.UserId == userId && l.CommentId == commentId);
        }

        public int GetLikesCountForBlog(int blogId)
        {
            return likes.Count(l => l.BlogId == blogId);
        }

        public Dictionary<int, int> GetLikesCountForComments()
        {
            var likesCount = new Dictionary<int, int>();

            foreach (var comment in comments)
            {
                int count = likes.Count(l => l.CommentId == comment.CommentId);

                likesCount[comment.CommentId] = count;
            }

            return likesCount;
        }
    }

    public class Like
    {
        public int? BlogId { get; set; }
        public int UserId { get; set; }
        public int? CommentId { get; set; }
    }
}
