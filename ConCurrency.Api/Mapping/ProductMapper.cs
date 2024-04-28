using ConCurrency.Data.Dtos.Products;
using ConCurrency.Data.Models;

using Riok.Mapperly.Abstractions;

namespace ConCurrency.Api.Mapping;

[Mapper]
public partial class ProductMapper
{
    public partial ProductDto ProductToProductDto(Product product);
    public partial Product CreateProductDtoToProduct(CreateProductDto dto);
    public partial void UpdateProductDtoToProduct(UpdateProductDto dto, Product product);
}
