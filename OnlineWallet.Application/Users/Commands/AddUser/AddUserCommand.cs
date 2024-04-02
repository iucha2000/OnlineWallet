using MediatR;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Users.Commands.AddUser
{
    public record AddUserCommand(string FirstName, string LastName, string Email, string Password, int Role) : IRequest<Result>;

}
