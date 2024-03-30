using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public string WalletName { get; set; }
        public string WalletCode { get; set; }
        public decimal Balance { get; set; }
        public User User { get; set; }
        public Transaction[] TransactionHistory { get; set; }

    }
}
