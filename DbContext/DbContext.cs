using Microsoft.EntityFrameworkCore;
using PecckosChatProgram.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace PecckosChatProgram.Data
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

        public DbSet<ChatMessage> MessagesChat { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            
        }

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
                Email = "admin@admin.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin_hej")
            };

            modelBuilder.Entity<User>().HasData(adminUser);
            
            

            // Seedar ett första meddelande
            modelBuilder.Entity<ChatMessage>().HasData(
                new ChatMessage
                {
                    id = 1,
                    UserId = 1,
                    MessagesChat = "Welcome to Pecckos chat",
                    TimeStamp = new DateTime()
                }
            );
        }
    }
}
