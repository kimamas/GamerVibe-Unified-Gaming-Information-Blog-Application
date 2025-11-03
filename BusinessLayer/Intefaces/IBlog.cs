using BLLClassLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLClassLibrary.Intefaces
{
    public interface IBlog
    {
        public void AddBlog(string name, string description, string gameName, string userUsername, string category);
        public List<string> GetBlogCategories();
        public List<Blog> GetBlogs();
        public void UpdateBlog(int blogId, string name, string description, string gameName, string userUsername, string category);
        public Blog GetBlogById(int BlogId);
        public void AddComment(int blogId, string username, string commentText, int? parentCommentId);
        public void UpdateComment(int commentId, string commentText);
        public List<Comment> GetCommentsForBlog(int blogId);
        public void AddLike(int? blogId, int userId, int? commentId);
        public void RemoveLike(int? blogId, int userId, int? commentId);
        public bool HasLike(int? blogId, int userId, int? commentId);
        public int GetLikesCountForBlog(int blogId);
        public Dictionary<int, int> GetLikesCountForComments();
        public void DeleteComment(int commentId);
        public void DeleteBlog(int blogId);
    }
}
