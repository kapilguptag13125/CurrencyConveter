using CurrencyConverterApi.Configuration;
using CurrencyConverterApi.Infrastructures.ExternalServices;
using CurrencyConverterApi.Infrastructures.ExternalServices.Interfaces;
using Microsoft.Extensions.Options;

namespace CurrencyConverterApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

           

            builder.Services.Configure<ExchangeRatesApiConfig>(builder.Configuration.GetSection("ExchangeRatesApiConfig"));
            builder.Services.Configure<FrankfurterApiConfig>(builder.Configuration.GetSection("FrankfurterApiConfig"));

            builder.Services.AddHttpClient("ExchangeRatesClient", (serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<ExchangeRatesApiConfig>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });


            builder.Services.AddHttpClient("FrankfurterClient", (serviceProvider, client) =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<FrankfurterApiConfig>>().Value;
                client.BaseAddress = new Uri(options.BaseUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });


            builder.Services.AddTransient<IExchangeRatesService, ExchangeRatesService>();
            builder.Services.AddTransient<IHistoricalExchangeRatesService, HistoricalExchangeRatesService>();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
