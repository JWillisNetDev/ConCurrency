namespace ConCurrency.Data.Dtos.Products;

public class CreateProductDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    public double Price { get; set; }
}
