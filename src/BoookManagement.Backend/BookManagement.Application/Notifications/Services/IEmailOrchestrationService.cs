using BookManagement.Application.Notifications.Models;
using BookManagement.Domain.Common.Exceptions;

namespace BookManagement.Application.Notifications.Services;

public interface IEmailOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(EmailProcessNotificationEvent @event, CancellationToken cancellationToken = default);
}
