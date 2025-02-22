using BookManagement.Application.Books.Commands;
using BookManagement.Application.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async ValueTask<IActionResult> Get([FromQuery] BookGetQuery bookGetQuery, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(bookGetQuery, cancellationToken);

        return result.Any() ? Ok(result) : NoContent();
    }

    [HttpGet("{bookId:guid}")]
    public async ValueTask<IActionResult> GetBookById([FromRoute] Guid bookId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new BookGetByIdQuery { BookId = bookId }, cancellationToken);

        return result is not null ? Ok(result) : NotFound();
    }

    [HttpPost]
    public async ValueTask<IActionResult> CreateBook([FromBody] BookCreateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return result is not null ? Ok(result) : BadRequest();
    }

    [HttpPut]
    public async ValueTask<IActionResult> UpdateBook([FromBody] BookUpdateCommand command, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpDelete("{bookId:guid}")]
    public async ValueTask<IActionResult> DeleteBookById([FromRoute] Guid bookId, CancellationToken cancellationToken = default)
    {
        var result = await mediator.Send(new BookDeleteByIdCommand { BookId = bookId }, cancellationToken);

        return result ? Ok() : BadRequest();
    }
}
