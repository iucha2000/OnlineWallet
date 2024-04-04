using MediatR;
using OnlineWallet.Application.Common.Handlers;
using OnlineWallet.Domain.Common.Interfaces;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Entities;
using OnlineWallet.Domain.Exceptions;

namespace OnlineWallet.Application.Authentication.Queries.LoginUser
{
    internal class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<string>>
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHandler _passwordHandler;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserQueryHandler(IGenericRepository<User> userRepository, IUnitOfWork unitOfWork, IPasswordHandler passwordHandler, ITokenHandler tokenHandler)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHandler = passwordHandler;
            _tokenHandler = tokenHandler;
        }

        public async Task<Result<string>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId != Guid.Empty)
            {
                throw new UserIsAuthenticatedException(ErrorMessages.UserIsAlreadyAuthenticated);
            }

            var existingUser = await _userRepository.GetAsync(x => x.Email == request.Email);

            if (existingUser.Value == null)
            {
                throw new EntityNotFoundException(ErrorMessages.IncorrectCredentials);
            }

            if (!_passwordHandler.VerifyPasswordHash(request.Password, existingUser.Value.PasswordHash, existingUser.Value.PasswordSalt))
            {
                throw new EntityNotFoundException(ErrorMessages.IncorrectCredentials);
            }

            var token = _tokenHandler.GenerateToken(existingUser.Value);

            return Result<string>.Succeed(token);
        }
    }
}
