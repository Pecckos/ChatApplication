using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PecckosChatProgram.Models;
using PecckosChatProgram.Data;

namespace PecckosChatProgram.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ChatDbContext _context;


    public HomeController(ILogger<HomeController> logger, ChatDbContext context)
    {
        _logger = logger;
        _context = context;
    }
    
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Delete()
    {
        ViewData["Title"] = "Delete All Messages";
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteAllMessages()
    {
        // Här raderar vi alla meddelanden
        var messages = _context.MessagesChat.ToList();
        _context.MessagesChat.RemoveRange(messages);
        await _context.SaveChangesAsync();

        return RedirectToAction("", "Home");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
