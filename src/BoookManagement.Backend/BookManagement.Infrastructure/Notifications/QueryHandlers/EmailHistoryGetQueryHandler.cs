using AutoMapper;
using BookManagement.Application.Notifications.Models;
using BookManagement.Application.Notifications.Queries;
using BookManagement.Application.Notifications.Services;
using BookManagement.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Notifications.QueryHandlers;

public class EmailHistoryGetQueryHandler(
    IMapper mapper,
    IEmailHistoryService emailHistoryService)
    : IQueryHandler<EmailHistoryGetQuery, ICollection<EmailHistoryDto>>
{
    public async Task<ICollection<EmailHistoryDto>> Handle(EmailHistoryGetQuery request, CancellationToken cancellationToken)
    {
        var result = await emailHistoryService.Get(
            request.EmailHistoryFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<EmailHistoryDto>>(result);
    }
}
