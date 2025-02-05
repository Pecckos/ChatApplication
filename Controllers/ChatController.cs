using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PecckosChatProgram.Data;
using PecckosChatProgram.Models;
using System.Security.Claims;
using PecckosChatProgram.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace PecckosChatProgram.Controllers
{
[Authorize]
public class ChatController : Controller 
{
     private readonly ChatDbContext _context;
     private readonly UserService _userService;

     public ChatController(ChatDbContext context, UserService userService)
     {
         _context = context; // Inject the databaseconnection
         _userService = userService; // Inject the service
     }

    // //Show the chat
     public IActionResult Index()
     {
         var messages = _context.Messages
         .Include(m => m.User) //Include User information
         .OrderBy(m => m.TimeStamp) //Sort the messages after time to List
         .ToList();

         return View(messages); //send the messages to the view
     }


    // //Send a new message
     [HttpPost]
     public async Task<IActionResult> SendMessage(ChatMessage messages)
     {
        if (!ModelState.IsValid)
        {
            return View("Index", await _context.Messages.Include(m => m.User).OrderBy(m => m.TimeStamp).ToListAsync());
        }

        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        var userName = User.FindFirstValue(ClaimTypes.Name);
        Console.WriteLine("Email",ClaimTypes.Email);
        Console.WriteLine("Name",ClaimTypes.Name);

        if (userEmail == null || userName == null )
        {
            return Unauthorized();
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if(user==null)
        {
            return Unauthorized();
        }

        messages.User = user;
        messages.UserId = user.Id;
        messages.TimeStamp = DateTime.Now;

        _context.Messages.Add(messages);
        await _context.SaveChangesAsync();

        

        return RedirectToAction("index");

        

     }
    
        // // Statisk lista för att lagra meddelanden i minnet
        // private static List<ChatMessage> _messages = new List<ChatMessage>();

        // // Visa meddelanden
        // public IActionResult Index()
        // {
        //     return View(_messages); // Skicka den statiska listan till vyn
        // }

        // // Skicka meddelande
        // [HttpPost]
        // public IActionResult SendMessage(ChatMessage chatMessage)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         chatMessage.TimeStamp = DateTime.Now; // Lägg till tidsstämpel
        //         _messages.Add(chatMessage); // Lägg till meddelandet i listan
        //         return RedirectToAction("Index"); // Återgå till vyn
        //     }
        //     return View("Index");
        //}
    }
}
