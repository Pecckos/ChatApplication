using Azure.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.SignalR;
using PecckosChatProgram.Models;

namespace PecckosChatProgram.Hubs 
{
    //SignalR hub for chat functionality, allow real-time communication between the server and connected clients. 
    public class ChatHub : Hub
    {
        public async Task SendMessage(string userName, string message)
        {
            var timeStamp = DateTime.Now.ToString("HH:mm");
             //"reciveMessage method will be called on the client side. Sending anonymous object whit user, message and timestamp properties.
            await Clients.All.SendAsync("ReceiveMessage", userName, message, timeStamp);
        }

    }
}