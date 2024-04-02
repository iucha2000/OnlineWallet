using MediatR;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Wallets.Commands.AddWallet
{
    public record AddWalletCommand(Guid UserId, string WalletName, string Currency, bool IsDefault) : IRequest<Result>;

}
