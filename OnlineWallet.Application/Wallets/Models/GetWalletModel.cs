using OnlineWallet.Application.Transactions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Wallets.Models
{
    public class GetWalletModel
    {
        public string WalletName { get; set; }
        public string WalletCode { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public bool IsDefault { get; set; }
        public GetTransactionModel[] TransactionHistory { get; set; }
    }
}
