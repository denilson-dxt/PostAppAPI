using Application.Dtos;
using Application.Users;
using Doiman;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController : BaseApiController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUser.CreateUserCommand command)
    {
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> ListAllUsers()
    {
        return await _mediator.Send(new ListUsers.ListUsersQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> ListUserById(string id)
    {
        return await _mediator.Send(new ListUserById.ListUserByIdQuery() {Id = id});
    }


    [HttpPut("{id}")]
    public async Task<ActionResult<UserDto>> UpdateUser(UpdateUser.UpdateUserCommand command, string id)
    {
        command.Id = id;
        return await _mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<UserDto>> DeleteUser(string id)
    {
        return await _mediator.Send(new DeleteUser.DeleteUserCommand() {Id = id});
    }

    [HttpGet("usernames")]
    public async Task<ActionResult<List<string>>> GetUsernames()
    {
        return await _mediator.Send(new UsernameList.UsernameListQuery());
    }
}