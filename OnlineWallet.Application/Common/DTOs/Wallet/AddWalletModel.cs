using OnlineWallet.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Application.Common.DTOs.Wallet
{
    public class AddWalletModel
    {
        [Required]
        public string WalletName { get; set; }

        [Required]
        public CurrencyCode Currency { get; set; }

        [Required]
        public bool IsDefault { get; set; }
    }
}
