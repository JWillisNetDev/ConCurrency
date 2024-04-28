namespace ConCurrency.Api.Endpoints.Orders;

public static class OrdersExtensions
{
    public static IEndpointRouteBuilder MapOrdersEndpoints(this IEndpointRouteBuilder router)
    {
        var grouped = router.MapGroup("orders");

        grouped.MapGet("/", OrdersHandlers.GetOrders)
            .WithName(nameof(OrdersHandlers.GetOrders));

        grouped.MapGet("/{orderId}", OrdersHandlers.GetOrderById)
            .WithName(nameof(OrdersHandlers.GetOrderById));

        grouped.MapPost("/", OrdersHandlers.CreateOrder)
            .WithName(nameof(OrdersHandlers.CreateOrder));

        grouped.MapPut("/{orderId}", OrdersHandlers.UpdateOrder)
            .WithName(nameof(OrdersHandlers.UpdateOrder));

        grouped.MapDelete("/{orderId}", OrdersHandlers.DeleteOrder)
            .WithName(nameof(OrdersHandlers.DeleteOrder));

        return router;
    }
}
