using CurrencyConverterApi.Infrastructures.ExternalServices.Interfaces;
using CurrencyConverterApi.Models;
using System.Text.Json;

namespace CurrencyConverterApi.Infrastructures.ExternalServices
{
    public class ExchangeRatesService: IExchangeRatesService
    {
        private readonly HttpClient _httpClient;

        public ExchangeRatesService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ExchangeRatesClient");
        }

        public async Task<ExchangeRates> GetExchangeRates()
        {
            var response = await _httpClient.GetAsync("latest");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var exchangeRates = JsonSerializer.Deserialize<ExchangeRates>(json);
            return exchangeRates;
        }
    }
}
