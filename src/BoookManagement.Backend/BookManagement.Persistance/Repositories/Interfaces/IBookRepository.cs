using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using System.Linq.Expressions;

namespace BookManagement.Persistence.Repositories.Interfaces;

public interface IBookRepository
{
    IQueryable<Book> Get(
             Expression<Func<Book, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    ValueTask<Book?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<Book>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Book> CreateAsync(
        Book book,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Book> UpdateAsync(
        Book book,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Book?> DeleteAsync(
        Book book,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<Book?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
