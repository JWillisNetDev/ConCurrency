var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("pg").AddDatabase("ConCurrencyDb");

var exchangeService = builder.AddProject<Projects.ConCurrency_ExchangeService>("concurrency-exchangeservice");

var conCurrencyApi = builder.AddProject<Projects.ConCurrency_Api>("concurrency-api")
    .WithReference(postgres);

var conCurrencyClient = builder.AddProject<Projects.ConCurrency_Site>("concurrency-site")
    .WithReference(exchangeService)
    .WithReference(conCurrencyApi);

builder.Build().Run();
