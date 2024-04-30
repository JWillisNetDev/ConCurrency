using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ConCurrency.Data.Dtos.Exchange;

public class RatesDto
{
    [JsonPropertyName("success")]
    [MemberNotNullWhen(true, nameof(Rates), nameof(Base))]
    [MemberNotNullWhen(false, nameof(Error))]
    public bool Success { get; set; }

    [JsonPropertyName("error")]
    public ErrorDto? Error { get; set; }

    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    [JsonPropertyName("base")]
    public string? Base { get; set; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("rates")]
    public IDictionary<string, double>? Rates { get; set; }
}
