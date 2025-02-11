using Microsoft.AspNetCore.SignalR;

namespace TmbOrderManagementSystem.Api.Orders
{
    public class OrderHub: Hub
    {
        public async Task SendOrderUpdate(Order order)
        {
            await Clients.All.SendAsync("ReceiveOrderUpdate", order);
            Console.WriteLine("Update sended.");
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"Cliente conectado: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"Cliente desconectado: {Context.ConnectionId}");
            await base.OnDisconnectedAsync(null);
        }
    }
}
