using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Common.Models.Wallet
{
    public class UpdateWalletModel
    {
        [Required]
        public string WalletCode { get; set; }

        public string? WalletName { get; set; }

        public string? Currency { get; set; }

        public bool? IsDefault { get; set; }
    }
}
