using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Entities;

namespace BookManagement.Persistence.Repositories.Interfaces;

public interface IAccessTokenRepository
{
    ValueTask<AccessToken> CreateAsync(
        AccessToken accessToken,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default);
    ValueTask<AccessToken?> GetByIdAsync(
        Guid accessTokeId,
        CancellationToken cancellationToken = default);
    ValueTask<AccessToken> UpdateAsync(
        AccessToken accessToken,
        CancellationToken cancellationToken = default);
}
