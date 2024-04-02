using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Application.Common.Models.Wallet;
using OnlineWallet.Application.Users.Commands.DeleteUser;
using OnlineWallet.Application.Wallets.Commands.AddWallet;
using OnlineWallet.Application.Wallets.Commands.DeleteWallet;
using OnlineWallet.Application.Wallets.Commands.UpdateWallet;
using OnlineWallet.Application.Wallets.Queries.GetWallet;
using OnlineWallet.Domain.Entities;
using OnlineWallet.WebApi.Extensions;

namespace OnlineWallet.WebApi.Controllers
{
    public class WalletController : BaseController
    {
        public WalletController(ISender mediator) : base(mediator) { }

        [Authorize]
        [HttpPost("add-wallet")]
        public async Task<IActionResult> AddWallet([FromForm] AddWalletModel addWalletModel)
        {
            var userId = HttpContext.GetUserId();
            var command = new AddWalletCommand(userId, addWalletModel.WalletName, addWalletModel.Currency, addWalletModel.IsDefault);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("update-wallet")]
        public async Task<IActionResult> UpdateWallet([FromForm] UpdateWalletModel updateWalletModel)
        {
            var userId = HttpContext.GetUserId();
            var command = new UpdateWalletCommand(userId, updateWalletModel.WalletCode, updateWalletModel.WalletName, updateWalletModel.Currency, updateWalletModel.IsDefault);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("delete-wallet")]
        public async Task<IActionResult> DeleteWallet(string walletCode)
        {
            var userId = HttpContext.GetUserId();
            var command = new DeleteWalletCommand(userId, walletCode);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("get-wallet-by-code")]
        public async Task<IActionResult> GetWallet(string walletCode)
        {
            var userId = HttpContext.GetUserId();
            var query = new GetWalletQuery(userId,walletCode);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
