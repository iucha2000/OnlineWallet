using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Transaction.Models
{
    public class TransactionModel
    {
        public Guid ReceiverUserId { get; set; }
        public Guid ReceiverWalletId { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
