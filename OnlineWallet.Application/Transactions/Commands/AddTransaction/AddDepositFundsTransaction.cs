using MediatR;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Enums;

namespace OnlineWallet.Application.Transactions.Commands.AddTransaction
{
    public record AddDepositFundsTransaction(Guid UserId, string? WalletCode, CurrencyCode Currency, decimal Amount) : IRequest<Result<string>>;
}
