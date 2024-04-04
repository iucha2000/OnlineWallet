using OnlineWallet.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Application.Common.DTOs.Wallet
{
    public class UpdateWalletModel
    {
        [Required]
        public string WalletCode { get; set; }

        public string? WalletName { get; set; }

        public CurrencyCode? Currency { get; set; }

        public bool? IsDefault { get; set; }
    }
}
