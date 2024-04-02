using MediatR;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Wallets.Commands.DeleteWallet
{
    internal class DeleteWalletCommandHandler : IRequestHandler<DeleteWalletCommand, Result>
    {
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteWalletCommandHandler(IGenericRepository<Wallet> walletRepository, IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteWalletCommand request, CancellationToken cancellationToken)
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

            var deletedWallet = user.Value.Wallets.FirstOrDefault(x => x.WalletCode == request.WalletCode);
            if (deletedWallet == null)
            {
                throw new EntityNotFoundException(ErrorMessages.WalletNotFound);
            }

            await _walletRepository.DeleteAsync(deletedWallet);
            await _unitOfWork.CommitAsync();

            return Result.Succeed();
        }
    }
}
