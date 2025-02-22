using BookManagement.Application.Identity.Commands;
using BookManagement.Application.Identity.Models;
using BookManagement.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class AdminController(IMediator mediator) : ControllerBase
{
    [HttpPost("users/{userId}/block")]
    public async ValueTask<IActionResult> BlockUser([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var user = new UserDto { Id = userId, UserState = UserState.Blocked };
        var command = new UserUpdateCommand { UserDto = user };

        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("users/{userId}/unblock")]
    public async ValueTask<IActionResult> UnblockUser([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var user = new UserDto { Id = userId, UserState = UserState.Blocked };
        var command = new UserUpdateCommand { UserDto = user };

        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("users/{userId}/makeAdmin")]
    public async ValueTask<IActionResult> MakeUserAdmin([FromRoute] Guid userId, CancellationToken cancellationToken)
    {
        var user = new UserDto { Id = userId, Role = Role.Admin };
        var command = new UserUpdateCommand { UserDto = user };

        await mediator.Send(command, cancellationToken);

        return Ok();
    }
}
