using MediatR;
using OnlineWallet.Domain.Common;
using OnlineWallet.Domain.Enums;

namespace OnlineWallet.Application.Wallets.Commands.AddWallet
{
    public record AddWalletCommand(Guid UserId, string WalletName, CurrencyCode Currency, bool IsDefault) : IRequest<Result>;

}
