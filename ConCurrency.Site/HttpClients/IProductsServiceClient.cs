using ConCurrency.Data.Dtos.Products;

namespace ConCurrency.Site.HttpClients
{
    public interface IProductsServiceClient
    {
        Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken = default);
        Task<ProductDto> GetProductAsync(string productId, CancellationToken cancellationToken = default);
        Task<List<ProductDto>> GetProductsAsync(int page = 0, int pageSize = 10, CancellationToken cancellationToken = default);
    }
}