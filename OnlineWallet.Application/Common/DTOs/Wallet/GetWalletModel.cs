using OnlineWallet.Application.Common.DTOs.Transaction;
using OnlineWallet.Domain.Enums;

namespace OnlineWallet.Application.Common.DTOs.Wallet
{
    public class GetWalletModel
    {
        public string WalletName { get; set; }
        public string WalletCode { get; set; }
        public CurrencyCode Currency { get; set; }
        public decimal Balance { get; set; }
        public bool IsDefault { get; set; }
        public IEnumerable<GetTransactionModel> Transactions { get; set; }
    }
}
