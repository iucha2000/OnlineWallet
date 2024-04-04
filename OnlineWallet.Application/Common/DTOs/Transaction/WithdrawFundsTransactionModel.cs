using OnlineWallet.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Application.Common.DTOs.Transaction
{
    public class WithdrawFundsTransactionModel
    {
        public string? WalletCode { get; set; }

        [Required]
        public CurrencyCode Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
