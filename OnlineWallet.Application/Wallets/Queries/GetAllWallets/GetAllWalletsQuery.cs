﻿using MediatR;
using OnlineWallet.Application.Common.Models.Wallet;
using OnlineWallet.Domain.Common;

namespace OnlineWallet.Application.Wallets.Queries.GetAllWallets
{
    public record GetAllWalletsQuery(Guid UserId) : IRequest<Result<IList<GetWalletModel>>>;

}
