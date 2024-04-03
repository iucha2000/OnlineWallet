using MediatR;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Transactions.Commands.AddTransaction
{
    public record AddTransferFundsTransaction(Guid SenderUserId, Guid ReceiverUserId, string? SenderWalletCode, string? ReceiverWalletCode, string Currency, decimal Amount) : IRequest<Result>;

}
