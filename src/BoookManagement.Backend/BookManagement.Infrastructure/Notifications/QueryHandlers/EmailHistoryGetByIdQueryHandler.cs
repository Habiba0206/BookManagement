using AutoMapper;
using BookManagement.Application.Notifications.Models;
using BookManagement.Application.Notifications.Queries;
using BookManagement.Application.Notifications.Services;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Infrastructure.Notifications.QueryHandlers;

public class EmailHistoryGetByIdQueryHandler(
    IMapper mapper,
    IEmailHistoryService emailHistoryService)
    : IQueryHandler<EmailHistoryGetByIdQuery, EmailHistoryDto>
{
    public async Task<EmailHistoryDto> Handle(EmailHistoryGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await emailHistoryService.GetByIdAsync(request.EmailHistoryId, cancellationToken: cancellationToken);

        return mapper.Map<EmailHistoryDto>(result);
    }
}
