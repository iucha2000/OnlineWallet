using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineWallet.Application.Common.Models.Transaction;

namespace OnlineWallet.Application.Common.Models.Wallet
{
    public class GetWalletModel
    {
        public string WalletName { get; set; }
        public string WalletCode { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public bool IsDefault { get; set; }
        public IEnumerable<GetTransactionModel> TransactionHistory { get; set; }
    }
}
