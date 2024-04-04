using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Services
{
    public interface IBalanceManager
    {
        decimal GetBalance(Wallet wallet, CurrencyCode currency);

        Task AddFunds(Wallet wallet, CurrencyCode currency,decimal amount);

        Task SubtractFunds(Wallet wallet, CurrencyCode currency, decimal amount);
    }
}
