﻿using MediatR;
using OnlineWallet.Application.Common.Handlers;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Enums;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Users.Commands.AddUser
{
    internal class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHandler _passwordHandler;

        public AddUserCommandHandler(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork, IPasswordHandler passwordHandler)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHandler = passwordHandler;
        }

        public async Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetAsync(x => x.Email == request.Email);
            if (existingUser.Value != null)
            {
                throw new EntityAlreadyExistsException(ErrorMessages.UserAlreadyExists);
            }

            _passwordHandler.CreateHashAndSalt(request.Password, out var passwordHash, out var passwordSalt);

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = (Role)request.Role,
                Wallets = new List<Wallet>()
            };

            await _userRepository.InsertAsync(newUser);
            await _unitOfWork.CommitAsync();

            return Result.Succeed();
        }
    }
}
