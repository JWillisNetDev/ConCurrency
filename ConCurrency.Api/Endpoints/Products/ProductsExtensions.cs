namespace ConCurrency.Api.Endpoints.Products;

public static class ProductsExtensions
{
    public static IEndpointRouteBuilder MapProductsEndpoints(this IEndpointRouteBuilder router)
    {
        var grouped = router.MapGroup("products");

        grouped.MapGet("/", ProductsHandlers.GetProducts)
            .WithName(nameof(ProductsHandlers.GetProducts));

        grouped.MapGet("/{productId}", ProductsHandlers.GetProductById)
            .WithName(nameof(ProductsHandlers.GetProductById));

        grouped.MapGet("/{productId}/orders", ProductsHandlers.GetOrdersForProduct)
            .WithName(nameof(ProductsHandlers.GetOrdersForProduct));

        grouped.MapPost("/", ProductsHandlers.CreateProduct)
            .WithName(nameof(ProductsHandlers.CreateProduct));

        grouped.MapPut("/{productId}", ProductsHandlers.UpdateProduct)
            .WithName(nameof(ProductsHandlers.UpdateProduct));

        grouped.MapDelete("/{productId}", ProductsHandlers.DeleteProduct)
            .WithName(nameof(ProductsHandlers.DeleteProduct));

        return router;
    }
}
