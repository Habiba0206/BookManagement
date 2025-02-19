using BookManagement.Application.Notifications.Models;

namespace BookManagement.Application.Notifications.Brokers;

public interface IEmailSenderBroker
{
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}