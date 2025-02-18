using CurrencyConverterApi.Models;

namespace CurrencyConverterApi.Infrastructures.ExternalServices.Interfaces
{
    public interface IHistoricalExchangeRatesService
    {
        Task<HistoricalExchangeRates> GetHistoricalExchangeRatesAsync(HistoricalExchangeRequest historicalExchangeRequest);
        Task<HistoricalExchangeRates> GetHistoricalExchangeRatesWithPaginationAsync(HistoricalExchangeRequestWithPagination historicalExchangeRequestWithPagination);
    }
}
