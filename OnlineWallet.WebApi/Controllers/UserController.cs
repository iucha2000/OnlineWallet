using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineWallet.WebApi.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ISender mediator) : base(mediator) { }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser()
        {

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser()
        {

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser()
        {

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUser()
        {

            return Ok();
        }
    }
}
