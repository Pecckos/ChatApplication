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

        public async Task<User> CreateUserAsync(string userName, string password, string email)
        {
            var user = new User 
            {
                UserName = userName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                Email = email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash ))
                return null;

            return user;

        }
    }
}