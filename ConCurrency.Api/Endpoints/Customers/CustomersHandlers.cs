using ConCurrency.Api.Mapping;
using ConCurrency.Data;
using ConCurrency.Data.Dtos.Customers;
using ConCurrency.Data.Dtos.Orders;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConCurrency.Api.Endpoints.Customers;

public class CustomersHandlers
{
    public static async Task<Ok<CustomerDto[]>> GetCustomers(
        [AsParameters] PaginationParams @params,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        CustomerMapper mapper = new();

        var customers = await db.Customers
            .OrderBy(x => x.CustomerId)
            .Skip(@params.Page * @params.PageSize)
            .Take(@params.PageSize)
            .Select(x => mapper.CustomerToCustomerDto(x))
            .ToArrayAsync(cancellationToken);

        return TypedResults.Ok(customers);
    }

    public static async Task<Results<Ok<CustomerDto>, NotFound>> GetCustomerById(
        [FromRoute] string customerId,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Customers.FindAsync(customerId, cancellationToken) is not { } customer)
        {
            return TypedResults.NotFound();
        }

        CustomerMapper mapper = new();
        var dto = mapper.CustomerToCustomerDto(customer);

        return TypedResults.Ok(dto);
    }

    public static async Task<Results<Ok<OrderDto[]>, NotFound>> GetOrdersForCustomer(
        [FromRoute] string customerId,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Customers.AllAsync(x => x.CustomerId != customerId, cancellationToken))
        {
            return TypedResults.NotFound();
        }

        OrderMapper orderMapper = new();
        var orders = await db.Orders
            .Include(x => x.Product)
            .Include(x => x.Customer)
            .Where(x => x.CustomerId == customerId)
            .Select(x => orderMapper.OrderToOrderDto(x))
            .ToArrayAsync(cancellationToken);

        return TypedResults.Ok(orders);
    }

    public static async Task<Results<CreatedAtRoute, BadRequest>> CreateCustomer(
        [FromBody] CreateCustomerDto dto,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (dto is not { Name: { Length: > 0 } name })
        {
            return TypedResults.BadRequest();
        }

        CustomerMapper mapper = new();
        var customer = mapper.CreateCustomerDtoToCustomer(dto);

        db.Customers.Add(customer);
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.CreatedAtRoute(
            nameof(GetCustomerById),
            new { customerId = customer.CustomerId });
    }

    public static async Task<Results<NoContent, NotFound, BadRequest>> UpdateCustomer(
        [FromRoute] string customerId,
        [FromBody] UpdateCustomerDto dto,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Customers.FindAsync(customerId, cancellationToken) is not { } customer)
        {
            return TypedResults.NotFound();
        }

        if (dto is not { Name: { Length: > 0 } name })
        {
            return TypedResults.BadRequest();
        }

        CustomerMapper mapper = new();
        mapper.UpdateCustomerDtoToCustomer(dto, customer);

        db.Customers.Update(customer);
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> DeleteCustomer(
        [FromRoute] string customerId,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Customers.FindAsync(customerId, cancellationToken) is not { } customer)
        {
            return TypedResults.NotFound();
        }

        db.Customers.Remove(customer);
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}
