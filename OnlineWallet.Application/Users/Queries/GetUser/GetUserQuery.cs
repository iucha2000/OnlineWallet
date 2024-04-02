using MediatR;
using OnlineWallet.Application.Common.Models;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Users.Queries.GetUser
{
    public record GetUserQuery(Guid userId) : IRequest<Result<GetUserModel>>;

}
