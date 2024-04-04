using Flurl.Http;
using Microsoft.Extensions.Options;
using OnlineWallet.Application.Extensions;
using OnlineWallet.Application.Services;
using OnlineWallet.Application.Services.Models;
using OnlineWallet.Domain.Enums;
using System.ComponentModel;

namespace OnlineWallet.Infrastructure.Services
{
    internal class ExchangeRateService : IExchangeRateService
    {
        private readonly ExchangeRatesConfigModel _configModel;
        private readonly ICacheService _cacheService;

        public ExchangeRateService(IOptions<ExchangeRatesConfigModel> config, ICacheService cacheService)
        {
            _configModel = config.Value;
            _cacheService = cacheService;
        }

        public async Task<ExchangeRatesModel> GetExchangeRates(CurrencyCode currency)
        {
            var data = _cacheService.GetData<ExchangeRatesModel>(currency.ToString());

            if(data == null)
            {
                var url = _configModel.BaseUrl + _configModel.ApiKey + _configModel.Param + currency.ToString();
                data = await url.GetJsonAsync<ExchangeRatesModel>();

                _cacheService.SetData(currency.ToString(), data);
            }
           
            return data;
        }

        public async Task<decimal> GetExchangeRate(CurrencyCode toCurrency, CurrencyCode fromCurrency)
        {
            var rates = await GetExchangeRates(toCurrency);

            //Using reflection, from service response, take the exchange rate of currency, that matches our fromCurrency code
            var exchangeRate = PropertyValueGetter.GetValue<double>(rates.conversion_rates, fromCurrency.ToString());

            return (decimal)exchangeRate;
        }
    }
}
