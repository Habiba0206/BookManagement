using BookManagement.Application.Notifications.Models;
using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using BookManagement.Domain.Enums;
using System.Linq.Expressions;

namespace BookManagement.Application.Notifications.Services;

public interface IEmailTemplateService
{
    IQueryable<EmailTemplate> Get(
             Expression<Func<EmailTemplate, bool>>? predicate = default,
             QueryOptions queryOptions = default);

    IQueryable<EmailTemplate> Get(
        EmailTemplateFilter emailTemplateFilter,
        QueryOptions queryOptions = default);

    ValueTask<EmailTemplate?> GetByTypeAsync(
        NotificationTemplateType templateType,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default);

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
