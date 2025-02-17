using CurrencyConverterApi.Infrastructures.ExternalServices.Interfaces;
using CurrencyConverterApi.Models;
using System.Text.Json;

namespace CurrencyConverterApi.Infrastructures.ExternalServices
{
    public class HistoricalExchangeRatesService : IHistoricalExchangeRatesService
    {
        private readonly HttpClient _httpClient;

        public HistoricalExchangeRatesService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("FrankfurterClient");
        }

        public async Task<HistoricalExchangeRates> GetHistoricalExchangeRatesAsync(HistoricalExchangeRequest historicalExchangeRequest)
        {
             return await GetHistoricRates(historicalExchangeRequest.StartDate, historicalExchangeRequest.EndDate, historicalExchangeRequest.BaseCurrency);
        }

        public async Task<HistoricalExchangeRates> GetHistoricalExchangeRatesWithPaginationAsync(HistoricalExchangeRequestWithPagination historicalExchangeRequestWithPagination)
        {
            var historicalRates = await GetHistoricRates(historicalExchangeRequestWithPagination.StartDate, historicalExchangeRequestWithPagination.EndDate, historicalExchangeRequestWithPagination.BaseCurrency);
            var paginatedRates = PaginateRates(historicalRates.Rates, historicalExchangeRequestWithPagination.Page, historicalExchangeRequestWithPagination.PageSize);
            historicalRates.Rates = paginatedRates;

            return historicalRates;
        }

        private async Task<HistoricalExchangeRates> GetHistoricRates(string startDate, string endDate, string baseCurrency)
        {
            // Build the API URL
            var url = $"{startDate}..{endDate}?from={baseCurrency}";

            // Make the API call
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            // Deserialize the response
            var json = await response.Content.ReadAsStringAsync();
            var historicalRates = JsonSerializer.Deserialize<HistoricalExchangeRates>(json);
            return historicalRates;
        }


        private Dictionary<string, Dictionary<string, decimal>> PaginateRates(Dictionary<string, Dictionary<string, decimal>> rates, int page, int pageSize)
        {
            var paginatedRates = new Dictionary<string, Dictionary<string, decimal>>();
            var skip = (page - 1) * pageSize;
            var take = pageSize;

            foreach (var rate in rates.Skip(skip).Take(take))
            {
                paginatedRates.Add(rate.Key, rate.Value);
            }

            return paginatedRates;
        }
    }
}
