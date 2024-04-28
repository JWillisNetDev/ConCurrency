namespace ConCurrency.Data.Models;

public class Product
{
    [Key]
    public string ProductId { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [StringLength(200)]
    public required string Name { get; set; }

    [Required]
    [StringLength(600)]
    public required string Description { get; set; }

    public double Price { get; set; }

    public IList<Order> Orders { get; set; } = [];
}
