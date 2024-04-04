using MediatR;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Authentication.Commands.RegisterUser
{
    public record RegisterUserCommand(Guid UserId, string FirstName, string LastName, string Email, string Password) : IRequest<Result<string>>;
}
