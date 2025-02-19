using BookManagement.Application.Notifications.Events;
using BookManagement.Domain.Enums;

namespace BookManagement.Application.Notifications.Models;

public record EmailProcessNotificationEvent : ProcessNotificationEvent
{
    public EmailProcessNotificationEvent()
    {
        Type = NotificationType.Email;
    }
}