using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using System.Linq.Expressions;
using BookManagement.Persistence.Caching.Brokers;
using BookManagement.Persistence.Repositories.Interfaces;
using BookManagement.Persistence.DataContexts;

namespace BookManagement.Persistence.Repositories;

public class UserRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<User, AppDbContext>(appDbContext, cacheBroker),
    IUserRepository

{
    public IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    base.Get(predicate, queryOptions);

    public ValueTask<User?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    base.CheckByIdAsync(id, cancellationToken);

    public ValueTask<User> CreateAsync(
        User user,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(user, commandOptions, cancellationToken);

    public ValueTask<User> UpdateAsync(
        User user,
        CommandOptions commandOptions,
        CancellationToken cancellationToken) =>
    base.UpdateAsync(user, commandOptions, cancellationToken);

    public ValueTask<User?> DeleteAsync(
        User user,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.UpdateAsync(user, commandOptions, cancellationToken);

    public ValueTask<User?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
