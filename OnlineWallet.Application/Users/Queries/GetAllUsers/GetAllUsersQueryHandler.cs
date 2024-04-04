using MediatR;
using OnlineWallet.Application.Common.DTOs.Transaction;
using OnlineWallet.Application.Common.DTOs.User;
using OnlineWallet.Application.Common.DTOs.Wallet;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;

namespace OnlineWallet.Application.Users.Queries.GetAllUsers
{
    internal class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<IList<GetUserModel>>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQueryHandler(IGenericRepository<User> userRepository, IGenericRepository<Wallet> walletRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<IList<GetUserModel>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync();

            var usersList = new List<GetUserModel>();
            foreach (User user in users.Value)
            {
                var wallets = await _walletRepository.GetListAsync(x => x.UserId == user.Id, includeProperties: "Transactions");

                var walletList = new List<GetWalletModel>();
                foreach (Wallet wallet in wallets.Value)
                {
                    var walletTransactions = new List<GetTransactionModel>();
                    foreach (Transaction transaction in wallet.Transactions)
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
                        walletTransactions.Add(transactionModel);
                    }

                    var walletModel = new GetWalletModel
                    {
                        WalletName = wallet.WalletName,
                        WalletCode = wallet.WalletCode,
                        Currency = wallet.Currency,
                        Balance = wallet.Balance,
                        IsDefault = wallet.IsDefault,
                        Transactions = walletTransactions
                    };
                    walletList.Add(walletModel);
                }

                var userModel = new GetUserModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Wallets = walletList
                };
                usersList.Add(userModel);
            }

            return Result<IList<GetUserModel>>.Succeed(usersList);

        }
    }
}
