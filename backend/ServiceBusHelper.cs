using Azure.Messaging.ServiceBus;
using System.Text.Json;
using TmbOrderManagementSystem.Api.Orders;

namespace TmbOrderManagementSystem.Api
{
    public class ServiceBusHelper
    {
        private readonly string connectionString;
        public ServiceBusHelper(IConfiguration config)
        {
            connectionString = config.GetValue<string>("AzureServiceBus") ?? "";
        }
        public async Task SendMessageQueue(Order order)
        {
            string queueName = "order";
            var client = new ServiceBusClient(connectionString);
            var sender = client.CreateSender(queueName);
            var message = new ServiceBusMessage(JsonSerializer.Serialize(order));
            await sender.SendMessageAsync(message);
        }
    }
}
