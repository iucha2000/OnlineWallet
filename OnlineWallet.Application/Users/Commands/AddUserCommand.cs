using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineWallet.Application.Common.Handlers;
using OnlineWallet.Application.Users.Models;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Enums;
using OnlineWallet.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineWallet.Application.Users.Commands
{
    public record AddUserCommand(string FirstName, string LastName, string Email, string Password, int Role) : IRequest<Result<string>>;

    internal class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result<string>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHandler _passwordHandler;
        private readonly ITokenHandler _tokenHandler;

        public AddUserCommandHandler(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork, IPasswordHandler passwordHandler, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHandler = passwordHandler;
            _tokenHandler = tokenHandler;
        }

        public async Task<Result<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
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

            var token = _tokenHandler.GenerateToken(newUser);

            return Result<string>.Succeed(token);
        }
    }

}
