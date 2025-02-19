using BookManagement.Domain.Entities;

namespace BookManagement.Application.Identity.Services;

public interface IAccountAggregatorService
{
    ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default);
}