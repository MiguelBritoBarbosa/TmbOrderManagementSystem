using Microsoft.EntityFrameworkCore;

namespace TmbOrderManagementSystem.Api.Orders
{
    public static class OrdersRoutes
    {
        public static void AddOrdersRoutes(this WebApplication app)
        {
            var ordersRoutes = app.MapGroup("orders");

            ordersRoutes.MapPost("", async (AddOrderRequest request, appDbContext context, ServiceBusHelper serviceBusHelper, CancellationToken ct) =>
            {
                var newOrder = new Order(request.client, request.product, request.value);
                await context.Orders.AddAsync(newOrder);
                await context.SaveChangesAsync(ct);
                var orderResult = new OrderDto(newOrder.Id, newOrder.Client, newOrder.Product, newOrder.Value, newOrder.Status, newOrder.CreatedAt);

                await serviceBusHelper.SendMessageQueue(newOrder);

                return Results.Ok(orderResult);
            });

            ordersRoutes.MapGet("", async (appDbContext context, CancellationToken ct) =>
            {
                var orders = await context.Orders
                .Select(order => new OrderDto(order.Id, order.Client, order.Product, order.Value, order.Status, order.CreatedAt))
                .ToListAsync(ct);
                return orders;
            });


            ordersRoutes.MapGet("{id}", async (Guid id, appDbContext context, CancellationToken ct) =>
            {
                var order = await context.Orders.SingleOrDefaultAsync(order => order.Id == id, ct);
                if (order == null)
                    return Results.NotFound();

                var orderResult = new OrderDto(order.Id, order.Client, order.Product, order.Value, order.Status, order.CreatedAt);
                return Results.Ok(orderResult);
            });
        }
    }
}
