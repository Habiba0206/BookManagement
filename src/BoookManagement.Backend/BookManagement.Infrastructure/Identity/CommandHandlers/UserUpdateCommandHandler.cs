using AutoMapper;
using BookManagement.Application.Identity.Commands;
using BookManagement.Application.Identity.Models;
using BookManagement.Application.Identity.Services;
using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Entities;

namespace BookManagement.Infrastructure.Identity.CommandHandlers;

public class UserUpdateCommandHandler(
    IMapper mapper,
    IUserService userService) : ICommandHandler<UserUpdateCommand, UserDto>
{
    public async Task<UserDto> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request.UserDto);

        var updatedUser = await userService.UpdateAsync(user, cancellationToken: cancellationToken);

        return mapper.Map<UserDto>(updatedUser);
    }
}
