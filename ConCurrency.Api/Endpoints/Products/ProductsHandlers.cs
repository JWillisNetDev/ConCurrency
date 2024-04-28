using ConCurrency.Api.Mapping;
using ConCurrency.Data;
using ConCurrency.Data.Dtos.Orders;
using ConCurrency.Data.Dtos.Products;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConCurrency.Api.Endpoints.Products;

public class ProductsHandlers
{
    public static async Task<Ok<ProductDto[]>> GetProducts(
        [AsParameters] PaginationParams @params,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        ProductMapper mapper = new();

        var products = await db.Products
            .OrderBy(p => p.ProductId)
            .Skip(@params.Page * @params.PageSize)
            .Take(@params.PageSize)
            .Select(x => mapper.ProductToProductDto(x))
            .ToArrayAsync(cancellationToken);

        return TypedResults.Ok(products);
    }

    public static async Task<Results<Ok<ProductDto>, NotFound>> GetProductById(
        [FromRoute] string productId,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Products.FindAsync(productId, cancellationToken) is not { } product)
        {
            return TypedResults.NotFound();
        }

        ProductMapper mapper = new();
        var dto = mapper.ProductToProductDto(product);

        return TypedResults.Ok(dto);
    }

    public static async Task<Results<Ok<OrderDto[]>, NotFound>> GetOrdersForProduct(
        [FromRoute] string productId,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Products.AllAsync(x => x.ProductId != productId, cancellationToken))
        {
            return TypedResults.NotFound();
        }

        OrderMapper orderMapper = new();

        var orders = await db.Orders
            .Include(x => x.Product)
            .Include(x => x.Customer)
            .Where(x => x.ProductId == productId)
            .Select(x => orderMapper.OrderToOrderDto(x))
            .ToArrayAsync(cancellationToken);

        return TypedResults.Ok(orders);
    }

    public static async Task<Results<CreatedAtRoute, BadRequest>> CreateProduct(
        [FromBody] CreateProductDto dto,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (dto is not { Name: { Length: > 0 } name, Description: { Length: > 0 } description })
        {
            return TypedResults.BadRequest();
        }

        ProductMapper mapper = new();
        var product = mapper.CreateProductDtoToProduct(dto);

        db.Products.Add(product);
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.CreatedAtRoute(
            nameof(GetProductById),
            new { productId = product.ProductId });
    }

    public static async Task<Results<NoContent, BadRequest, NotFound>> UpdateProduct(
        [FromRoute] string productId,
        [FromBody] UpdateProductDto dto,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Products.FindAsync(productId, cancellationToken) is not { } product)
        {
            return TypedResults.NotFound();
        }

        if (dto is not { Name: { Length: > 0 } name, Description: { Length: > 0 } description })
        {
            return TypedResults.BadRequest();
        }

        ProductMapper mapper = new();
        mapper.UpdateProductDtoToProduct(dto, product);

        db.Products.Update(product);
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, NotFound>> DeleteProduct(
        [FromRoute] string productId,
        [FromServices] ConCurrencyDbContext db,
        CancellationToken cancellationToken = default)
    {
        if (await db.Products.FindAsync(productId, cancellationToken) is not { } product)
        {
            return TypedResults.NotFound();
        }

        db.Products.Remove(product);
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.NoContent();
    }
}
