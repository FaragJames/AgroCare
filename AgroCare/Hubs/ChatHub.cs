using Microsoft.AspNetCore.SignalR;

namespace AgroCare.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message, string destination)
        {
            Console.WriteLine(destination);
            Console.WriteLine(destination);
            Console.WriteLine(destination);
            await Clients.User(destination).SendAsync("ReceiveMessage", user, message);
            //await Clients.All.SendAsync("ReceiveMessage", user, message, destination);
        }
    }
}