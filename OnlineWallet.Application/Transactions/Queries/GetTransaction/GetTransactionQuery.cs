using MediatR;
using OnlineWallet.Application.Common.Models.Transaction;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Transactions.Queries.GetTransaction
{
    public record GetTransactionQuery(Guid UserId, Guid TransactionId) : IRequest<Result<GetTransactionModel>>;
}
