using AutoMapper;
using BookManagement.Application.Notifications.Models;
using BookManagement.Application.Notifications.Queries;
using BookManagement.Application.Notifications.Services;
using BookManagement.Domain.Common.Queries;
using BookManagement.Infrastructure.Notifications.Services;

namespace BookManagement.Infrastructure.Notifications.QueryHandlers;

public class EmailTemplateGetByIdQueryHandler(
    IMapper mapper,
    IEmailTemplateService emailTemplateService)
    : IQueryHandler<EmailTemplateGetByIdQuery, EmailTemplateDto>
{
    public async Task<EmailTemplateDto> Handle(EmailTemplateGetByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await emailTemplateService.GetByIdAsync(request.EmailTemplateId, cancellationToken: cancellationToken);

        return mapper.Map<EmailTemplateDto>(result);
    }
}

