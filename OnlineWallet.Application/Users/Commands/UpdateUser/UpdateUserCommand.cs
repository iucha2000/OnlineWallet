using MediatR;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(Guid UserId, string? FirstName, string? LastName, string? Email, string? Password) : IRequest<Result>;

}
