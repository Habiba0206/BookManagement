using BookManagement.Application.Notifications.Models;

namespace BookManagement.Application.Notifications.Services;

public interface IEmailRenderingService
{
    ValueTask<string> RenderAsync(
        EmailMessage emailMessage,
        CancellationToken cancellationToken = default
    );
}