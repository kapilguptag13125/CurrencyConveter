using CurrencyConverterApi.Models;

namespace CurrencyConverterApi.Infrastructures.ExternalServices.Interfaces
{
    public interface IExchangeRatesService
    {
        Task<ExchangeRates> GetExchangeRates();
    }
}
