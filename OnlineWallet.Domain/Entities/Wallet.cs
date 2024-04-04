using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Enums;

namespace OnlineWallet.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public string WalletName { get; set; }
        public string WalletCode { get; set; }
        public CurrencyCode Currency { get; set; }
        public decimal Balance { get; set; }
        public Guid UserId { get; set; }
        public bool IsDefault { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }

    }
}
