using MediatR;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Transactions.Commands.AddTransaction
{
    public record AddWithdrawFundsTransaction(Guid UserId, string? WalletCode, string Currency, decimal Amount) : IRequest<Result<string>>;

}
