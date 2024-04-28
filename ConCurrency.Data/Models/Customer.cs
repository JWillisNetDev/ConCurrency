namespace ConCurrency.Data.Models;

public class Customer
{
	[Key]
	public string CustomerId { get; set; } = Guid.NewGuid().ToString();

	[Required]
	[StringLength(200)]
	public required string Name { get; set; }

	public IList<Order> Orders { get; set; } = [];
}