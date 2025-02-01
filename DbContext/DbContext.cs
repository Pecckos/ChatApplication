using Microsoft.EntityFrameworkCore;
using PecckosChatProgram.Models;

namespace PecckosChatProgram.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        public DbSet<ChatMessage> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definiera relation mellan ChatMessage och User
            modelBuilder.Entity<ChatMessage>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId)
                .IsRequired(false);

            // Seedar en admin-användare
            var adminUser = new User
            {
                Id = 1,
                UserName = "Admin",
                PasswordHash = "admin_default_hash" // Lägger till en dummy-hash
            };

            modelBuilder.Entity<User>().HasData(adminUser);
            var seedTimeStamp = new DateTime(2024, 1, 31, 12, 0, 0); // ÅÅÅÅ-MM-DD HH:MM:SS

            // Seedar ett första meddelande
            modelBuilder.Entity<ChatMessage>().HasData(
                new ChatMessage
                {
                    id = 1,
                    UserId = 1,
                    Message = "Welcome to Pecckos chat",
                    TimeStamp = seedTimeStamp
                }
            );
        }
    }
}
