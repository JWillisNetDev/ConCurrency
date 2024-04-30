using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ConCurrency.Data.Dtos.Exchange;

public class SymbolsDto
{
    [JsonPropertyName("success")]
    [MemberNotNullWhen(true, nameof(Symbols))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool Success { get; set; }

    [JsonPropertyName("error")]
    public ErrorDto? Error { get; set; }

    [JsonPropertyName("symbols")]
    public IDictionary<string, string>? Symbols { get; set; }
}
