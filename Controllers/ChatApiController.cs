// using Microsoft.AspNetCore.Mvc;
// using PecckosChatProgram.Data;
// using PecckosChatProgram.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.EntityFrameworkCore;

// namespace PecckosChatProgram.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class ChatApiController : ControllerBase
//     {
//         private readonly ChatDbContext _context;

//         public ChatApiController(ChatDbContext context)
//         {
//             _context = context;
//         }

//         // Hämta alla meddelanden
//         [AllowAnonymous]
//         [HttpGet("Messages")]
//         public IActionResult GetMessages()
//         {
//             var messages = _context.Messages
//                 .OrderByDescending(m => m.TimeStamp)
//                 .ToList();

//             return Ok(messages);
//         }

        



//         // Hämta ett enskilt meddelande
//         [AllowAnonymous]
//         [HttpGet("{id}")]
//         public IActionResult GetMessageById(int id)
//         {
//             var message = _context.Messages.Find(id);
//             if (message == null)
//             {
//                 return NotFound();
//             }
//             return Ok(message);
//         }

//         // Skicka ett nytt meddelande
//         [Authorize]
//         [HttpPost("SendMessage")]
//         public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
//         {
//             if (message == null || string.IsNullOrEmpty(message.Messages))
//             {
//                 return BadRequest("Message cannot be empty");
//             }

//             message.TimeStamp = DateTime.UtcNow;
//             await _context.Messages.AddAsync(message);
//             await _context.SaveChangesAsync();

//             return CreatedAtAction(nameof(GetMessageById), new { id = message.id }, message);
//         }
//     }
// }
