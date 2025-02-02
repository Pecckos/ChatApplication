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

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddScoped<UserService>();


//This code will connect to the database.
 builder.Services.AddDbContext<ChatDbContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnections")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//SignalR-routing
app.MapHub<ChatHub>("/chathub");

//This code will populate the database when it´s starts. 
 using (var scope = app.Services.CreateScope())
 {
     var context = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
     //DatabaseInixializer.SeedData(context);

 }
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseStaticFiles();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
