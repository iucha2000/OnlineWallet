using MediatR;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Authentication.Queries.LoginUser
{
    public record LoginUserQuery(Guid UserId, string Email, string Password) : IRequest<Result<string>>;
}
