using MediatR;
using OnlineWallet.Application.Common.Models.User;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Users.Queries.GetAllUsers
{
    public record GetAllUsersQuery() : IRequest<Result<IList<GetUserModel>>>;
}
