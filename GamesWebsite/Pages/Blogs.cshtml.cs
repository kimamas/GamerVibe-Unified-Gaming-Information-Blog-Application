using BLLClassLibrary.Entity;
using BLLClassLibrary.Exceptions;
using BLLClassLibrary.Managers;
using BusinessLayer.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

public class BlogsModel : PageModel
{
    private readonly BlogManager blogManager;
    private readonly UserManager userManager;
    private readonly GameManager gameManager;
    public BlogsModel(BlogManager blogManager, UserManager userManager, GameManager gameManager)
    {
        this.blogManager = blogManager;
        this.userManager = userManager;
        this.gameManager = gameManager;
    }
    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 6;
    public int TotalPages { get; set; }
    public List<Blog> Blogs { get; set; }
    public List<string> Categories { get; set; }
    public Dictionary<int, int> BlogLikes { get; set; }
    public HashSet<int> UserLikes { get; set; } = new HashSet<int>();
    public List<string> GameNames { get; set; } = new List<string>();
    [BindProperty]
    public string NewBlogName { get; set; }

    [BindProperty]
    public string NewBlogDescription { get; set; }
    [BindProperty]
    public string GameName { get; set; }
    [BindProperty]
    public string Category { get; set; }
    [BindProperty(SupportsGet = true)]
    public string FilterCategory { get; set; }
    [BindProperty(SupportsGet = true)]
    public string FilterName { get; set; }
    public void OnGet(string filterCategory, string filterName)
    {
        LoadPageData(filterCategory, filterName);
    }

    public void LoadPageData(string filterCategory, string filterName)
    {
        Categories = blogManager.GetBlogCategories();
        FilterCategory = filterCategory ?? "All";
        FilterName = filterName ?? string.Empty;

        Blogs = blogManager.GetBlogs()
            .Where(b => (FilterCategory == "All" || b.Category == FilterCategory) &&
                        (string.IsNullOrEmpty(FilterName) || b.Name.Contains(FilterName, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        BlogLikes = Blogs.ToDictionary(b => b.BlogId, b => blogManager.GetLikesCountForBlog(b.BlogId));
        GameNames = gameManager.GetGames().Select(g=>g.name).ToList();
        if (User.Identity.IsAuthenticated)
        {
            var usernameClaim = User.FindFirst(ClaimTypes.Name);
            if (usernameClaim != null)
            {
                string username = usernameClaim.Value;
                var user = userManager.FindUserByUsername(username);
                foreach (var blog in Blogs)
                {
                    if (blogManager.HasLikedBlog(blog.BlogId, user.userId))
                    {
                        UserLikes.Add(blog.BlogId);
                    }
                }
            }
        }
        TotalPages = (int)Math.Ceiling(Blogs.Count / (double)PageSize);
        Blogs = Blogs.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
    }

    public IActionResult OnPostToggleLikeBlog(int blogId)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        var user = userManager.FindUserByUsername(User.Identity.Name);
        if (blogManager.HasLikedBlog(blogId, user.userId))
        {
            blogManager.UnlikeBlog(blogId, user.userId);
        }
        else
        {
            blogManager.LikeBlog(blogId, user.userId);
        }
        LoadPageData(FilterCategory, FilterName);
        return Page();
    }
    public IActionResult OnPostAddBlog()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        var username = User.Identity.Name;

        try
        {
            blogManager.AddBlog(NewBlogName, NewBlogDescription, GameName, username, Category);
            LoadPageData(FilterCategory, FilterName);
            return Page();
        }
        catch (DuplicateBlogNameException)
        {
            LoadPageData(FilterCategory, FilterName);
            return Page();
        }
        catch (Exception)
        {
            LoadPageData(FilterCategory, FilterName);
            return Page();
        }
    }

    public IActionResult OnPostDeleteBlog(int blogId)
    {
        if (!User.Identity.IsAuthenticated)
        {
            return RedirectToPage("/Login");
        }

        var user = userManager.FindUserByUsername(User.Identity.Name);
        var blog = blogManager.GetBlogById(blogId);
        if (blog.UserUsername == user.username)
        {
            blogManager.DeleteBlog(blogId);
        }
        LoadPageData(FilterCategory, FilterName);
        return Page();
    }
    public IActionResult OnPostFilterBlogs(int blogId)
    {
        LoadPageData(FilterCategory, FilterName);
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
        return value == FilterCategory;
    }
}
