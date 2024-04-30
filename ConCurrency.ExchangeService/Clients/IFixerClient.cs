using ConCurrency.Data.Dtos.Exchange;

namespace ConCurrency.ExchangeService.Clients;

public interface IFixerClient
{
    public Task<SymbolsDto> GetSymbolsAsync(CancellationToken cancellationToken = default);
    public Task<RatesDto> GetExchangeRatesAsync(string baseSymbol, ICollection<string> intoSymbols, CancellationToken cancellationToken = default);
}
