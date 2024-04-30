using System.Text;
using System.Text.Json;

using ConCurrency.Data.Dtos.Exchange;
using ConCurrency.ExchangeService.Clients;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace ConCurrency.ExchangeService.Endpoints.ExchangeRates;

public class ExchangeRatesMethods
{
    public static async Task<Ok<SymbolsDto>> GetSymbols(
        [FromServices] IFixerClient fixerClient,
        [FromServices] IDistributedCache redis,
        [FromServices] ILogger<ExchangeRatesMethods> logger)
    {
        logger.LogInformation("Getting symbols from Fixer API {at}", DateTimeOffset.UtcNow);

        const string key = "symbols";

        if (await redis.GetAsync(key) is { } symbolsBytes)
        {
            logger.LogInformation("Accessing cached symbols from Redis {at}", DateTimeOffset.UtcNow);
            var symbols = JsonSerializer.Deserialize<SymbolsDto>(Encoding.UTF8.GetString(symbolsBytes));
            return TypedResults.Ok(symbols);
        }

        var response = await fixerClient.GetSymbolsAsync();

        await redis.SetStringAsync(key, JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
        });

        return TypedResults.Ok(response);
    }

    public static async Task<Ok<RatesDto>> GetExchangeRates(
        [FromQuery] string baseSymbol,
        [FromQuery] string[] intoSymbols,
        [FromServices] IDistributedCache redis,
        [FromServices] IFixerClient fixerClient)
    {
        var key = $"{baseSymbol}-{string.Join('-', intoSymbols)}";

        if (await redis.GetAsync(key) is { } ratesBytes)
        {
            var rates = JsonSerializer.Deserialize<RatesDto>(Encoding.UTF8.GetString(ratesBytes));
            return TypedResults.Ok(rates);
        }

        var response = await fixerClient.GetExchangeRatesAsync(baseSymbol, intoSymbols);

        await redis.SetStringAsync(key, JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
        });

        return TypedResults.Ok(response);
    }
}
