using BookManagement.Application.Notifications.Models;
using BookManagement.Domain.Common.Commands;

namespace BookManagement.Application.Notifications.Commands;

public class EmailTemplateCreateCommand : ICommand<EmailTemplateDto>
{
    public EmailTemplateDto EmailTemplateDto { get; set; }
}
