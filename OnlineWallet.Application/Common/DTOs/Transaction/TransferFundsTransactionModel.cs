using OnlineWallet.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OnlineWallet.Application.Common.DTOs.Transaction
{
    public class TransferFundsTransactionModel
    {
        [Required]
        public Guid ReceiverUserId { get; set; }

        public string? SenderWalletCode { get; set; }

        public string? ReceiverWalletCode { get; set; }

        [Required]
        public CurrencyCode Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
