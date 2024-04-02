using MediatR;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Wallets.Commands.DeleteWallet
{
    public record DeleteWalletCommand(Guid UserId, string WalletCode) : IRequest<Result>;

}
