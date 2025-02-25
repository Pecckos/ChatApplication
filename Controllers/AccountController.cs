using Microsoft.AspNetCore.Mvc;
using PecckosChatProgram.Data;
using PecckosChatProgram.Service;
using PecckosChatProgram.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;



namespace PecckosChatProgram.Controllers
{

public class AccountController : Controller
{
    private readonly ChatDbContext _context; 
    private readonly UserService _userService;
    private readonly ILogger<AccountController> _logger;

    public AccountController(ChatDbContext context, UserService userService, ILogger<AccountController> logger)
    {
        _context = context; //Inject the database connection
        _userService = userService; //Inject the service
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegistrationViewModel model)
    {   //Check if the modelstate is valid. all the field are filled and pass validation.
        if (ModelState.IsValid)
        {   
            //Attemt to create a new user with the provided email och password
            var user = await _userService.CreateUserAsync(model.UserName, model.Email, model.Password);
            if (user != null)
            {   
                //Login the user if succsess automaticly then return to chat index page.
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(new ClaimsIdentity(
                    new[] { new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    },

                    CookieAuthenticationDefaults.AuthenticationScheme)));
                return RedirectToAction("Index", "Chat");
            }
            //Or error message if failed
            ModelState.AddModelError("", "Register failed");
        }

        //If modelstate is invalid or registration failed, return to the page with the model.
        return View(model);
    }

   [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        try
        {
            var user = await _userService.AuthenticateAsync(email, password);
            if (user == null){
                ModelState.AddModelError("", "Wrong password or email");
                return View();
            }

            if (user != null)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                    new ClaimsPrincipal(new ClaimsIdentity(
                    new[] { new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email), },
                    CookieAuthenticationDefaults.AuthenticationScheme)));
                return RedirectToAction("Index", "Chat");
            }
            return View();
        }
        catch (Exception ex)
        {
            // Logga felet
            _logger.LogError(ex, "Someting went wrong, opsie..");
            return View("Error");
        }
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }








}





}