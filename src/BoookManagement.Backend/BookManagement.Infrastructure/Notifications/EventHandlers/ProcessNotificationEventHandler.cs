using BookManagement.Application.Common.EventBus.Brokers;
using BookManagement.Application.Common.Settings;
using BookManagement.Application.Identity.Services;
using BookManagement.Application.Notifications.Events;
using BookManagement.Application.Notifications.Services;
using BookManagement.Domain.Common.Events;
using BookManagement.Domain.Common.Queries;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BookManagement.Infrastructure.Notifications.EventHandlers;

public class ProcessNotificationEventHandler(
    IServiceScopeFactory serviceScopeFactory,
    IOptions<NotificationSubscriberSettings> notificationSubscriberSettings,
    IEventBusBroker eventBusBroker,
    IOptions<NotificationSettings> notificationSettings) : EventHandlerBase<ProcessNotificationEvent>
{
    private readonly NotificationSettings _notificationSettings = notificationSettings.Value;
    protected override async ValueTask HandleAsync(ProcessNotificationEvent @event, CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var emailTemplateService = scope.ServiceProvider.GetRequiredService<IEmailTemplateService>();
        var processNotificationEventValidator = scope.ServiceProvider.GetRequiredService<IValidator<ProcessNotificationEvent>>();

        var validationResult = await processNotificationEventValidator.ValidateAsync(@event, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var senderUser = @event.SenderUserId != Guid.Empty
            ? await userService.GetByIdAsync(@event.SenderUserId, cancellationToken: cancellationToken)
            : await userService.GetSystemUserAsync(new QueryOptions(QueryTrackingMode.AsNoTracking), cancellationToken);

        var receiverUser = await userService.GetByIdAsync(@event.ReceiverUserId, cancellationToken: cancellationToken);

        //if (!@event.Type.HasValue && receiverUser!.UserSettings!.PreferredNotificationType.HasValue)
        //    @event.Type = receiverUser!.UserSettings!.PreferredNotificationType;

        if (senderUser is null)
            throw new ArgumentNullException(nameof(senderUser), "Sender user is null.");

        if (receiverUser is null)
            throw new ArgumentNullException(nameof(receiverUser), "Receiver user is null.");

        var template = await emailTemplateService.GetByTypeAsync(@event.TemplateType, cancellationToken: cancellationToken) ?? throw new InvalidOperationException($"Template for type {@event.TemplateType} was not found.");

        if (!@event.Type.HasValue)
            @event.Type = _notificationSettings.DefaultNotificationType;

        var renderNotificationEvent = new RenderNotificationEvent
        {
            SenderUserId = senderUser!.Id,
            ReceiverUserId = receiverUser!.Id,
            Template = template!,
            SenderUser = senderUser!,
            ReceiverUser = receiverUser!,
            Variables = @event.Variables ?? new Dictionary<string, string>(),
        };

        await eventBusBroker.PublishAsync(
            renderNotificationEvent!,
            cancellationToken
        );
    }
}
