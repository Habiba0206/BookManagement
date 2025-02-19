using AutoMapper;
using BookManagement.Application.Common.EventBus.Brokers;
using BookManagement.Application.Common.Settings;
using BookManagement.Application.Notifications.Events;
using BookManagement.Application.Notifications.Models;
using BookManagement.Application.Notifications.Services;
using BookManagement.Domain.Common.Events;
using BookManagement.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BookManagement.Infrastructure.Notifications.EventHandlers;

public class SendNotificationEventHandler(
    IServiceScopeFactory serviceScopeFactory,
    IMapper mapper,
    IOptions<NotificationSubscriberSettings> notificationSubscriberSettings,
    IEventBusBroker eventBusBroker,
    IOptions<NotificationSettings> notificationSettings
) : EventHandlerBase<SendNotificationEvent>
{
    protected override async ValueTask HandleAsync(SendNotificationEvent @event, CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var emailSenderService = scope.ServiceProvider.GetRequiredService<IEmailSenderService>();
        var emailHistoryService = scope.ServiceProvider.GetRequiredService<IEmailHistoryService>();

        if (@event.Message is EmailMessage emailMessage)
        {
            await emailSenderService.SendAsync(emailMessage, cancellationToken);

            var history = mapper.Map<EmailHistory>(emailMessage);
            history.SenderUserId = @event.SenderUserId;
            history.ReceiverUserId = @event.ReceiverUserId;

            Console.WriteLine($"TEMPLATE YES?: {history.TemplateId}");

            //await emailHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            if (!history.IsSuccessful) throw new InvalidOperationException("Email history is not created");
        }
    }
}
