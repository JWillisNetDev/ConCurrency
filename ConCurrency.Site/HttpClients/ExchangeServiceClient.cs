using ConCurrency.Data.Dtos.Exchange;

namespace ConCurrency.Site.HttpClients;

public class ExchangeServiceClient
{
    private readonly HttpClient _client;

    public ExchangeServiceClient(HttpClient httpClient)
    {
        _client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<Dictionary<string, string>> GetSymbolsAsync(CancellationToken cancellationToken = default)
    {
        var response = await _client.GetFromJsonAsync<SymbolsDto>("symbols", cancellationToken)
                       ?? throw new InvalidOperationException("Request could not be deserialized.");

        if (!response.Success)
        {
            throw new InvalidOperationException($"Request failed with error: {response.Error.Info}");
        }

        return response.Symbols.ToDictionary();
    }

    public async Task<Dictionary<string, double>> GetExchangeRatesAsync(string baseSymbol, ICollection<string> intoSymbols, CancellationToken cancellationToken = default)
    {
        var response = await _client.GetFromJsonAsync<RatesDto>($"convert?{string.Join('&', GetQueryParameters())}", cancellationToken)
                       ?? throw new InvalidOperationException("Request could not be deserialized.");

        if (!response.Success)
        {
            throw new InvalidOperationException($"Request failed with error: {response.Error.Info}");
        }

        return response.Rates.ToDictionary();

        IEnumerable<string> GetQueryParameters()
        {
            yield return $"baseSymbol={baseSymbol}";
            yield return $"intoSymbols={string.Join(',', intoSymbols)}";
        }
    }
}