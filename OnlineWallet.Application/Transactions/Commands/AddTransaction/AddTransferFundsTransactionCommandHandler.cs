using MediatR;
using OnlineWallet.Application.Services;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Transactions.Commands.AddTransaction
{
    internal class AddTransferFundsTransactionCommandHandler : IRequestHandler<AddTransferFundsTransaction, Result<string>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBalanceManager _balanceManager;

        public AddTransferFundsTransactionCommandHandler(IGenericRepository<Transaction> transactionRepository, IGenericRepository<Wallet> walletRepository, IGenericRepository<User> userRepository, IUnitOfWork unitOfWork, IBalanceManager balanceManager)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _balanceManager = balanceManager;
        }

        public async Task<Result<string>> Handle(AddTransferFundsTransaction request, CancellationToken cancellationToken)
        {
            if (request.SenderUserId == Guid.Empty)
            {
                throw new EntityNotFoundException(ErrorMessages.AuthenticatedUserNotFound);
            }

            var senderUser = await _userRepository.GetAsync(x => x.Id == request.SenderUserId);
            if (senderUser.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserNotFound);
            }

            var receiverUser = await _userRepository.GetAsync(x=> x.Id == request.ReceiverUserId);
            if (receiverUser.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserNotFound);
            }

            //If given SenderWalletCode is null, we take user's default wallet for this transaction, else, we find wallet by given SenderWalletCode and use it
            var senderWallet = request.SenderWalletCode == null
                ? await _walletRepository.GetAsync(x => x.IsDefault == true && x.UserId == senderUser.Value.Id, includeProperties: "Transactions")
                : await _walletRepository.GetAsync(x => x.WalletCode == request.SenderWalletCode, includeProperties: "Transactions");

            if (senderWallet.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserHasNoWallets);
            }

            //If given ReceiverWalletCode is null, we take user's default wallet for this transaction, else, we find wallet by given ReceiverWalletCode and use it
            var receiverWallet = request.ReceiverWalletCode == null
                ? await _walletRepository.GetAsync(x => x.IsDefault == true && x.UserId == receiverUser.Value.Id, includeProperties: "Transactions")
                : await _walletRepository.GetAsync(x => x.WalletCode == request.ReceiverWalletCode, includeProperties: "Transactions");

            if (receiverWallet.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserHasNoWallets);
            }

            await _balanceManager.SubtractFunds(senderWallet.Value, request.Currency, request.Amount);
            await _balanceManager.AddFunds(receiverWallet.Value, request.Currency, request.Amount);

            var transaction = new Transaction
            {
                SenderUserId = senderUser.Value.Id,
                ReceiverUserId = receiverUser.Value.Id,
                SenderWalletCode = senderWallet.Value.WalletCode,
                ReceiverWalletCode = receiverWallet.Value.WalletCode,
                Currency = request.Currency,
                Amount = request.Amount,
                Date = DateTime.Now,
                Wallets = new List<Wallet> { senderWallet.Value, receiverWallet.Value },
            };

            await _transactionRepository.InsertAsync(transaction);
            await _unitOfWork.CommitAsync();

            return Result<string>.Succeed($"Transaction success: {request.Amount} {request.Currency} transferred from {senderUser.Value.FirstName}'s {senderWallet.Value.WalletName} to {receiverUser.Value.FirstName}'s {receiverWallet.Value.WalletName}");
        }
    }
}
