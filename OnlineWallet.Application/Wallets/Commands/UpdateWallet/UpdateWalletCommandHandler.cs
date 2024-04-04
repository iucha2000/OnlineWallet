using MediatR;
using OnlineWallet.Application.Services;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Enums;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Wallets.Commands.UpdateWallet
{
    internal class UpdateWalletCommandHandler : IRequestHandler<UpdateWalletCommand, Result>
    {
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBalanceManagerService _balanceManagerService;

        public UpdateWalletCommandHandler(IGenericRepository<Wallet> walletRepository, IGenericRepository<User> userRepository, IUnitOfWork unitOfWork, IBalanceManagerService balanceManagerService)
        {
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _balanceManagerService = balanceManagerService;
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

            if (request.Currency != null)
            {
                var oldCurrency = updatedWallet.Currency;
                var oldBalance = updatedWallet.Balance;
                updatedWallet.Currency = (CurrencyCode)request.Currency;
                updatedWallet.Balance = 0;
                await _balanceManagerService.AddFunds(updatedWallet, oldCurrency, oldBalance);
            }

            if (request.IsDefault != null)
            {
                if (request.IsDefault == true)
                {
                    foreach (var wallet in user.Value.Wallets)
                    {
                        wallet.IsDefault = false;
                    }
                }

                updatedWallet.IsDefault = (bool)request.IsDefault;
            }

            await _walletRepository.UpdateAsync(updatedWallet);
            await _unitOfWork.CommitAsync();

            return Result.Succeed();
        }
    }
}
