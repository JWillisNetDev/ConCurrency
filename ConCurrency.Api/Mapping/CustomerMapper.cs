using ConCurrency.Data.Dtos.Customers;
using ConCurrency.Data.Models;

using Riok.Mapperly.Abstractions;

namespace ConCurrency.Api.Mapping;

[Mapper]
public partial class CustomerMapper
{
    public partial CustomerDto CustomerToCustomerDto(Customer customer);
    public partial Customer CreateCustomerDtoToCustomer(CreateCustomerDto dto);
    public partial void UpdateCustomerDtoToCustomer(UpdateCustomerDto dto, Customer customer);
}
