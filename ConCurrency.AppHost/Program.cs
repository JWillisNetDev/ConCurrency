var builder = DistributedApplication.CreateBuilder(args);

var exchangeService = builder.AddProject<Projects.ConCurrency_ExchangeService>("concurrency-exchangeservice");

var conCurrencyApi = builder.AddProject<Projects.ConCurrency_Api>("concurrency-api");

var conCurrencyClient = builder.AddProject<Projects.ConCurrency_Site>("concurrency-site")
    .WithReference(exchangeService)
    .WithReference(conCurrencyApi);

builder.Build().Run();
