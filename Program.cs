using PecckosChatProgram.Models;
using PecckosChatProgram.Controllers;
using Microsoft.EntityFrameworkCore;
using PecckosChatProgram.Data;
using PecckosChatProgram.Hubs;
using Microsoft.AspNetCore.Identity;
using PecckosChatProgram.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.WebHost.UseUrls("http://localhost:5000");

// Add Swagger services
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddScoped<UserService>();

// This code will connect to the database.
builder.Services.AddDbContext<ChatDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnections")));

var app = builder.Build();


// This code will populate the database when it starts.
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
    // DatabaseInitializer.SeedData(context);
}


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers(); // API-Routing
app.MapHub<ChatHub>("/ChatHub"); // SignalR-routing

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
