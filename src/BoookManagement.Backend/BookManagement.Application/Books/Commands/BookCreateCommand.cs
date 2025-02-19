using BookManagement.Application.Books.Models;
using BookManagement.Domain.Common.Commands;

namespace BookManagement.Application.Books.Commands;

public record BookCreateCommand : ICommand<CreateBookDto>
{
    public CreateBookDto BookDto { get; set; }
}
