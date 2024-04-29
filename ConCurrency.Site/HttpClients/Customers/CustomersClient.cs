using ConCurrency.Data.Dtos.Customers;

namespace ConCurrency.Site.HttpClients.Customers;

public class CustomersClient(HttpClient httpClient) : ICustomersClient
{
    public async Task<List<CustomerDto>> GetCustomersAsync(int page = 0, int pageSize = 10, CancellationToken cancellationToken = default)
    {
        List<CustomerDto>? customers = await httpClient.GetFromJsonAsync<List<CustomerDto>>($"/customers?page={page}&pageSize={pageSize}");
        return customers ?? [];
    }

    public async Task<CustomerDto?> GetCustomerAsync(string customerId, CancellationToken cancellationToken = default)
    {
        CustomerDto? customer = await httpClient.GetFromJsonAsync<CustomerDto>($"/customers/{customerId}", cancellationToken);
        return customer;
    }

    public async Task<CustomerDto?> CreateCustomerAsync(CreateCustomerDto createCustomerDto, CancellationToken cancellationToken = default)
    {
        var result = await httpClient.PostAsJsonAsync("/customers", createCustomerDto, cancellationToken);
        if (!result.IsSuccessStatusCode) { return null; }

        var customer = await httpClient.GetFromJsonAsync<CustomerDto?>(result.Headers.Location, cancellationToken);
        return customer;
    }

    public async Task<bool> UpdateCustomerAsync(string customerId, UpdateCustomerDto updateCustomerDto, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/customers/{customerId}", updateCustomerDto, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteCustomerAsync(string customerId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/customers/{customerId}", cancellationToken);
        return response.IsSuccessStatusCode;
    }
}
