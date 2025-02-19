using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using System.Linq.Expressions;
using BookManagement.Persistence.Repositories.Interfaces;
using BookManagement.Persistence.Caching.Brokers;
using BookManagement.Persistence.DataContexts;

namespace BookManagement.Persistence.Repositories;

public class EmailTemplateRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<EmailTemplate, AppDbContext>(appDbContext, cacheBroker),
    IEmailTemplateRepository

{
    public IQueryable<EmailTemplate> Get(
        Expression<Func<EmailTemplate, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    base.Get(predicate, queryOptions);

    public ValueTask<EmailTemplate?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<EmailTemplate>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    base.CheckByIdAsync(id, cancellationToken);

    public ValueTask<EmailTemplate> CreateAsync(
        EmailTemplate emailTemplate,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    base.CreateAsync(emailTemplate, commandOptions, cancellationToken);
}