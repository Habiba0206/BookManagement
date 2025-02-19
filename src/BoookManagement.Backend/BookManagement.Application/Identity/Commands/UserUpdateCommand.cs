using BookManagement.Application.Identity.Models;
using BookManagement.Domain.Common.Commands;

namespace BookManagement.Application.Identity.Commands;

public class UserUpdateCommand : ICommand<UserDto>
{
    public UserDto UserDto { get; set; }
}
