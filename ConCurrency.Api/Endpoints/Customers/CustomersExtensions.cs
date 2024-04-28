namespace ConCurrency.Api.Endpoints.Customers;

public static class CustomersExtensions
{
    public static IEndpointRouteBuilder MapCustomersEndpoints(this IEndpointRouteBuilder router)
    {
        var grouped = router.MapGroup("customers");

        grouped.MapGet("/", CustomersHandlers.GetCustomers)
            .WithName(nameof(CustomersHandlers.GetCustomers));

        grouped.MapGet("/{customerId}", CustomersHandlers.GetCustomerById)
            .WithName(nameof(CustomersHandlers.GetCustomerById));

        grouped.MapGet("/{customerId}/orders", CustomersHandlers.GetOrdersForCustomer)
            .WithName(nameof(CustomersHandlers.GetOrdersForCustomer));

        grouped.MapPost("/", CustomersHandlers.CreateCustomer)
            .WithName(nameof(CustomersHandlers.CreateCustomer));

        grouped.MapPut("/{customerId}", CustomersHandlers.UpdateCustomer)
            .WithName(nameof(CustomersHandlers.UpdateCustomer));

        grouped.MapDelete("/{customerId}", CustomersHandlers.DeleteCustomer)
            .WithName(nameof(CustomersHandlers.DeleteCustomer));

        return router;
    }
}
