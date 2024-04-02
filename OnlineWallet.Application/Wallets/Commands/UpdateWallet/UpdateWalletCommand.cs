using MediatR;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Wallets.Commands.UpdateWallet
{
    public record UpdateWalletCommand(Guid UserId, string WalletCode, string? WalletName, string? Currency, bool? IsDefault) : IRequest<Result>;

}
