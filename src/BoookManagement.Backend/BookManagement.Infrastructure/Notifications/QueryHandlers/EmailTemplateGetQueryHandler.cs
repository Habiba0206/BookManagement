using AutoMapper;
using BookManagement.Application.Notifications.Models;
using BookManagement.Application.Notifications.Queries;
using BookManagement.Application.Notifications.Services;
using BookManagement.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Notifications.QueryHandlers;

public class EmailTemplateGetQueryHandler(
    IMapper mapper,
    IEmailTemplateService emailTemplateService)
    : IQueryHandler<EmailTemplateGetQuery, ICollection<EmailTemplateDto>>
{
    public async Task<ICollection<EmailTemplateDto>> Handle(EmailTemplateGetQuery request, CancellationToken cancellationToken)
    {
        var result = await emailTemplateService.Get(
            request.EmailTemplateFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<EmailTemplateDto>>(result);
    }
}
