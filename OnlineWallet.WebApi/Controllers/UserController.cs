﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Application.Users.Commands;
using OnlineWallet.Application.Users.Models;
using OnlineWallet.Application.Users.Queries;

namespace OnlineWallet.WebApi.Controllers
{
    public class UserController : BaseController
    {
        public UserController(ISender mediator) : base(mediator) { }

        [Authorize(Roles = "Admin")]
        [HttpPost("add-user")]
        public async Task<IActionResult> AddUser(AddUserModel userModel)
        {
            var command = new AddUserCommand(userModel.FirstName, userModel.LastName, userModel.Email, userModel.Password, userModel.Role);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserModel userModel)
        {
            var command = new UpdateUserCommand(userId, userModel.FirstName, userModel.LastName, userModel.Email, userModel.Password);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var command = new DeleteUserCommand(userId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-user-by-id")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            var query = new GetUserQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
