using ConCurrency.Data.Dtos.Customers;
using ConCurrency.Data.Dtos.Products;

namespace ConCurrency.Site.HttpClients;

public class ConCurrencyServiceClient : ICustomersServiceClient, IProductsServiceClient
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

    #region Implements ICustomerServiceClient

    public async Task<List<CustomerDto>> GetCustomersAsync(int page = 0, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        List<CustomerDto> customers = await _client.GetFromJsonAsync<List<CustomerDto>>($"/customers?page={page}&pageSize={pageSize}")
                                      ?? throw new InvalidOperationException("Request returned nothing.");
        return customers;
    }

    public async Task<CustomerDto> GetCustomerAsync(string customerId, CancellationToken cancellationToken = default)
    {
        CustomerDto customer = await _client.GetFromJsonAsync<CustomerDto>($"/customers/{customerId}", cancellationToken)
                               ?? throw new InvalidOperationException("Request returned nothing.");
        return customer;
    }

    public async Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto, CancellationToken cancellationToken = default)
    {
        var result = await _client.PostAsJsonAsync("/customers", createCustomerDto, cancellationToken);
        var customer = await _client.GetFromJsonAsync<CustomerDto?>(result.Headers.Location, cancellationToken)
                       ?? throw new InvalidOperationException("Request returned nothing.");
        return customer;
    }

    public async Task<bool> UpdateCustomerAsync(string customerId, UpdateCustomerDto updateCustomerDto, CancellationToken cancellationToken = default)
    {
        var response = await _client.PutAsJsonAsync($"/customers/{customerId}", updateCustomerDto, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCustomerAsync(string customerId, CancellationToken cancellationToken = default)
    {
        var response = await _client.DeleteAsync($"/customers/{customerId}", cancellationToken);
        return response.IsSuccessStatusCode;
    }

    #endregion Implements ICustomerServiceClient
}
