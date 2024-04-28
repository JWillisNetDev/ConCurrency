namespace ConCurrency.Data.Dtos.Orders;

public record UpdateOrderDto(
    [property: Required] string ProductId,
    [property: Required] string CustomerId,
    int Quantity);