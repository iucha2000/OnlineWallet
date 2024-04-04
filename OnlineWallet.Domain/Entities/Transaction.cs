using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Enums;

namespace OnlineWallet.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public Guid SenderUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public string SenderWalletCode { get; set; }
        public string ReceiverWalletCode { get; set; }
        public CurrencyCode Currency { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public IEnumerable<Wallet> Wallets { get; set; }
    }
}
