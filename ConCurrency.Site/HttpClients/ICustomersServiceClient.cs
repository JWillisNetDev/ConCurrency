using ConCurrency.Data.Dtos.Customers;

namespace ConCurrency.Site.HttpClients;

public interface ICustomersServiceClient
{
    Task<List<CustomerDto>> GetCustomersAsync(int page = 0, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<CustomerDto> GetCustomerAsync(string customerId, CancellationToken cancellationToken = default);
    Task<CustomerDto> CreateCustomerAsync(CreateCustomerDto createCustomerDto, CancellationToken cancellationToken = default);
    Task<bool> UpdateCustomerAsync(string customerId, UpdateCustomerDto updateCustomerDto, CancellationToken cancellation = default);
    Task<bool> DeleteCustomerAsync(string customerId, CancellationToken cancellationToken = default);
}