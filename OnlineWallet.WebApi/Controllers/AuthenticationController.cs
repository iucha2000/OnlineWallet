using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Application.Authentication.Commands.RegisterUser;
using OnlineWallet.Application.Authentication.Queries.LoginUser;
using OnlineWallet.Application.Common.Models.Authentication;

namespace OnlineWallet.WebApi.Controllers
{
    public class AuthenticationController : BaseController
    {
        public AuthenticationController(ISender mediator) : base(mediator) { }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var command = new RegisterUserCommand(registerModel.FirstName, registerModel.LastName, registerModel.EmailAddress, registerModel.Password);

            var result = await _mediator.Send(command);
            
            if(result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Problem(result.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginModel loginUserModel)
        {
            var query = new LoginUserQuery(loginUserModel.EmailAddress, loginUserModel.Password);

            var result = await _mediator.Send(query);

            if(result.Success)
            {
                return Ok(result);
            }
            else
            {
                return Problem(result.Message);
            }
        }
    }
}
