namespace ConCurrency.Data.Dtos.Products;

public record UpdateProductDto(
    [property: Required] string Name,
    [property: Required] string Description,
    double Price);