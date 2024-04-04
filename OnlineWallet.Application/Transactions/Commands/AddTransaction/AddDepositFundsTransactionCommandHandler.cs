﻿using MediatR;
using OnlineWallet.Application.Services;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Transactions.Commands.AddTransaction
{
    internal class AddDepositFundsTransactionCommandHandler : IRequestHandler<AddDepositFundsTransaction, Result<string>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBalanceManager _balanceManager;

        public AddDepositFundsTransactionCommandHandler(IGenericRepository<User> userRepository, IGenericRepository<Wallet> walletRepository, IGenericRepository<Transaction> transactionRepository, IUnitOfWork unitOfWork, IBalanceManager balanceManager)
        {
            _userRepository = userRepository;
            _walletRepository = walletRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _balanceManager = balanceManager;
        }

        public async Task<Result<string>> Handle(AddDepositFundsTransaction request, CancellationToken cancellationToken)
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

            //If given WalletCode is null, we take user's default wallet for this transaction, else, we find wallet by given WalletCode and use it
            var wallet = request.WalletCode == null
                ? await _walletRepository.GetAsync(x => x.IsDefault == true && x.UserId == user.Value.Id, includeProperties: "TransactionHistory")
                : await _walletRepository.GetAsync(x => x.WalletCode == request.WalletCode, includeProperties: "TransactionHistory");

            if (wallet.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserHasNoWallets);
            }

            await _balanceManager.AddFunds(wallet.Value, request.Currency, request.Amount);

            var transaction = new Transaction
            {
                //TODO fix static values
                SenderUserId = Guid.Empty,
                ReceiverUserId = request.UserId,
                SenderWalletCode = "DEPOSIT OPERATION",
                ReceiverWalletCode = wallet.Value.WalletCode,
                Currency = request.Currency,
                Amount = request.Amount,
                Date = DateTime.Now,
                WalletId = wallet.Value.Id,
            };

            wallet.Value.TransactionHistory.ToList().Add(transaction);

            await _transactionRepository.InsertAsync(transaction);
            await _unitOfWork.CommitAsync();

            return Result<string>.Succeed($"Transaction success: {request.Amount} {request.Currency} deposited to {user.Value.FirstName}'s {wallet.Value.WalletName}");
        }
    }
}