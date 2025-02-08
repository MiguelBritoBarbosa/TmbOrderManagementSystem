namespace TmbOrderManagementSystem.Api.Orders
{
    public class Order
    {
        public Guid Id { get; init; }
        public string Client { get; private set; }
        public string Product { get; private set; }
        public double Value { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public enum OrderStatus
        {
            Pendente,
            Processando,
            Finalizado
        }

        public Order(string client, string product, double value, OrderStatus status, DateTime createdAt)
        {
            Id = Guid.NewGuid();
            Client = client;
            Product = product;
            Value = value;
            Status = status;
            CreatedAt = createdAt;
        }


    }
}
