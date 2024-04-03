using MediatR;
using OnlineWallet.Application.Common.Models.Transaction;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Transactions.Queries.GetAllTransactions
{
    public record GetAllTransactionsQuery(Guid UserId) : IRequest<Result<IList<GetTransactionModel>>>;
}
