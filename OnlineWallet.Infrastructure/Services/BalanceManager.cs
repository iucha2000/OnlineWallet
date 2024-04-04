using OnlineWallet.Application.Services;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Enums;
using OnlineWallet.Domain.Exceptions;


namespace OnlineWallet.Infrastructure.Services
{
    public class BalanceManager : IBalanceManager
    {
        public BalanceManager()
        {

        }

        public decimal GetBalance(Wallet wallet, CurrencyCode currency)
        {
            //Use currency conversion here
            return wallet.Balance;
        }

        public async Task AddFunds(Wallet wallet, CurrencyCode currency, decimal amount)
        {
            //Use currency conversion here
            wallet.Balance += amount;
        }

        public async Task SubtractFunds(Wallet wallet, CurrencyCode currency, decimal amount)
        {
            //Use currency conversion here
            if (GetBalance(wallet, currency) < amount)
            {
                throw new NoEnoughBalanceException(ErrorMessages.NoEnoughBalance);
            }

            wallet.Balance -= amount;
        }
    }
}
