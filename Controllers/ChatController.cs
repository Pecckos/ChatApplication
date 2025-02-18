using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PecckosChatProgram.Data;
using PecckosChatProgram.Models;
using System.Security.Claims;
using PecckosChatProgram.Service;
using PecckosChatProgram.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;


namespace PecckosChatProgram.Controllers
{
[Authorize]
public class ChatController : Controller 
{
     private readonly ChatDbContext _context;
     private readonly UserService _userService;
     private readonly IHubContext<ChatHub> _hubContext;
     
     

     public ChatController(ChatDbContext context, UserService userService, IHubContext<ChatHub> hubContext)
     {
         _context = context; // Inject the databaseconnection
         _userService = userService; // Inject the service
         _hubContext = hubContext;

     }

    // //Show the chat
    public async Task<IActionResult> Index()
    {
     var messages = await _context.MessagesChat
         .Include(m => m.User)
         .OrderBy(m => m.TimeStamp)
         .ToListAsync();

     return View(messages);
    }


    // //Send a new message
     [HttpPost]
     public async Task<IActionResult> SendMessage([FromBody] ChatMessage chatMessage)
{
    if (string.IsNullOrWhiteSpace(chatMessage.MessagesChat))
    {
        return Json(new { success = false, message = "Meddelandet kan inte vara tomt." });
    }

    var userEmail = User.FindFirstValue(ClaimTypes.Email);
    var userName = User.FindFirstValue(ClaimTypes.Name);

    if (userEmail == null || userName == null)
    {
        return Json(new { success = false, message = "Unauthorized" });
    }

    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

    if (user == null)
    {
        return Json(new { success = false, message = "User not found" });
    }

    var message = new ChatMessage
    {
        UserId = user.Id,
        MessagesChat = chatMessage.MessagesChat,
        TimeStamp = DateTime.Now
    };

    _context.MessagesChat.Add(message);
    await _context.SaveChangesAsync();

    return Json(new { success = true, message = "Message sent and saved" });

    
        }
    }
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
    

