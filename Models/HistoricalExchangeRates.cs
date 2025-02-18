using System.Text.Json.Serialization;

namespace CurrencyConverterApi.Models
{
    public class HistoricalExchangeRates
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("base")]
        public required string Base { get; set; }

        [JsonPropertyName("start_date")]
        public required string StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public required string EndDate { get; set; }

        [JsonPropertyName("rates")]
        public Dictionary<string, Dictionary<string, decimal>> Rates { get; set; }
    }
}
