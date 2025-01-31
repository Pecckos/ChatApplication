using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PecckosChatProgram.Data;
using PecckosChatProgram.Models;
using Microsoft.AspNetCore.Authorization;

namespace PecckosChatProgram.Controllers
{
[Authorize]
public class ChatController : Controller 
{
     private readonly ChatDbContext _context;

     public ChatController(ChatDbContext context)
     {
         _context = context; // Inject the databaseconnection
     }

    // //Show the chat
     public IActionResult Index()
     {
         var messages = _context.Message
         .OrderBy(m => m.TimeStamp) //Sort the messages after time to List
         .ToList();

         return View(messages); //send the messages to the view
     }


    // //Send a new message
     [HttpPost]
     public IActionResult SendMessage(ChatMessage messages)
     {
         if(ModelState.IsValid)
         {
             messages.TimeStamp = DateTime.Now; //Add timestamp
             _context.Message.Add(messages);
             _context.SaveChanges(); //Save to the database

             return RedirectToAction("Index");
         }

         return View("Index", _context.Message.ToList()); //Show errormessage
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
