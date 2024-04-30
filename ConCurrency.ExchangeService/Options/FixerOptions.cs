using System.ComponentModel.DataAnnotations;

namespace ConCurrency.ExchangeService.Options;

public class FixerOptions
{
    [Required]
    public required Uri BaseAddress { get; set; }

    [Required]
    public required string AccessKey { get; set; }
}
