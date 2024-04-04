using OnlineWallet.Domain.Enums;

namespace OnlineWallet.Application.Common.DTOs.Transaction
{
    public class GetTransactionModel
    {
        public Guid SenderUserId { get; set; }
        public Guid ReceiverUserId { get; set; }
        public string SenderWalletCode { get; set; }
        public string ReceiverWalletCode { get; set; }
        public CurrencyCode Currency { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
