using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Microsoft.AspNetCore.Hosting;
using DALClassLibrary;
using BusinessLayer.Managers;
using BLLClassLibrary.Intefaces;
using BLLClassLibrary.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
   .AddCookie(options =>
   {
       options.LoginPath = new PathString("/Privacy");
       options.AccessDeniedPath = new PathString("/Error");
   });
builder.Services.AddSingleton<IUser, UserRepository>();
builder.Services.AddScoped<UserManager>();

builder.Services.AddSingleton<IGames, GameRepository>();
builder.Services.AddScoped<GameManager>();

builder.Services.AddSingleton<IReview, ReviewRepository>();
builder.Services.AddScoped<ReviewManager>();

builder.Services.AddSingleton<IBlog, BlogRepository>();
builder.Services.AddScoped<BlogManager>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();
app.MapRazorPages();

app.Run();
