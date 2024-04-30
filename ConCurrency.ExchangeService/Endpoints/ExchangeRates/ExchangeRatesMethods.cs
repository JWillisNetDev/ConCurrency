using ConCurrency.Data.Dtos.Exchange;
using ConCurrency.ExchangeService.Clients;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ConCurrency.ExchangeService.Endpoints.ExchangeRates;

public class ExchangeRatesMethods
{
    public static async Task<Ok<SymbolsDto>> GetSymbols([FromServices] IFixerClient fixerClient)
    {
        var response = await fixerClient.GetSymbolsAsync();

        return TypedResults.Ok(response);
    }

    public static async Task<Ok<RatesDto>> GetExchangeRates(
        [FromQuery] string baseSymbol,
        [FromQuery] string[] intoSymbols,
        [FromServices] IFixerClient fixerClient)
    {
        var response = await fixerClient.GetExchangeRatesAsync(baseSymbol, intoSymbols);

        return TypedResults.Ok(response);
    }
}
