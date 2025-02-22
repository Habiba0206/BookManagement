using AutoMapper;
using BookManagement.Application.Books.Commands;
using BookManagement.Application.Books.Models;
using BookManagement.Application.Books.Services;
using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Entities;

namespace BookManagement.Infrastructure.Books.CommandHandlers;

public class BookUpdateCommandHandler(
    IMapper mapper,
    IBookService bookService) : ICommandHandler<BookUpdateCommand, CreateBookDto>
{
    public async Task<CreateBookDto> Handle(BookUpdateCommand request, CancellationToken cancellationToken)
    {
        var book = mapper.Map<Book>(request.BookDto);

        var updatedBook = await bookService.UpdateAsync(book, cancellationToken: cancellationToken);

        Console.WriteLine(updatedBook.PublicationYear);

        return mapper.Map<CreateBookDto>(updatedBook);
    }
}
