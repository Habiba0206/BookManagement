using BookManagement.Application.Books.Commands;
using BookManagement.Application.Books.Services;
using BookManagement.Domain.Common.Commands;

namespace BookManagement.Infrastructure.Books.CommandHandlers;

public class BookDeleteByIdCommandHandler(
    IBookService bookService)
    : ICommandHandler<BookDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(BookDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await bookService.DeleteByIdAsync(request.BookId, cancellationToken: cancellationToken);

        return result is not null;
    }
}