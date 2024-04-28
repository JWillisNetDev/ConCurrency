using ConCurrency.Api.Mapping;
using ConCurrency.Data;
using ConCurrency.Data.Dtos.Orders;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConCurrency.Api.Endpoints.Orders;

public class OrdersHandlers
{
    public static async Task<Ok<OrderDto[]>> GetOrders(
        [AsParameters] PaginationParams @params,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        OrderMapper mapper = new();

        var orders = await db.Orders
            .Include(x => x.Product)
            .Include(x => x.Customer)
            .OrderBy(x => x.OrderId)
            .Skip(@params.Page * @params.PageSize)
            .Take(@params.PageSize)
            .Select(x => mapper.OrderToOrderDto(x))
            .ToArrayAsync(cancellationToken);

        return TypedResults.Ok(orders);
    }

    public static async Task<Results<Ok<OrderDto>, NotFound>> GetOrderById(
        [FromRoute] string orderId,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Orders.FindAsync(orderId, cancellationToken) is not { } order)
        {
            return TypedResults.NotFound();
        }

        OrderMapper mapper = new();
        var dto = mapper.OrderToOrderDto(order);

        return TypedResults.Ok(dto);
    }

    public static async Task<Results<CreatedAtRoute, BadRequest<string>>> CreateOrder(
        [FromBody] CreateOrderDto dto,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (dto.Quantity <= 0)
        {
            return TypedResults.BadRequest("Quantity must be greater than 0.");
        }

        if (await db.Products.AllAsync(x => x.ProductId != dto.ProductId, cancellationToken))
        {
            return TypedResults.BadRequest($"Product with id {dto.ProductId} does not exist.");
        }

        if (await db.Customers.AllAsync(x => x.CustomerId != dto.CustomerId, cancellationToken))
        {
            return TypedResults.BadRequest($"Customer with id {dto.CustomerId} does not exist.");
        }

        OrderMapper mapper = new();
        var order = mapper.CreateOrderDtoToOrder(dto);

        db.Orders.Add(order);
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.CreatedAtRoute(
            nameof(GetOrderById),
            new { orderId = order.OrderId });
    }

    public static async Task<Results<NoContent, NotFound, BadRequest<string>>> UpdateOrder(
        [FromRoute] string orderId,
        [FromBody] UpdateOrderDto dto,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Orders.FindAsync(orderId, cancellationToken) is not { } order)
        {
            return TypedResults.NotFound();
        }

        if (dto.Quantity <= 0)
        {
            return TypedResults.BadRequest("Quantity must be greater than 0.");
        }

        if (await db.Products.AllAsync(x => x.ProductId != dto.ProductId, cancellationToken))
        {
            return TypedResults.BadRequest($"Product with id {dto.ProductId} does not exist.");
        }

        if (await db.Customers.AllAsync(x => x.CustomerId != dto.CustomerId, cancellationToken))
        {
            return TypedResults.BadRequest($"Customer with id {dto.CustomerId} does not exist.");
        }

        OrderMapper mapper = new();
        mapper.UpdateOrderDtoToOrder(dto, order);

        db.Orders.Update(order);
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> DeleteOrder(
        [FromRoute] string orderId,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Orders.FindAsync(orderId, cancellationToken) is not { } order)
        {
            return TypedResults.NotFound();
        }

        db.Orders.Remove(order);
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}
