using ConCurrency.Data.Dtos.Customers;
using ConCurrency.Data.Dtos.Products;

namespace ConCurrency.Data.Dtos.Orders;

public class OrderDto
{
    public string? OrderId { get; set; }
    public CustomerDto? Customer { get; set; }
    public ProductDto? Product { get; set; }
    public int Quantity { get; set; }
}