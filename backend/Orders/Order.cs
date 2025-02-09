namespace TmbOrderManagementSystem.Api.Orders
{
    public class Order
    {
        public Guid Id { get; init; }
        public string Client { get; private set; }
        public string Product { get; private set; }
        public double Value { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public enum OrderStatus
        {
            Pending,
            Processing,
            Finished
        }

        public Order(string client, string product, double value)
        {
            Id = Guid.NewGuid();
            Client = client;
            Product = product;
            Value = value;
            Status = OrderStatus.Pending;
            CreatedAt = DateTimeOffset.UtcNow;
        }


        public void setStatus(OrderStatus newStatus)
        {
            Status = newStatus;
        }
    }
}
