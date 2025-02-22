using AutoMapper;
using BookManagement.Application.Books.Commands;
using BookManagement.Application.Books.Models;
using BookManagement.Application.Books.Services;
using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Entities;

namespace BookManagement.Infrastructure.Books.CommandHandlers;

public class BookCreateCommandHandler(
    IMapper mapper,
    IBookService bookService) : ICommandHandler<BookCreateCommand, CreateBookDto>
{
    public async Task<CreateBookDto> Handle(BookCreateCommand request, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Book>(request.BookDto);

        var createdBook = await bookService.CreateAsync(book, cancellationToken: cancellationToken);

        return mapper.Map<CreateBookDto>(createdBook);
    }
}
