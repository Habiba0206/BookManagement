using BookManagement.Application.Books.Models;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Application.Books.Queries;

public class BookGetQuery : IQuery<ICollection<GetBookDto>>
{
    public BookFilter BookFilter { get; set; }
}
