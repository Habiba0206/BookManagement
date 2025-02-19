using AutoMapper;
using BookManagement.Application.Identity.Commands;
using BookManagement.Application.Identity.Models;
using BookManagement.Application.Identity.Services;
using BookManagement.Application.Notifications.Commands;
using BookManagement.Application.Notifications.Models;
using BookManagement.Application.Notifications.Services;
using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Entities;

namespace BookManagement.Infrastructure.Notifications.CommandHandlers;

public class EmailTemplateCreateCommandHandler(
    IMapper mapper,
    IEmailTemplateService emailTemplateService) : ICommandHandler<EmailTemplateCreateCommand, EmailTemplateDto>
{
    public async Task<EmailTemplateDto> Handle(EmailTemplateCreateCommand request, CancellationToken cancellationToken)
    {
        var emailTemplate = mapper.Map<EmailTemplate>(request.EmailTemplateDto);

        var createdEmailTemplate = await emailTemplateService.CreateAsync(emailTemplate, cancellationToken: cancellationToken);

        return mapper.Map<EmailTemplateDto>(createdEmailTemplate);
    }
}
