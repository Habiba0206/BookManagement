using AutoMapper;
using BookManagement.Application.Books.Models;
using BookManagement.Application.Books.Queries;
using BookManagement.Application.Books.Services;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Infrastructure.Books.QueryHandlers;

public class BookGetByIdQueryHandler(
    IMapper mapper,
    IBookService bookService)
    : IQueryHandler<BookGetByIdQuery, GetBookDto>
{
    public async Task<GetBookDto> Handle(BookGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await bookService.GetByIdAsync(request.BookId, cancellationToken: cancellationToken);

        return mapper.Map<GetBookDto>(result);
    }
}
