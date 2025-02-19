using BookManagement.Application.Notifications.Events;
using BookManagement.Application.Notifications.Models;
using BookManagement.Domain.Common.Exceptions;
using BookManagement.Domain.Entities;

namespace BookManagement.Application.Notifications.Services;

public interface INotificationAggregatorService
{
    public ValueTask<FuncResult<bool>> SendAsync(ProcessNotificationEvent processNotificationEvent, CancellationToken cancellationToken = default);

    public ValueTask<IList<NotificationTemplate>> GetTemplatesByFilterAsync(
        NotificationTemplateFilter notificationTemplateFilter,
        CancellationToken cancellationToken = default
        );
}
