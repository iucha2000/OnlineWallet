using MediatR;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Enums;

namespace OnlineWallet.Application.Transactions.Commands.AddTransaction
{
    public record AddTransferFundsTransaction(Guid SenderUserId, Guid ReceiverUserId, string? SenderWalletCode, string? ReceiverWalletCode, CurrencyCode Currency, decimal Amount) : IRequest<Result<string>>;

}
