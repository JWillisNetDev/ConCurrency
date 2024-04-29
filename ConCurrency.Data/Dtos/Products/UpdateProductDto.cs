namespace ConCurrency.Data.Dtos.Products;

public class UpdateProductDto
{
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Description { get; set; }
    public double Price { get; set; }
}