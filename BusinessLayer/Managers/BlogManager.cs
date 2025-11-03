using BLLClassLibrary.Intefaces;
using System;
using BLLClassLibrary.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLClassLibrary.Exceptions;

namespace BLLClassLibrary.Managers
{
    public class BlogManager
    {
        private readonly IBlog iBlog;
        public BlogManager(IBlog iBlog)
        {
            this.iBlog = iBlog;
        }
        public void AddBlog(string name, string description, string gameName, string userUsername, string category)
        {
            if (iBlog.GetBlogs().Any(b => b.Name == name))
            {
                throw new DuplicateBlogNameException();
            }

            iBlog.AddBlog(name, description, gameName, userUsername, category);
        }
        public List<string> GetBlogCategories() => iBlog.GetBlogCategories();
        public List<Blog> GetBlogs() => iBlog.GetBlogs();
        public void UpdateBlog(int blogId, string oldName, string newName, string description, string gameName, string userUsername, string category)
        {
            if (iBlog.GetBlogs().Where(b => b.Name != oldName).Any(b => b.Name == newName))
            {
                throw new DuplicateBlogNameException();
            }
            iBlog.UpdateBlog(blogId, newName, description, gameName, userUsername, category);
        }
        public Blog GetBlogById(int BlogId) => iBlog.GetBlogById(BlogId);
        public void AddComment(int blogId, string username, string commentText, int? parentCommentId) => iBlog.AddComment(blogId, username, commentText, parentCommentId);
        public void UpdateComment(int commentId, string commentText) => iBlog.UpdateComment(commentId, commentText);
        public List<Comment> GetCommentsForBlog(int blogId) => iBlog.GetCommentsForBlog(blogId);
        public void LikeBlog(int blogId, int userId)
        {
            if (!HasLikedBlog(blogId, userId))
            {
                iBlog.AddLike(blogId, userId, null);
            }
        }

        public void UnlikeBlog(int blogId, int userId)
        {
            if (HasLikedBlog(blogId, userId))
            {
                iBlog.RemoveLike(blogId, userId, null);
            }
        }

        public bool HasLikedBlog(int blogId, int userId)
        {
            return iBlog.HasLike(blogId, userId, null);
        }

        public void LikeComment(int commentId, int userId)
        {
            if (!HasLikedComment(commentId, userId))
            {
                iBlog.AddLike(null, userId, commentId);
            }
        }

        public void UnlikeComment(int commentId, int userId)
        {
            if (HasLikedComment(commentId, userId))
            {
                iBlog.RemoveLike(null, userId, commentId);
            }
        }

        public bool HasLikedComment(int commentId, int userId)
        {
            return iBlog.HasLike(null, userId, commentId);
        }
        public int GetLikesCountForBlog(int blogId) => iBlog.GetLikesCountForBlog(blogId);
        public Dictionary<int, int> GetLikesCountForComments() => iBlog.GetLikesCountForComments();
        public void DeleteBlog(int blogId) => iBlog.DeleteBlog(blogId);
        public void DeleteComment(int commentId) => iBlog.DeleteComment(commentId);
    }
}
