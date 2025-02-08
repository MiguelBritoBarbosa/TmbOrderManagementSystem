using static TmbOrderManagementSystem.Api.Orders.Order;

namespace TmbOrderManagementSystem.Api.Orders
{
    public record AddOrderRequest(string client, string product, double value);
}
