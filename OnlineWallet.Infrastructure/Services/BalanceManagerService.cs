using OnlineWallet.Application.Services;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Enums;
using OnlineWallet.Domain.Exceptions;


namespace OnlineWallet.Infrastructure.Services
{
    public class BalanceManagerService : IBalanceManagerService
    {
        private readonly IExchangeRateService _exchangeRateService;

        public BalanceManagerService(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        public async Task AddFunds(Wallet wallet, CurrencyCode currency, decimal amount)
        {
            decimal rate = await _exchangeRateService.GetExchangeRate(wallet.Currency, currency);
            var convertedAmount = Math.Round(amount / rate, 2);

            wallet.Balance += convertedAmount;
        }

        public async Task SubtractFunds(Wallet wallet, CurrencyCode currency, decimal amount)
        {
            decimal rate = await _exchangeRateService.GetExchangeRate(wallet.Currency, currency);
            var convertedBalance = Math.Round(wallet.Balance * rate, 2);

            if (convertedBalance < amount)
            {
                throw new NoEnoughBalanceException(ErrorMessages.NoEnoughBalance);
            }

            var convertedAmount = Math.Round(amount / rate, 2);

            wallet.Balance -= convertedAmount;
        }
    }
}
