using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using PecckosChatProgram.Models;


namespace PecckosChatProgram.Data
{

    public class ChatDbContext : DbContext
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }
    public DbSet<ChatMessage> Message { get; set; }
    public DbSet<User> Users { get; set; }

}

public static class DatabaseInixializer 
{
    public static void SeedData(ChatDbContext context){
        if (!context.Message.Any())
        {
            context.Message.AddRange(
                new Models.ChatMessage {User = "Admin", Message = "Welcome to Pecckos chat", TimeStamp = DateTime.Now}
            );
        }
        context.SaveChanges();
    }

}


}