using BookManagement.Domain.Common.Events;

namespace BookManagement.Application.Notifications.Events;

public record NotificationEvent : EventBase
{
    public Guid SenderUserId { get; set; }

    public Guid ReceiverUserId { get; set; }
}
