using MediatR;
using OnlineWallet.Application.Common.DTOs.Transaction;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Transactions.Queries.GetAllTransactions
{
    internal class GetAllTransactionsQueryHandler : IRequestHandler<GetAllTransactionsQuery, Result<IList<GetTransactionModel>>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTransactionsQueryHandler(IGenericRepository<User> userRepository, IGenericRepository<Transaction> transactionRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IList<GetTransactionModel>>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId == Guid.Empty)
            {
                throw new EntityNotFoundException(ErrorMessages.AuthenticatedUserNotFound);
            }

            var user = await _userRepository.GetAsync(x => x.Id == request.UserId);
            if (user.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserNotFound);
            }

            var transactions = await _transactionRepository.GetListAsync(x=> x.ReceiverUserId == request.UserId || x.SenderUserId == request.UserId);

            var transactionsList = new List<GetTransactionModel>();
            foreach (Transaction transaction in transactions.Value)
            {
                var transactionModel = new GetTransactionModel
                {
                    SenderUserId = transaction.SenderUserId,
                    ReceiverUserId = transaction.ReceiverUserId,
                    SenderWalletCode = transaction.SenderWalletCode,
                    ReceiverWalletCode = transaction.ReceiverWalletCode,
                    Currency = transaction.Currency,
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                };
                transactionsList.Add(transactionModel);
            }

            return Result<IList<GetTransactionModel>>.Succeed(transactionsList);
        }
    }
}
