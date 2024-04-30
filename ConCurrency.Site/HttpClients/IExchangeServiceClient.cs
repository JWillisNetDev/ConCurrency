
namespace ConCurrency.Site.HttpClients;

public interface IExchangeServiceClient
{
    Task<Dictionary<string, double>> GetExchangeRatesAsync(string baseSymbol, ICollection<string> intoSymbols, CancellationToken cancellationToken = default);
    Task<Dictionary<string, string>> GetSymbolsAsync(CancellationToken cancellationToken = default);
}