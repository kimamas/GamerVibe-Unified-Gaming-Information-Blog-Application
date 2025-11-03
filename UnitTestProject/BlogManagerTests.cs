namespace UnitTestProject.Tests
{
    using Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BLLClassLibrary.Managers;
    using BLLClassLibrary.Exceptions;
    using System.Collections.Generic;
    using System.Linq;

    [TestClass]
    public class BlogManagerTests
    {
        private BlogManager blogManager;
        private FakeBlogRepository fakeBlogRep;

        [TestInitialize]
        public void Setup()
        {
            fakeBlogRep = new FakeBlogRepository();
            blogManager = new BlogManager(fakeBlogRep);
        }

        [TestMethod]
        public void AddBlog_Should_AddBlog_Successfully()
        {
            blogManager.AddBlog("New Blog", "Description", "GameName", "UserUsername", "Category1");

            Assert.AreEqual(1, fakeBlogRep.GetBlogs().Count);
            Assert.AreEqual("New Blog", fakeBlogRep.GetBlogs()[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateBlogNameException))]
        public void AddBlog_Should_ThrowException_If_DuplicateBlogName()
        {
            blogManager.AddBlog("Existing Blog", "Description", "GameName", "UserUsername", "Category1");
            blogManager.AddBlog("Existing Blog", "Description", "GameName", "UserUsername", "Category1");
        }

        [TestMethod]
        public void UpdateBlog_Should_UpdateBlog_Successfully()
        {
            fakeBlogRep.AddBlog("Old Blog", "Description", "GameName", "UserUsername", "Category1");
            var blog = fakeBlogRep.GetBlogs()[0];

            blogManager.UpdateBlog(blog.BlogId, "Old Blog", "Updated Blog", "Description", "GameName", "UserUsername", "Category1");

            Assert.AreEqual("Updated Blog", fakeBlogRep.GetBlogs()[0].Name);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateBlogNameException))]
        public void UpdateBlog_Should_ThrowException_If_DuplicateBlogName()
        {
            fakeBlogRep.AddBlog("Old Blog", "Description", "GameName", "UserUsername", "Category1");
            fakeBlogRep.AddBlog("Existing Blog", "Description", "GameName", "UserUsername", "Category1");
            var blog = fakeBlogRep.GetBlogs().First(b => b.Name == "Old Blog");

            blogManager.UpdateBlog(blog.BlogId, "Old Blog", "Existing Blog", "Description", "GameName", "UserUsername", "Category1");
        }

        [TestMethod]
        public void DeleteBlog_Should_DeleteBlog_Successfully()
        {
            fakeBlogRep.AddBlog("Blog to Delete", "Description", "GameName", "UserUsername", "Category1");
            var blog = fakeBlogRep.GetBlogs()[0];

            blogManager.DeleteBlog(blog.BlogId);

            Assert.AreEqual(0, fakeBlogRep.GetBlogs().Count);
        }

        [TestMethod]
        public void AddComment_Should_AddComment_Successfully()
        {
            blogManager.AddComment(1, "Username", "Comment Text", null);

            Assert.AreEqual(1, fakeBlogRep.GetCommentsForBlog(1).Count);
            Assert.AreEqual("Comment Text", fakeBlogRep.GetCommentsForBlog(1)[0].CommentText);
        }

        [TestMethod]
        public void UpdateComment_Should_UpdateComment_Successfully()
        {
            fakeBlogRep.AddComment(1, "Username", "Old Comment", null);
            var comment = fakeBlogRep.GetCommentsForBlog(1)[0];

            blogManager.UpdateComment(comment.CommentId, "Updated Comment");

            Assert.AreEqual("Updated Comment", fakeBlogRep.GetCommentsForBlog(1)[0].CommentText);
        }

        [TestMethod]
        public void DeleteComment_Should_DeleteComment_Successfully()
        {
            fakeBlogRep.AddComment(1, "Username", "Comment to Delete", null);
            var comment = fakeBlogRep.GetCommentsForBlog(1)[0];

            blogManager.DeleteComment(comment.CommentId);

            Assert.AreEqual(0, fakeBlogRep.GetCommentsForBlog(1).Count);
        }

        [TestMethod]
        public void LikeBlog_Should_AddLike_Successfully()
        {
            blogManager.LikeBlog(1, 1);

            Assert.IsTrue(blogManager.HasLikedBlog(1, 1));
        }

        [TestMethod]
        public void UnlikeBlog_Should_RemoveLike_Successfully()
        {
            fakeBlogRep.AddLike(1, 1, null);

            blogManager.UnlikeBlog(1, 1);

            Assert.IsFalse(blogManager.HasLikedBlog(1, 1));
        }

        [TestMethod]
        public void HasLikedBlog_Should_ReturnTrue_If_Liked()
        {
            fakeBlogRep.AddLike(1, 1, null);

            var result = blogManager.HasLikedBlog(1, 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasLikedBlog_Should_ReturnFalse_If_NotLiked()
        {
            var result = blogManager.HasLikedBlog(1, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LikeComment_Should_AddLike_Successfully()
        {
            blogManager.LikeComment(1, 1);

            Assert.IsTrue(blogManager.HasLikedComment(1, 1));
        }

        [TestMethod]
        public void UnlikeComment_Should_RemoveLike_Successfully()
        {
            fakeBlogRep.AddLike(null, 1, 1);

            blogManager.UnlikeComment(1, 1);

            Assert.IsFalse(blogManager.HasLikedComment(1, 1));
        }

        [TestMethod]
        public void HasLikedComment_Should_ReturnTrue_If_Liked()
        {
            fakeBlogRep.AddLike(null, 1, 1);

            var result = blogManager.HasLikedComment(1, 1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HasLikedComment_Should_ReturnFalse_If_NotLiked()
        {
            var result = blogManager.HasLikedComment(1, 1);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetLikesCountForBlog_Should_Return_LikesCount()
        {
            fakeBlogRep.AddLike(1, 1, null);
            fakeBlogRep.AddLike(1, 2, null);

            var result = blogManager.GetLikesCountForBlog(1);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void GetLikesCountForComments_Should_Return_LikesCount()
        {
            fakeBlogRep.AddComment(1, "Username1", "Comment 1", null);
            fakeBlogRep.AddComment(1, "Username2", "Comment 2", null);

            fakeBlogRep.AddLike(null, 1, 1);
            fakeBlogRep.AddLike(null, 2, 1);

            fakeBlogRep.AddLike(null, 3, 2);

            var expectedLikesCount = new Dictionary<int, int>
            {
                { 1, 2 },
                { 2, 1 }
            };

            var result = blogManager.GetLikesCountForComments();

            CollectionAssert.AreEquivalent(expectedLikesCount, result);
        }

        [TestMethod]
        public void GetBlogCategories_Should_Return_Categories()
        {
            var expectedCategories = new List<string> { "Category1", "Category2" };

            var result = blogManager.GetBlogCategories();

            CollectionAssert.AreEqual(expectedCategories, result);
        }

        [TestMethod]
        public void GetBlogById_Should_Return_Blog()
        {
            fakeBlogRep.AddBlog("Blog1", "Description", "GameName", "UserUsername", "Category1");
            var expectedBlog = fakeBlogRep.GetBlogs()[0];

            var result = blogManager.GetBlogById(expectedBlog.BlogId);

            Assert.AreEqual(expectedBlog, result);
        }

        [TestMethod]
        public void GetBlogById_Should_Return_Null_If_NotFound()
        {
            var result = blogManager.GetBlogById(1);

            Assert.IsNull(result);
        }
    }
}
