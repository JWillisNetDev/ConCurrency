using System.Text.Json.Serialization;

namespace ConCurrency.Data.Dtos.Exchange;

public class ErrorDto
{
    [JsonPropertyName("info")]
    public string Info { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public int Code { get; set; }
}
