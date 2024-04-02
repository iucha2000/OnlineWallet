using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Common.Models.Wallet
{
    public class AddWalletModel
    {
        [Required]
        public string WalletName { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public bool IsDefault { get; set; }
    }
}
