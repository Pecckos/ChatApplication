using Azure.Identity;
using Microsoft.AspNetCore.SignalR;
using PecckosChatProgram.Models;

namespace PecckosChatProgram.Hubs 
{
    
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage message, string user)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);

        }

    }
}