using MediatR;
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
    public record UpdateUserCommand(Guid UserId, string? FirstName, string? LastName, string? Email, string? Password) : IRequest<Result>;

    internal class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHandler _passwordHandler;

        public UpdateUserCommandHandler(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork, IPasswordHandler passwordHandler)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHandler = passwordHandler;
        }

        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByIdAsync(request.UserId);

            if (existingUser.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.UserNotFound);
            }

            var userWithNewEmail = await _userRepository.GetAsync(x => x.Email == request.Email);

            if (userWithNewEmail.Value != null)
            {
                throw new EntityAlreadyExistsException(ErrorMessages.EmailAlreadyExists);
            }

            var updatedUser = existingUser.Value;

            if (!string.IsNullOrWhiteSpace(request.FirstName))
            {
                updatedUser.FirstName = request.FirstName;
            }

            if (!string.IsNullOrWhiteSpace(request.LastName))
            {
                updatedUser.LastName = request.LastName;
            }

            if (!string.IsNullOrWhiteSpace(request.Email) )
            {
                updatedUser.Email = request.Email;
            }

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                _passwordHandler.CreateHashAndSalt(request.Password, out var passwordHash, out var passwordSalt);
                updatedUser.PasswordHash = passwordHash;
                updatedUser.PasswordSalt = passwordSalt;
            }

            await _userRepository.UpdateAsync(updatedUser);
            await _unitOfWork.CommitAsync();

            return Result.Succeed();
        }
    }
}
