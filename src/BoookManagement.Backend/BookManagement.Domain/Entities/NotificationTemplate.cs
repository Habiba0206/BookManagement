using BookManagement.Domain.Common.Entities;
using BookManagement.Domain.Enums;

namespace BookManagement.Domain.Entities;

public abstract class NotificationTemplate : AuditableEntity
{
    public NotificationType Type { get; set; }

    public NotificationTemplateType TemplateType { get; set; }

    public string Content { get; set; } = default!;

    public IList<NotificationHistory> Histories { get; set; } = new List<NotificationHistory>();
    public Guid Id { get; set; }
}