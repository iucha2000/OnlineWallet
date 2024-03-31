using MediatR;
using Microsoft.AspNetCore.Http;
using OnlineWallet.Application.Users.Models;
using OnlineWallet.Domain.Abstractions.Interfaces;
using OnlineWallet.Domain.Common;
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
    public record AddUserCommand(string FirstName, string LastName, string Email, string Password, int Role) : IRequest<Result>;

    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddUserCommandHandler(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = _userRepository.GetAsync(x => x.Email == request.Email).Result.Value;
            
            if (existingUser != null)
            {
                throw new EntityAlreadyExistsException(ErrorMessages.UserAlreadyExistsMessage);
            }

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = Encoding.ASCII.GetBytes(request.Password),
                PasswordSalt = Encoding.ASCII.GetBytes(request.Password),
                Role = (Role)request.Role,
                Wallets = new List<Wallet>()
            };

            await _userRepository.InsertAsync(newUser);
            await _unitOfWork.CommitAsync();
            return Result.Succeed();
        }
    }

}
