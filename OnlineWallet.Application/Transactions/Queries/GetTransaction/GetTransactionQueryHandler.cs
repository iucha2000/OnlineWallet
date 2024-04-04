using MediatR;
using OnlineWallet.Application.Common.DTOs.Transaction;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Transactions.Queries.GetTransaction
{
    internal class GetTransactionQueryHandler : IRequestHandler<GetTransactionQuery, Result<GetTransactionModel>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetTransactionQueryHandler(IGenericRepository<User> userRepository, IGenericRepository<Transaction> transactionRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetTransactionModel>> Handle(GetTransactionQuery request, CancellationToken cancellationToken)
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

            var transaction = await _transactionRepository.GetAsync(x=> x.Id == request.TransactionId && (x.ReceiverUserId == request.UserId || x.SenderUserId == request.UserId));
            if(transaction.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.TransactionNotFound);
            }

            var transactionModel = new GetTransactionModel
            {
                SenderUserId = transaction.Value.SenderUserId,
                ReceiverUserId = transaction.Value.ReceiverUserId,
                SenderWalletCode = transaction.Value.SenderWalletCode,
                ReceiverWalletCode = transaction.Value.ReceiverWalletCode,
                Currency = transaction.Value.Currency,
                Amount = transaction.Value.Amount,
                Date = transaction.Value.Date,
            };

            return Result<GetTransactionModel>.Succeed(transactionModel);
        }
    }
}
