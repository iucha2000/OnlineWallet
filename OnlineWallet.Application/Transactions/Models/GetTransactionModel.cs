using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Transactions.Models
{
    public class GetTransactionModel
    {
        public Guid SenderUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public Guid SenderWalletId { get; set; }
        public Guid ReceiverWalletId { get; set; }
        public string Currency { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
