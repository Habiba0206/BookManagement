using BookManagement.Application.Notifications.Models;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Application.Notifications.Queries;

public class EmailTemplateGetByIdQuery : IQuery<EmailTemplateDto?>
{
    public Guid EmailTemplateId { get; set; }
}
