using BookManagement.Application.Notifications.Models;

namespace BookManagement.Application.Notifications.Services;

public interface IEmailSenderService
{
    ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default);
}
