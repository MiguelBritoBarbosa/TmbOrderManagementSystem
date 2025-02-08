namespace TmbOrderManagementSystem.Api.Orders
{
    public static class OrdersRoutes
    {
        public static void AddOrdersRoutes(this WebApplication app)
        {
            app.MapPost("orders", () => "orders");
            app.MapGet("orders", () => "orders");
            app.MapGet("orders/id", () => "orders");
        }
    }
}
