using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using BookManagement.Persistance.Caching.Brokers;
using BookManagement.Persistance.DataContexts;
using BookManagement.Persistance.Repositories;
using BookManagement.Persistence.Repositories.Interfaces;
using System.Linq.Expressions;

namespace BookManagement.Persistence.Repositories;

public class BookRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<Book, AppDbContext>(appDbContext, cacheBroker),
    IBookRepository

{
    public IQueryable<Book> Get(
        Expression<Func<Book, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    base.Get(predicate, queryOptions);

    public ValueTask<Book?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Book>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    base.CheckByIdAsync(id, cancellationToken);

    public ValueTask<Book> CreateAsync(
    Book book,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(book, commandOptions, cancellationToken);

    public ValueTask<Book> UpdateAsync(
        Book book,
        CommandOptions commandOptions,
        CancellationToken cancellationToken) =>
    base.UpdateAsync(book, commandOptions, cancellationToken);

    public ValueTask<Book?> DeleteAsync(
        Book book,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.UpdateAsync(book, commandOptions, cancellationToken);

    public ValueTask<Book?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions,
        CancellationToken cancellationToken = default) =>
    base.DeleteByIdAsync(id, commandOptions, cancellationToken);
}