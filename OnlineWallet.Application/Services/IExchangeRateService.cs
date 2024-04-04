using OnlineWallet.Application.Services.Models;
using OnlineWallet.Domain.Enums;

namespace OnlineWallet.Application.Services
{
    public interface IExchangeRateService
    {
        Task<ExchangeRatesModel> GetExchangeRates(CurrencyCode currency);

        Task<decimal> GetExchangeRate(CurrencyCode fromCurrency, CurrencyCode toCurrency);
    }
}
