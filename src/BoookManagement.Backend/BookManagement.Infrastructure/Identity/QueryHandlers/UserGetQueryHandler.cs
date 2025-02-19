using AutoMapper;
using BookManagement.Application.Identity.Models;
using BookManagement.Application.Identity.Queries;
using BookManagement.Application.Identity.Services;
using BookManagement.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Identity.QueryHandlers;

public class UserGetQueryHandler(
    IMapper mapper,
    IUserService userService)
    : IQueryHandler<UserGetQuery, ICollection<UserDto>>
{
    public async Task<ICollection<UserDto>> Handle(UserGetQuery request, CancellationToken cancellationToken)
    {
        var result = await userService.Get(
            request.UserFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<UserDto>>(result);
    }
}
