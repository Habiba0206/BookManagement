using AutoMapper;
using BookManagement.Application.Identity.Models;
using BookManagement.Application.Identity.Queries;
using BookManagement.Application.Identity.Services;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Infrastructure.Identity.QueryHandlers;

public class UserGetByIdQueryHandler(
    IMapper mapper,
    IUserService userService)
    : IQueryHandler<UserGetByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(UserGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await userService.GetByIdAsync(request.UserId, cancellationToken: cancellationToken);

        return mapper.Map<UserDto>(result);
    }
}
