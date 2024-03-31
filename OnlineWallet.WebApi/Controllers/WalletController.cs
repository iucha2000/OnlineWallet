using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineWallet.WebApi.Controllers
{
    public class WalletController : BaseController
    {
        public WalletController(ISender mediator) : base(mediator) { }

        [Authorize]
        [HttpPost("add-wallet")]
        public async Task<IActionResult> AddWallet()
        {

            return Ok();
        }

        [Authorize]
        [HttpPut("update-wallet")]
        public async Task<IActionResult> UpdateWallet()
        {

            return Ok();
        }

        [Authorize]
        [HttpDelete("delete-wallet")]
        public async Task<IActionResult> DeleteWallet()
        {

            return Ok();
        }

        [Authorize]
        [HttpGet("get-wallet-by-id")]
        public async Task<IActionResult> GetWallet()
        {

            return Ok();
        }
    }
}
