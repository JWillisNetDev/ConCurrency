using ConCurrency.ExchangeService.Clients;
using ConCurrency.ExchangeService.Endpoints.ExchangeRates;
using ConCurrency.ExchangeService.Options;

using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<FixerOptions>(builder.Configuration.GetSection("FixerOptions"));

builder.Services.AddHttpClient<IFixerClient, FixerClient>((isp, http) =>
{
    var options = isp.GetRequiredService<IOptions<FixerOptions>>();
    Console.WriteLine($"Using base address: {options.Value.BaseAddress.AbsoluteUri} and access key {options.Value.AccessKey}");

    http.BaseAddress = options.Value.BaseAddress;
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/symbols", ExchangeRatesMethods.GetSymbols);
app.MapGet("/convert", ExchangeRatesMethods.GetExchangeRates);

app.UseHttpsRedirection();

app.Run();
