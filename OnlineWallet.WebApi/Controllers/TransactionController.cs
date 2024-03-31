using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineWallet.WebApi.Controllers
{
    public class TransactionController : BaseController
    {
        public TransactionController(ISender mediator) : base(mediator) { }

        [Authorize]
        [HttpPost("add-transaction")]
        public async Task<IActionResult> AddTransaction()
        {

            return Ok();
        }

        [Authorize]
        [HttpGet("get-transaction-by-id")]
        public async Task<IActionResult> GetTransaction()
        {

            return Ok();
        }
    }
}
