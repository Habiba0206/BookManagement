using BookManagement.Application.Books.Models;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Application.Books.Queries;

public class BookGetByIdQuery : IQuery<GetBookDto?>
{
    public Guid BookId { get; set; }
}
