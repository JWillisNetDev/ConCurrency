using System.ComponentModel.DataAnnotations.Schema;

namespace ConCurrency.Data.Models;

public class Order
{
	[Key]
	public string OrderId { get; set; } = Guid.NewGuid().ToString();

	[Required]
	public required string CustomerId { get; set; }

	[ForeignKey(nameof(CustomerId))]
	public Customer? Customer { get; set; }

	[Required]
	public required string ProductId { get; set; }

	[ForeignKey(nameof(ProductId))]
	public Product? Product { get; set; }

	public int Quantity { get; set; }
}
