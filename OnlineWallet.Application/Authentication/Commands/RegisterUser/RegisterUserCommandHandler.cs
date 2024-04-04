using MediatR;
using OnlineWallet.Application.Common.Handlers;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Enums;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Authentication.Commands.RegisterUser
{
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<string>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHandler _passwordHandler;
        private readonly ITokenHandler _tokenHandler;

        public RegisterUserCommandHandler(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork, IPasswordHandler passwordHandler, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHandler = passwordHandler;
            _tokenHandler = tokenHandler;
        }

        public async Task<Result<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if(request.UserId != Guid.Empty)
            {
                throw new UserIsAuthenticatedException(ErrorMessages.UserIsAlreadyAuthenticated);
            }

            var existingUser = await _userRepository.GetAsync(x => x.Email == request.Email);
            if (existingUser.Value != null)
            {
                throw new EntityAlreadyExistsException(ErrorMessages.EmailAlreadyExists);
            }

            _passwordHandler.CreateHashAndSalt(request.Password, out var passwordHash, out var passwordSalt);

            var newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = Role.Client,
                Wallets = new List<Wallet>(),
            };

            await _userRepository.InsertAsync(newUser);
            await _unitOfWork.CommitAsync();

            var token = _tokenHandler.GenerateToken(newUser);

            return Result<string>.Succeed(token);
        }
    }
}
