using Microsoft.AspNetCore.SignalR;
using UMS.Infrastructure.Abstraction.Interfaces;

namespace UMS.Infrastructure.Services
{
    public class ChatHub : Hub, IChatHub
    {
        public async Task SendMessage(string partOfTheAppThatSentNotification, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", partOfTheAppThatSentNotification, message);
        }
    }
}