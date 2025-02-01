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

        public async Task<User> CreateUserAsync(string userName, string password)
        {
            var user = new User 
            {
                UserName = userName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> AuthenticateAsync(string userName, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            return null;

            return user;

        }
    }
}