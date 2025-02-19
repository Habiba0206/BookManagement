using BookManagement.Application.Notifications.Models;

namespace BookManagement.Application.Notifications.Events;

public record SendNotificationEvent : NotificationEvent
{
    public NotificationMessage Message { get; set; } = default!;
}
