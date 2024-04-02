using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public string WalletName { get; set; }
        public string WalletCode { get; set; }
        public string Currency { get; set; }
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
        public bool IsDefault { get; set; }
        public IEnumerable<Transaction> TransactionHistory { get; set; }

    }
}
