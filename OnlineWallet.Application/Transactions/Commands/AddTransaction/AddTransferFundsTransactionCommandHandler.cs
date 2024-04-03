﻿using MediatR;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;

namespace OnlineWallet.Application.Transactions.Commands.AddTransaction
{
    internal class AddTransferFundsTransactionCommandHandler : IRequestHandler<AddTransferFundsTransaction, Result>
    {
        private readonly IGenericRepository<Transaction> _transactionRepository;
        private readonly IGenericRepository<Wallet> _walletRepository;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddTransferFundsTransactionCommandHandler(IGenericRepository<Transaction> transactionRepository, IGenericRepository<Wallet> walletRepository, IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _transactionRepository = transactionRepository;
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(AddTransferFundsTransaction request, CancellationToken cancellationToken)
        {








            throw new NotImplementedException();
        }
    }
}
