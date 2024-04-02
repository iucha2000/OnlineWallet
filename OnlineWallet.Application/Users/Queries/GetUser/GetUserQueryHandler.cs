using MediatR;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;
using OnlineWallet.Application.Common.Models.User;
using OnlineWallet.Application.Common.Models.Wallet;

namespace OnlineWallet.Application.Users.Queries.GetUser
{
    internal class GetUserQueryHandler : IRequestHandler<GetUserQuery, Result<GetUserModel>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GetUserQueryHandler(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<GetUserModel>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(x => x.Id == request.userId, includeProperties: "Wallets");

            if (user.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserNotFound);
            }

            var userWallets = new List<GetWalletModel>();
            foreach(var wallet in user.Value.Wallets)
            {
                var userWallet = new GetWalletModel()
                {
                    WalletName = wallet.WalletName,
                    WalletCode = wallet.WalletCode,
                    Currency = wallet.Currency,
                    Balance = wallet.Balance,
                    IsDefault = wallet.IsDefault,
                };
                userWallets.Add(userWallet);
            }

            //TODO optimize
            var userModel = new GetUserModel
            {
                FirstName = user.Value.FirstName,
                LastName = user.Value.LastName,
                Email = user.Value.Email,
                Wallets = userWallets
            };

            return Result<GetUserModel>.Succeed(userModel);
        }
    }
}
