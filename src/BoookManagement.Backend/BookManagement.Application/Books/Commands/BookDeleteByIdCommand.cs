using BookManagement.Domain.Common.Commands;

namespace BookManagement.Application.Books.Commands;

public class BookDeleteByIdCommand : ICommand<bool>
{
    public Guid BookId { get; set; }
}
