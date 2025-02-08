using static TmbOrderManagementSystem.Api.Orders.Order;

namespace TmbOrderManagementSystem.Api.Orders
{
    public record OrderDto(Guid id, string Client, string Product, double Value, OrderStatus Status, DateTimeOffset CreatedAt);

}
