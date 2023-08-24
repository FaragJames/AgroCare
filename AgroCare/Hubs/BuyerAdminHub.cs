using Microsoft.AspNetCore.SignalR;

namespace AgroCare.Hubs
{
    public class BuyerAdminHub: Hub
    {
        public async Task SendToAdmin(string userName)
        {
            await Clients.All.SendAsync("ChangeIcon", userName);
        }
    }
}
