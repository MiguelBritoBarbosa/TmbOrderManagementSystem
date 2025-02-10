using static TmbOrderManagementSystem.Api.Orders.Order;

namespace TmbOrderManagementSystem.Api.Orders
{
    public record AddOrderRequest(string client, string product, double value)
    {
        public Dictionary<string, string> Validate()
        {
            var errors = new Dictionary<string, string>();

            if (string.IsNullOrWhiteSpace(client))
                errors["client"] = "O cliente é obrigatório.";

            if (string.IsNullOrWhiteSpace(product))
                errors["product"] = "O produto é obrigatório.";

            if (value <= 0)
                errors["value"] = "O valor deve ser maior que zero.";

            return errors;
        }
    }
}