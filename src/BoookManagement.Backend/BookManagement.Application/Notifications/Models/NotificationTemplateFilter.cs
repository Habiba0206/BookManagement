using BookManagement.Domain.Enums;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Application.Notifications.Models;

public class NotificationTemplateFilter : FilterPagination
{
    public IList<NotificationType> TemplateType { get; set; }
}