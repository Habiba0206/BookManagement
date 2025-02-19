using BookManagement.Application.Identity.Models;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Application.Identity.Queries;

public class UserGetByIdQuery : IQuery<UserDto?>
{
    public Guid UserId { get; set; }
}
