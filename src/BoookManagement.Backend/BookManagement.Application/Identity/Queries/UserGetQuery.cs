using BookManagement.Application.Identity.Models;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Application.Identity.Queries;

public class UserGetQuery : IQuery<ICollection<UserDto>>
{
    public UserFilter UserFilter { get; set; }
}
