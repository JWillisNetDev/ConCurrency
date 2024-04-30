var builder = DistributedApplication.CreateBuilder(args);

var postgresPassword = builder.AddParameter("postgresPassword", secret: true); // Don't forget to set your secret!

var postgres = builder
    .AddPostgres("pg", password: postgresPassword)
    .WithDataVolume()
    .WithPgAdmin()
    .AddDatabase("ConCurrencyDb");

var redis = builder.AddRedis("cache");

var exchangeService = builder.AddProject<Projects.ConCurrency_ExchangeService>("concurrency-exchangeservice")
    .WithReference(redis);

var conCurrencyApi = builder.AddProject<Projects.ConCurrency_Api>("concurrency-api")
    .WithReference(postgres);

var conCurrencyClient = builder.AddProject<Projects.ConCurrency_Site>("concurrency-site")
    .WithReference(exchangeService)
    .WithReference(conCurrencyApi);

builder.Build().Run();
