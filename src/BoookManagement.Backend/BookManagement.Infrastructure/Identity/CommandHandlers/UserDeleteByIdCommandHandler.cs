using BookManagement.Application.Identity.Commands;
using BookManagement.Application.Identity.Services;
using BookManagement.Domain.Common.Commands;

namespace BookManagement.Infrastructure.Identity.CommandHandlers;

public class UserDeleteByIdCommandHandler(
    IUserService userService)
    : ICommandHandler<UserDeleteByIdCommand, bool>
{
    public async Task<bool> Handle(UserDeleteByIdCommand request, CancellationToken cancellationToken)
    {
        var result = await userService.DeleteByIdAsync(request.UserId, cancellationToken: cancellationToken);

        return result is not null;
    }
}
