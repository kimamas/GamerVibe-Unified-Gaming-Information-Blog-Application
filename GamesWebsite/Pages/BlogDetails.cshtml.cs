using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLLClassLibrary.Entity;
using BLLClassLibrary.Managers;
using System.Collections.Generic;
using BusinessLayer.Managers;
using BLLClassLibrary.Exceptions;

public class BlogDetailsModel : PageModel
{
    private readonly BlogManager blogManager;
    private readonly UserManager userManager;
    public BlogDetailsModel(BlogManager blogManager, UserManager userManager)
    {
        this.blogManager = blogManager;
        this.userManager = userManager;
        Replies = new Dictionary<int?, List<Comment>>();
        CommentLikesCount = new Dictionary<int, int>();
    }

    [BindProperty(SupportsGet = true)]
    public int BlogId { get; set; }
    public Blog Blog { get; set; }
    public List<Comment> Comments { get; set; }
    public bool HasLikedBlog { get; set; }
    public Dictionary<int, bool> CommentLikes { get; set; }
    public Dictionary<int, int> CommentLikesCount { get; set; }
    public int BlogLikes { get; set; } = 0;

    [BindProperty]
    public string CommentText { get; set; }

    [BindProperty]
    public int? ParentCommentId { get; set; }
    [BindProperty]
    public string editCommentText { get; set; }
    public Dictionary<int?, List<Comment>> Replies { get; set; }
    [BindProperty(SupportsGet = true)]
    public string SortOrder { get; set; } = "newest";
    public void OnGet()
    {
        LoadPageData();
    }

    public void LoadPageData()
    {
        Blog = blogManager.GetBlogById(BlogId);
        Comments = blogManager.GetCommentsForBlog(BlogId);
        if (User.Identity.IsAuthenticated)
        {
            User user = userManager.FindUserByUsername(User.Identity.Name);
            if (user != null)
            {
                HasLikedBlog = blogManager.HasLikedBlog(BlogId, user.userId);
                CommentLikes = Comments.ToDictionary(c => c.CommentId, c => blogManager.HasLikedComment(c.CommentId, user.userId));
            }
        }
        CommentLikesCount = blogManager.GetLikesCountForComments();
        BlogLikes = blogManager.GetLikesCountForBlog(BlogId);
        SetRepliesForComments();

        switch (SortOrder)
        {
            case "oldest":
                Comments = Comments.OrderBy(c => c.CreateDate).ToList();
                break;
            case "mostLiked":
                Comments = Comments.OrderByDescending(c => CommentLikesCount.ContainsKey(c.CommentId) ? CommentLikesCount[c.CommentId] : 0).ToList();
                break;
            default:
                Comments = Comments.OrderByDescending(c => c.CreateDate).ToList();
                break;
        }
    }
    private void SetRepliesForComments()
    {
        foreach (var comment in Comments)
        {
            if (comment.ParentCommentId.HasValue)
            {
                if (!Replies.ContainsKey(comment.ParentCommentId))
                {
                    Replies[comment.ParentCommentId] = new List<Comment>();
                }
                Replies[comment.ParentCommentId].Add(comment);
            }
        }
    }
    public IActionResult OnPostAddComment(string openReplies)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        var username = User.Identity.Name;
        blogManager.AddComment(BlogId, username, CommentText, ParentCommentId);
        ViewData["OpenReplies"] = openReplies;
        LoadPageData();
        return Page();
    }
    public IActionResult OnPostToggleLikeBlog(string openReplies)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        User user = userManager.FindUserByUsername(User.Identity.Name);
        if (blogManager.HasLikedBlog(BlogId, user.userId))
        {
            blogManager.UnlikeBlog(BlogId, user.userId);
        }
        else
        {
            blogManager.LikeBlog(BlogId, user.userId);
        }

        ViewData["OpenReplies"] = openReplies;
        LoadPageData();
        return Page();
    }

    public IActionResult OnPostToggleLikeComment(int commentId, string openReplies)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        User user = userManager.FindUserByUsername(User.Identity.Name);
        if (blogManager.HasLikedComment(commentId, user.userId))
        {
            blogManager.UnlikeComment(commentId, user.userId);
        }
        else
        {
            blogManager.LikeComment(commentId, user.userId);
        }

        ViewData["OpenReplies"] = openReplies;
        LoadPageData();
        return Page();
    }
    public IActionResult OnPostDeleteBlog()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        User user = userManager.FindUserByUsername(User.Identity.Name);
        if (blogManager.GetBlogById(BlogId).UserUsername == user.username)
        {
            blogManager.DeleteBlog(BlogId);
            return RedirectToPage("/Blogs");
        }
        return Page();
    }
    public IActionResult OnPostDeleteComment(int commentId)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        User user = userManager.FindUserByUsername(User.Identity.Name);

        blogManager.DeleteComment(commentId);
        LoadPageData();

        return Page();
    }

    public IActionResult OnPostEditBlog(string name, string description)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        try
        {
            User user = userManager.FindUserByUsername(User.Identity.Name);
            Blog blog = blogManager.GetBlogById(BlogId);
            if (blog.UserUsername == user.username)
            {
                blogManager.UpdateBlog(BlogId, blog.Name, name, description, blog.GameName, blog.UserUsername, blog.Category);
                LoadPageData();
            }
            return Page();
        }
        catch (DuplicateBlogNameException)
        {
            LoadPageData();
            return Page();
        }
        catch (NullReferenceException)
        {
            LoadPageData();
            return Page();
        }
        catch (Exception)
        {
            LoadPageData();
            return Page();
        }
    }

    public IActionResult OnPostEditComment(int commentId, string commentText)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        User user = userManager.FindUserByUsername(User.Identity.Name);

        blogManager.UpdateComment(commentId, commentText);
        LoadPageData();

        return Page();
    }
    public string GetTimeAgo(DateTime dateTime)
    {
        var timeSpan = DateTime.Now.Subtract(dateTime);

        if (timeSpan <= TimeSpan.FromSeconds(60))
            return $"{timeSpan.Seconds} seconds ago";
        else if (timeSpan <= TimeSpan.FromMinutes(60))
            return timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} minutes ago" : "a minute ago";
        else if (timeSpan <= TimeSpan.FromHours(24))
            return timeSpan.Hours > 1 ? $"{timeSpan.Hours} hours ago" : "an hour ago";
        else if (timeSpan <= TimeSpan.FromDays(30))
            return timeSpan.Days > 1 ? $"{timeSpan.Days} days ago" : "yesterday";
        else if (timeSpan <= TimeSpan.FromDays(365))
            return timeSpan.Days > 30 ? $"{timeSpan.Days / 30} months ago" : "a month ago";
        else
            return timeSpan.Days > 365 ? $"{timeSpan.Days / 365} years ago" : "a year ago";
    }
    public bool IsSelected(string value)
    {
        return value == SortOrder;
    }
}
