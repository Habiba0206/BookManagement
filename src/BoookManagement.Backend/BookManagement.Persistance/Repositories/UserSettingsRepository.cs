using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using BookManagement.Persistence.Caching.Brokers;
using BookManagement.Persistence.Repositories.Interfaces;
using BookManagement.Persistence.DataContexts;

namespace BookManagement.Persistence.Repositories;

public class UserSettingsRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<UserSettings, AppDbContext>(appDbContext, cacheBroker),
    IUserSettingsRepository

{
    public ValueTask<UserSettings?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<UserSettings> CreateAsync(
        UserSettings userSettings,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(userSettings, commandOptions, cancellationToken);
}
