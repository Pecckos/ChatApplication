using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PecckosChatProgram.Data;
using PecckosChatProgram.Models;
using BCrypt.Net;



namespace PecckosChatProgram.Service
{

    public class UserService
    {
        private readonly ChatDbContext _context; 

        public UserService(ChatDbContext context)
        {
            _context = context; 
        }

        public async Task<User> CreateUserAsync(string userName, string email, string password)
        {
            var user = new User 
            {
                UserName = userName,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        //Checks if email and password is correct, if user or password is wrong then return null, otherwise return user.
        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email );
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash ))
                return null;

            return user;

        }


        public async Task<ChatMessage> SendMessageAsync(int userId, string messageContent)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null; // Om användaren inte finns
            }

            var message = new ChatMessage
            {
                UserId = userId,
                MessagesChat = messageContent,
                TimeStamp = DateTime.Now
            };

            _context.MessagesChat.Add(message);
            await _context.SaveChangesAsync();
            return null; // Meddelandet skickades och sparades
        }
    }
}