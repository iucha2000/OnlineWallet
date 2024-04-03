using MediatR;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Enums;

namespace OnlineWallet.Application.Wallets.Commands.UpdateWallet
{
    public record UpdateWalletCommand(Guid UserId, string WalletCode, string? WalletName, CurrencyCode? Currency, bool? IsDefault) : IRequest<Result>;

}
