namespace ConCurrency.Data.Dtos.Products;

public record CreateProductDto(
    [property: Required] string Name,
    [property: Required] string Description,
    double Price);
