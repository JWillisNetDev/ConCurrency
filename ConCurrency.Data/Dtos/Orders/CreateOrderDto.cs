namespace ConCurrency.Data.Dtos.Orders;

public record CreateOrderDto(
    [property: Required] string ProductId,
    [property: Required] string CustomerId,
    int Quantity);
