using BookManagement.Application.Notifications.Models;
using BookManagement.Application.Identity.Models;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Application.Notifications.Queries;

public class EmailTemplateGetQuery : IQuery<ICollection<EmailTemplateDto>>
{
    public EmailTemplateFilter EmailTemplateFilter { get; set; }
}
