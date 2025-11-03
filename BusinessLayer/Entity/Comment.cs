using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLClassLibrary.Entity
{
    public class Comment
    {
        private int commentId;
        private int blogId;
        private string username;
        private string commentText;
        private DateTime createDate;
        private int? parentCommentId;
        private bool updated;
        public Comment(int commentId, int blogId, string username, string commentText, DateTime createDate, int? parentCommentId, bool updated)
        {
            this.commentId = commentId;
            this.blogId = blogId;
            this.username = username;
            this.commentText = commentText;
            this.createDate = createDate;
            this.parentCommentId = parentCommentId;
            this.updated = updated;
        }
        public int CommentId { get => commentId; set => commentId = value; }
        public int BlogId { get => blogId; set => blogId = value; }
        public string Username { get => username; set => username = value; }
        public string CommentText { get => commentText; set => commentText = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public int? ParentCommentId { get => parentCommentId; set => parentCommentId = value; }
        public bool Updated { get => updated; set => updated = value; }
    }
}
