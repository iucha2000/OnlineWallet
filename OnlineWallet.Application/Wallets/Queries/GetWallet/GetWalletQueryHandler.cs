using MediatR;
using OnlineWallet.Application.Common.Models.Transaction;
using OnlineWallet.Application.Common.Models.Wallet;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Wallets.Queries.GetWallet
{
    internal class GetWalletQueryHandler : IRequestHandler<GetWalletQuery, Result<GetWalletModel>>
    {
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetWalletQueryHandler(IGenericRepository<Wallet> walletRepository, IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetWalletModel>> Handle(GetWalletQuery request, CancellationToken cancellationToken)
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

            var wallet = await _walletRepository.GetAsync(x => x.WalletCode == request.WalletCode && x.UserId == user.Value.Id, includeProperties: "Transactions");
            if(wallet.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.WalletNotFound);
            }

            var walletTransactions = new List<GetTransactionModel>();
            foreach (Transaction transaction in wallet.Value.Transactions)
            {
                var walletTransaction = new GetTransactionModel
                {
                    SenderUserId = transaction.SenderUserId,
                    ReceiverUserId = transaction.ReceiverUserId,
                    SenderWalletCode = transaction.SenderWalletCode,
                    ReceiverWalletCode = transaction.ReceiverWalletCode,
                    Currency = transaction.Currency,
                    Amount = transaction.Amount,
                    Date = transaction.Date,
                };
                walletTransactions.Add(walletTransaction);
            }

            var walletModel = new GetWalletModel
            {
                WalletCode = wallet.Value.WalletCode,
                WalletName = wallet.Value.WalletName,
                Currency = wallet.Value.Currency,
                Balance = wallet.Value.Balance,
                IsDefault = wallet.Value.IsDefault,
                Transaction = walletTransactions
            };

            return Result<GetWalletModel>.Succeed(walletModel);
        }
    }
}
