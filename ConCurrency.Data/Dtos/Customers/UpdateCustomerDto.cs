namespace ConCurrency.Data.Dtos.Customers;

public class UpdateCustomerDto
{
    [Required]
    public string? Name { get; set; }
}