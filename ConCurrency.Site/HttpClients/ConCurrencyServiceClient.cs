using ConCurrency.Data.Dtos.Products;

namespace ConCurrency.Site.HttpClients;

public class ConCurrencyServiceClient
{
    private readonly HttpClient _client;

    public ConCurrencyServiceClient(HttpClient httpClient)
    {
        _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<List<ProductDto>> GetProductsAsync(int page = 0, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        List<ProductDto> products = await _client.GetFromJsonAsync<List<ProductDto>>($"/products?page={page}&pageSize={pageSize}")
                                    ?? throw new InvalidOperationException("Request returned nothing.");
        return products;
    }

    public async Task<ProductDto> GetProductAsync(string productId, CancellationToken cancellationToken = default)
    {
        ProductDto product = await _client.GetFromJsonAsync<ProductDto>($"/products/{productId}", cancellationToken)
                             ?? throw new InvalidOperationException("Request returned nothing.");
        return product;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken = default)
    {
        var result = await _client.PostAsJsonAsync("/products", createProductDto, cancellationToken);
        var product = await _client.GetFromJsonAsync<ProductDto?>(result.Headers.Location, cancellationToken)
                      ?? throw new InvalidOperationException("Request returned nothing.");
        return product;
    }
}
