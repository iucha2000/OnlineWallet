using MediatR;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Wallets.Commands.UpdateWallet
{
    internal class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, Result>
    {
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWalletCommandHandler(IGenericRepository<Wallet> walletRepository, IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
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

            var updatedWallet = user.Value.Wallets.FirstOrDefault(x=> x.WalletCode == request.WalletCode);
            if (updatedWallet == null)
            {
                throw new EntityNotFoundException(ErrorMessages.WalletNotFound);
            }

            if (!string.IsNullOrWhiteSpace(request.WalletName))
            {
                updatedWallet.WalletName = request.WalletName;
            }

            if (!string.IsNullOrWhiteSpace(request.Currency))
            {
                updatedWallet.Currency = request.Currency;
            }

            if (request.IsDefault != null)
            {
                updatedWallet.IsDefault = (bool)request.IsDefault;
            }

            await _walletRepository.UpdateAsync(updatedWallet);
            await _unitOfWork.CommitAsync();

            return Result.Succeed();
        }
    }
}
