using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Application.Users.Commands;
using OnlineWallet.Application.Users.Models;

namespace OnlineWallet.WebApi.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ISender mediator) : base(mediator) { }

        //[Authorize(Roles = "Admin")]
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser(UserModel userModel)
        {
            var command = new AddUserCommand(userModel.FirstName, userModel.LastName, userModel.Email, userModel.Password, userModel.Role);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser()
        {

            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser()
        {

            return Ok();
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUser()
        {

            return Ok();
        }
    }
}
