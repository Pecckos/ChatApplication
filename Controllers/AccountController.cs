using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PecckosChatProgram.Data;
using PecckosChatProgram.Service;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using PecckosChatProgram.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


namespace PecckosChatProgram.Controllers
{

public class AccountController : Controller
{
    private readonly ChatDbContext _context; 
    private readonly UserService _userService;

    public AccountController(ChatDbContext context, UserService userService)
    {
        _context = context; //Inject the database connection
        _userService = userService; //Inject the service
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(string userName, string password)
    {
        var user = await _userService.CreateUserAsync(userName, password);
        if (user != null)
        return RedirectToAction("Login");

        ModelState.AddModelError("", "Registration failed");
        return View();
    }

   [HttpPost]
    public async Task<IActionResult> Login(string userName, string password)
    {
        try
        {
            var user = await _userService.AuthenticateAsync(userName, password);
            if (user != null)
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.UserName) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Ogiltigt användarnamn eller lösenord");
            return View();
        }
        catch (Exception ex)
        {
            // Logga felet
            return View("Error");
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }








}





}