namespace CurrencyConverterApi.Models
{
    public class HistoricalExchangeRequestWithPagination :HistoricalExchangeRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; }   
    }
}
