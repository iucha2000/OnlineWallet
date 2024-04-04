using MediatR;
using OnlineWallet.Application.Common.DTOs.Wallet;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Wallets.Queries.GetWallet
{
    public record GetWalletQuery(Guid UserId, string WalletCode) : IRequest<Result<GetWalletModel>>;
}
