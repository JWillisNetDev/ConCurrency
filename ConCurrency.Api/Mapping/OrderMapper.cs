using ConCurrency.Data.Dtos.Orders;
using ConCurrency.Data.Models;

using Riok.Mapperly.Abstractions;

namespace ConCurrency.Api.Mapping;

[Mapper]
public partial class OrderMapper
{
    public partial OrderDto OrderToOrderDto(Order order);
    public partial Order CreateOrderDtoToOrder(CreateOrderDto dto);
    public partial void UpdateOrderDtoToOrder(UpdateOrderDto dto, Order order);
}
