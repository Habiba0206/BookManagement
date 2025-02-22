using AutoMapper;
using BookManagement.Application.Identity.Commands;
using BookManagement.Application.Identity.Models;
using BookManagement.Application.Identity.Queries;
using BookManagement.Application.Identity.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMapper mapper, IAuthAggregationService authAggregationService, IMediator mediator) : ControllerBase
{
    [HttpPost("sign-up")]
    public async ValueTask<IActionResult> SignUp([FromBody] SignUpDetails signUpDetails, CancellationToken cancellationToken)
    {
        var result = await authAggregationService.SignUpAsync(signUpDetails, cancellationToken);
        return result ? Ok() : BadRequest();
    }

    [HttpPost("sign-in")]
    public async ValueTask<IActionResult> SignIn([FromBody] SignInDetails singInDetails, CancellationToken cancellationToken)
    {
        var result = await authAggregationService.SignInAsync(singInDetails, cancellationToken);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] UserGetQuery userGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(userGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{userId:guid}")]
    public async ValueTask<IActionResult> GetUserById([FromRoute] Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new UserGetByIdQuery { UserId = userId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateUser([FromBody] UserUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{userId:guid}")]
    public async ValueTask<IActionResult> DeleteQuestionById([FromRoute] Guid userId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new UserDeleteByIdCommand { UserId = userId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}