using ConCurrency.Data.Dtos.Customers;
using ConCurrency.Data.Dtos.Products;

namespace ConCurrency.Data.Dtos.Orders;

public record OrderDto(
    string OrderId,
    CustomerDto Customer,
    ProductDto Product,
    int Quantity);