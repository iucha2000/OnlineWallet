using MediatR;
using OnlineWallet.Application.Extensions;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Wallets.Commands.AddWallet
{
    internal class AddWalletCommandHandler : IRequestHandler<AddWalletCommand, Result>
    {
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddWalletCommandHandler(IGenericRepository<Wallet> walletRepository, IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddWalletCommand request, CancellationToken cancellationToken)
        {
            if(request.UserId == Guid.Empty)
            {
                throw new EntityNotFoundException(ErrorMessages.AuthenticatedUserNotFound);
            }

            var user = await _userRepository.GetAsync(x=> x.Id == request.UserId, includeProperties: "Wallets");
            if(user.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserNotFound);
            }

            if(request.IsDefault == true)
            {
                foreach(var wallet in user.Value.Wallets)
                {
                    wallet.IsDefault = false;
                }
            }

            var newWallet = new Wallet
            {
                WalletName = request.WalletName,
                WalletCode = WalletCodeGenerator.GenerateRandom(6),
                Currency = request.Currency,
                Balance = 0,
                UserId = request.UserId,
                IsDefault = request.IsDefault,
                Transactions = new List<Transaction>()
            };

            user.Value.Wallets.ToList().Add(newWallet);

            await _walletRepository.InsertAsync(newWallet);
            await _unitOfWork.CommitAsync();

            return Result.Succeed();
        }
    }
}
