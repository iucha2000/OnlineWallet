using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Common.Models.Transaction
{
    public class DepositFundsTransactionModel
    {
        public string? WalletCode { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
