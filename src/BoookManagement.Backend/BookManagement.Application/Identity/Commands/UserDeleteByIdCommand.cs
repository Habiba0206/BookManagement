using BookManagement.Domain.Common.Commands;

namespace BookManagement.Application.Identity.Commands;

public class UserDeleteByIdCommand : ICommand<bool>
{
    public Guid UserId { get; set; }
}
