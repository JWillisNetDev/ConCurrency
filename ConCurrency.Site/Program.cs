using ConCurrency.Site.Components;
using ConCurrency.Site.HttpClients;

using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddMudServices();

builder.Services.AddHttpClient<ConCurrencyServiceClient>(httpClient =>
{
    httpClient.BaseAddress = builder.Configuration.GetSection("ConCurrencyApi").GetValue<Uri>("BaseAddress")
                             ?? throw new InvalidOperationException("No base address found for ConCurrency API");
});

builder.Services.AddHttpClient<ExchangeServiceClient>(httpClient =>
{
    httpClient.BaseAddress = builder.Configuration.GetSection("ExchangeServiceApi").GetValue<Uri>("BaseAddress")
                             ?? throw new InvalidOperationException("No base address found for Exchange Service API");
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
