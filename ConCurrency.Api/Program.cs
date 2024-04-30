using ConCurrency.Api.Endpoints.Customers;
using ConCurrency.Api.Endpoints.Orders;
using ConCurrency.Api.Endpoints.Products;
using ConCurrency.Data;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddNpgsqlDbContext<ConCurrencyDbContext>("ConCurrencyDb",
    configureDbContextOptions: options => options.UseNpgsql(o => o.MigrationsAssembly("ConCurrency.Data")));

var app = builder.Build();

app.MapDefaultEndpoints();

// Migrate the database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ConCurrencyDbContext>();
    await db.Database.MigrateAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapProductsEndpoints()
    .MapCustomersEndpoints()
    .MapOrdersEndpoints();

app.UseHttpsRedirection();

app.Run();
