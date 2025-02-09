using Azure.Messaging.ServiceBus;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TmbOrderManagementSystem.Api.Orders;

namespace TmbOrderManagementSystem.Api
{
    public class OrderServiceBusConsumer
    {
        private readonly string connectionString;
        private readonly ServiceBusClient client;
        private readonly ServiceBusProcessor processor;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public OrderServiceBusConsumer(IConfiguration config, IServiceScopeFactory scopeFactory)
        {
            connectionString = config.GetValue<string>("AzureServiceBus") ?? "";
            serviceScopeFactory = scopeFactory;

            client = new ServiceBusClient(connectionString);
            processor = client.CreateProcessor("order", new ServiceBusProcessorOptions());
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            processor.ProcessMessageAsync += ProcessMessageAsync;
            processor.ProcessErrorAsync += ProcessErrorAsync;
            await processor.StartProcessingAsync(cancellationToken);
        }

        private async Task ProcessMessageAsync(ProcessMessageEventArgs args)
        {
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<appDbContext>();

                Console.WriteLine("Processando a mensagem...");

                var receivedOrder = JsonSerializer.Deserialize<Order>(args.Message.Body.ToString());
                if (receivedOrder != null)
                {
                    Console.WriteLine($"Ordem recebida: {receivedOrder.Id}");
                    var order = await context.Orders.SingleOrDefaultAsync(order => order.Id == receivedOrder.Id);
                    if (order == null)
                    {
                        Console.WriteLine($"Ordem {receivedOrder.Id} não encontrada no banco de dados.");
                        return;
                    }
                    order.setStatus(Order.OrderStatus.Processing);
                    await context.SaveChangesAsync();
                    Console.WriteLine($"Status alterado para 'Processing' para a ordem {order.Id}");

                    await Task.Delay(5000);

                    order.setStatus(Order.OrderStatus.Finished);
                    await context.SaveChangesAsync();
                    Console.WriteLine($"Status alterado para 'Finished' para a ordem {order.Id}");
                }
                await args.CompleteMessageAsync(args.Message);
            }
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs args)
        {
            Console.WriteLine($"Erro no processador: {args.Exception.Message}");
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await processor.StopProcessingAsync(cancellationToken);
            await processor.DisposeAsync();
        }
    }
}
