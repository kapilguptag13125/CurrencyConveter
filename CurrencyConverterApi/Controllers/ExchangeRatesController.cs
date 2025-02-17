using CurrencyConverterApi.Infrastructures.ExternalServices.Interfaces;
using CurrencyConverterApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverterApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRatesService _exchangeRatesService;  
        private readonly IHistoricalExchangeRatesService _historicalExchangeRatesService;

        public ExchangeRatesController(IExchangeRatesService exchangeRatesService, IHistoricalExchangeRatesService historicalExchangeRatesService )
        {
            _exchangeRatesService = exchangeRatesService;
            _historicalExchangeRatesService = historicalExchangeRatesService;   
        }

        [HttpGet("GetExchangeRates")]
        public async Task<IActionResult> GetExchangeRates()
        {
            var exchangeRates = await _exchangeRatesService.GetExchangeRates();
            return Ok(exchangeRates);   
        }

        [HttpPost("GetHistoricalExchangeRates")]
        public async Task<IActionResult> GetHistoricalExchangeRates([FromBody] HistoricalExchangeRequest historicalExchangeRequest)
        {
            var exchangeRates = await _historicalExchangeRatesService.GetHistoricalExchangeRatesAsync(historicalExchangeRequest);
            return Ok(exchangeRates);
        }

        [HttpPost("GetHistoricalExchangeRatesWithPagination")]
        public async Task<IActionResult> GetHistoricalExchangeRatesWithPagination([FromBody] HistoricalExchangeRequestWithPagination historicalExchangeRequestWithPagination)
        {
            var exchangeRates = await _historicalExchangeRatesService.GetHistoricalExchangeRatesWithPaginationAsync(historicalExchangeRequestWithPagination);
            return Ok(exchangeRates);
        }
    }
}
