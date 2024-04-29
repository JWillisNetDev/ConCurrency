namespace ConCurrency.Data.Dtos.Customers;

public class CreateCustomerDto
{
    [Required]
    public string? Name { get; set; }
}
