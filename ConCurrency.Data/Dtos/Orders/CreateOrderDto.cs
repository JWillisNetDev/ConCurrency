namespace ConCurrency.Data.Dtos.Orders;

public class CreateOrderDto
{
    [Required]
    public string? ProductId { get; set; }
    [Required]
    public string? CustomerId { get; set; }
    public int Quantity { get; set; }
}