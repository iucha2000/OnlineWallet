using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnlineWallet.WebApi.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(ISender mediator) : base(mediator) { }

        [HttpPost("Register")]
        public async Task<IActionResult> Register()
        {
            //TODO register command

            return Ok();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login()
        {
            //TODO login command

            return Ok();
        }
    }
}
