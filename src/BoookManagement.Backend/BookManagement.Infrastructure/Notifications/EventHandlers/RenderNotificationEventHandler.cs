using AutoMapper;
using BookManagement.Application.Common.EventBus.Brokers;
using BookManagement.Application.Common.Settings;
using BookManagement.Application.Notifications.Events;
using BookManagement.Application.Notifications.Models;
using BookManagement.Application.Notifications.Services;
using BookManagement.Domain.Common.Events;
using BookManagement.Domain.Entities;
using BookManagement.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BookManagement.Infrastructure.Notifications.EventHandlers;

public class RenderNotificationEventHandler(
    IServiceScopeFactory serviceScopeFactory,
    IOptions<NotificationSubscriberSettings> notificationSubscriberSettings,
    IEventBusBroker eventBusBroker,
    IOptions<NotificationSettings> notificationSettings) : EventHandlerBase<RenderNotificationEvent>
{
    protected override async ValueTask HandleAsync(RenderNotificationEvent @event, CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var emailRenderingService = scope.ServiceProvider.GetService<IEmailRenderingService>();

        if (@event.Template.Type == NotificationType.Email)
        {
            var emailMessage = new EmailMessage()
            {
                SenderEmailAddress = @event.SenderUser.EmailAddress,
                ReceiverEmailAddress = @event.ReceiverUser.EmailAddress,
                Template = (EmailTemplate)@event.Template,
                Variables = @event.Variables
            };

            await emailRenderingService.RenderAsync(emailMessage, cancellationToken);

            var sendNotificationEvent = new SendNotificationEvent
            {
                SenderUserId = @event.SenderUserId,
                ReceiverUserId = @event.ReceiverUserId,
                Message = emailMessage,
            };

            await eventBusBroker.PublishAsync(
               sendNotificationEvent,
               cancellationToken
           );
        }
    }
}
