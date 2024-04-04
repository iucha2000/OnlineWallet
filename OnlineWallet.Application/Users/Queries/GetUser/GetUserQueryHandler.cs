using MediatR;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;
using OnlineWallet.Application.Common.DTOs.User;
using OnlineWallet.Application.Common.DTOs.Wallet;
using OnlineWallet.Application.Common.DTOs.Transaction;

namespace OnlineWallet.Application.Users.Queries.GetUser
{
    internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<GetUserModel>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserQueryHandler(IGenericRepository<User> userRepository, IGenericRepository<Wallet> walletRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetUserModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x => x.Id == request.userId);

            if (user.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserNotFound);
            }

            var userWallets = await _walletRepository.GetListAsync(x=> x.UserId == user.Value.Id, includeProperties: "Transactions");

            var walletsList = new List<GetWalletModel>();
            foreach (Wallet wallet in userWallets.Value)
            {
                var transactionsList = new List<GetTransactionModel>();
                foreach(Transaction transaction in wallet.Transactions)
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
                    transactionsList.Add(walletTransaction);
                }

                var userWallet = new GetWalletModel()
                {
                    WalletName = wallet.WalletName,
                    WalletCode = wallet.WalletCode,
                    Currency = wallet.Currency,
                    Balance = wallet.Balance,
                    IsDefault = wallet.IsDefault,
                    Transactions = transactionsList
                };
                walletsList.Add(userWallet);
            }

            var userModel = new GetUserModel
            {
                Id = user.Value.Id,
                FirstName = user.Value.FirstName,
                LastName = user.Value.LastName,
                Email = user.Value.Email,
                Wallets = walletsList
            };

            return Result<GetUserModel>.Succeed(userModel);
        }
    }
}
