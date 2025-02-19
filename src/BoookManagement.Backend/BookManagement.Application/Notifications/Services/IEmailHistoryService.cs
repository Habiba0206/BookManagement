using BookManagement.Application.Notifications.Models;
using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using System.Linq.Expressions;

namespace BookManagement.Application.Notifications.Services;

public interface IEmailHistoryService
{
    IQueryable<EmailHistory> Get(
             Expression<Func<EmailHistory, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    IQueryable<EmailHistory> Get(
        EmailHistoryFilter emailHistoryFilter,
        QueryOptions queryOptions = default);

    ValueTask<EmailHistory?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<EmailHistory>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<EmailHistory> CreateAsync(
        EmailHistory emailHistory,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}
