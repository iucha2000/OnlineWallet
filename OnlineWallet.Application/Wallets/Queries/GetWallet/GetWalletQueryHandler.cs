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

            var user = await _userRepository.GetAsync(x => x.Id == request.UserId, includeProperties: "Wallets");
            if (user.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserNotFound);
            }

            var userWallet = user.Value.Wallets.FirstOrDefault(x => x.WalletCode == request.WalletCode);
            if (userWallet == null)
            {
                throw new EntityNotFoundException(ErrorMessages.WalletNotFound);
            }

            //TODO optimize
            var wallet = await _walletRepository.GetAsync(x => x.WalletCode == userWallet.WalletCode, includeProperties: "TransactionHistory");
            if(wallet == null)
            {
                throw new EntityNotFoundException(ErrorMessages.WalletNotFound);
            }

            var walletTransactions = new List<GetTransactionModel>();
            foreach (Transaction t in wallet.Value.TransactionHistory)
            {
                var transaction = new GetTransactionModel
                {
                    SenderUserId = t.SenderUserId,
                    ReceiverUserId = t.ReceiverUserId,
                    SenderWalletId = t.SenderWalletId,
                    ReceiverWalletId = t.ReceiverWalletId,
                    Currency = t.Currency,
                    Amount = t.Amount,
                    Date = t.Date,
                };
                walletTransactions.Add(transaction);
            }

            var walletModel = new GetWalletModel
            {
                WalletCode = wallet.Value.WalletCode,
                WalletName = wallet.Value.WalletName,
                Currency = wallet.Value.Currency,
                Balance = wallet.Value.Balance,
                IsDefault = wallet.Value.IsDefault,
                TransactionHistory = walletTransactions
            };

            return Result<GetWalletModel>.Succeed(walletModel);
        }
    }
}
