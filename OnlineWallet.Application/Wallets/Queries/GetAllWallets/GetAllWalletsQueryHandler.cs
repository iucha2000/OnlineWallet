using MediatR;
using OnlineWallet.Application.Common.Models.Transaction;
using OnlineWallet.Application.Common.Models.Wallet;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Wallets.Queries.GetAllWallets
{
    internal class GetAllWalletsQueryHandler : IRequestHandler<GetAllWalletsQuery, Result<IList<GetWalletModel>>>
    {
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllWalletsQueryHandler(IGenericRepository<Wallet> walletRepository, IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IList<GetWalletModel>>> Handle(GetAllWalletsQuery request, CancellationToken cancellationToken)
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

            var wallets = await _walletRepository.GetListAsync(x=> x.UserId == user.Value.Id, includeProperties: "TransactionHistory");

            var walletsList = new List<GetWalletModel>();
            foreach (var wallet in wallets.Value)
            {
                var transactionsList = new List<GetTransactionModel>();
                foreach (Transaction transaction in wallet.TransactionHistory)
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

                var walletModel = new GetWalletModel
                {
                    WalletName = wallet.WalletName,
                    WalletCode = wallet.WalletCode,
                    Currency = wallet.Currency,
                    Balance = wallet.Balance,
                    IsDefault = wallet.IsDefault,
                    TransactionHistory = transactionsList
                };
                walletsList.Add(walletModel);
            }

            return Result<IList<GetWalletModel>>.Succeed(walletsList);

        }
    }
}
