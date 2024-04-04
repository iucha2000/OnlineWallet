using MediatR;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Users.Commands.DeleteUser
{
    public record DeleteUserCommand(Guid UserId) : IRequest<Result>;

}
