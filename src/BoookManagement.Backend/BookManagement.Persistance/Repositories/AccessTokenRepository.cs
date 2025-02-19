using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Entities;
using BookManagement.Persistence.Caching.Models;
using BookManagement.Persistence.Caching.Brokers;
using BookManagement.Persistence.Repositories.Interfaces;

namespace BookManagement.Persistence.Repositories;

public class AccessTokenRepository(ICacheBroker cacheBroker) : IAccessTokenRepository
{
    public async ValueTask<AccessToken> CreateAsync(AccessToken accessToken, CommandOptions commandOptions, CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new CacheEntryOptions(accessToken.ExpiryTime - DateTimeOffset.UtcNow, null);
        await cacheBroker.SetAsync(accessToken.Id.ToString(), accessToken, cacheEntryOptions, cancellationToken);

        return accessToken;
    }

    public ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return cacheBroker.GetAsync<AccessToken>(accessTokenId.ToString(), cancellationToken);
    }

    public async ValueTask<AccessToken> UpdateAsync(AccessToken accessToken, CancellationToken cancellationToken = default)
    {
        var cacheEntryOptions = new CacheEntryOptions(accessToken.ExpiryTime - DateTimeOffset.UtcNow, null);
        await cacheBroker.SetAsync(accessToken.Id.ToString(), accessToken, cacheEntryOptions, cancellationToken);

        return accessToken;
    }
}