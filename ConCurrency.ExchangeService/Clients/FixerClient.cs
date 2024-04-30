using ConCurrency.Data.Dtos.Exchange;
using ConCurrency.ExchangeService.Options;

using Microsoft.Extensions.Options;

namespace ConCurrency.ExchangeService.Clients;

public class FixerClient : IFixerClient
{
    private readonly HttpClient _Client;
    private readonly IOptions<FixerOptions> _Options;

    public FixerClient(HttpClient client, IOptions<FixerOptions> options)
    {
        _Client = client ?? throw new ArgumentNullException(nameof(client));
        _Options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<SymbolsDto> GetSymbolsAsync(CancellationToken cancellationToken = default)
    {
        var symbolsDto = await _Client.GetFromJsonAsync<SymbolsDto>($"symbols?access_key={_Options.Value.AccessKey}", cancellationToken)
                         ?? throw new InvalidOperationException("Request could not be deserialized.");
        return symbolsDto;
    }

    public async Task<RatesDto> GetExchangeRatesAsync(string baseSymbol, ICollection<string> intoSymbols, CancellationToken cancellationToken = default)
    {
        var ratesDto = await _Client.GetFromJsonAsync<RatesDto>(new Uri($"latest?{string.Join('&', GetQueryParameters())}", UriKind.Relative), cancellationToken)
                       ?? throw new InvalidOperationException("Request could not be deserialized.");
        return ratesDto;

        IEnumerable<string> GetQueryParameters()
        {
            yield return $"access_key={_Options.Value.AccessKey}";
            yield return $"base={baseSymbol}";
            yield return $"symbols={string.Join(',', intoSymbols)}";
        }
    }
}
