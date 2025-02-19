using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using System.Linq.Expressions;

namespace BookManagement.Persistence.Repositories.Interfaces;

public interface IEmailTemplateRepository
{
    IQueryable<EmailTemplate> Get(
             Expression<Func<EmailTemplate, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    ValueTask<EmailTemplate?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    ValueTask<IList<EmailTemplate>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

    ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate emailTemplate,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default);
}

