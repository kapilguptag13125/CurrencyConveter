namespace CurrencyConverterApi.Models
{
    public class HistoricalExchangeRequest
    {
        public required string StartDate { get; set; }
        public required string EndDate { get; set; }
        public required string BaseCurrency { get; set; } = "EUR";
    }
}
