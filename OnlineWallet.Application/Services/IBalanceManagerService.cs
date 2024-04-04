using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Enums;

namespace OnlineWallet.Application.Services
{
    public interface IBalanceManagerService
    {
        Task AddFunds(Wallet wallet, CurrencyCode currency, decimal amount);

        Task SubtractFunds(Wallet wallet, CurrencyCode currency, decimal amount);
    }
}
