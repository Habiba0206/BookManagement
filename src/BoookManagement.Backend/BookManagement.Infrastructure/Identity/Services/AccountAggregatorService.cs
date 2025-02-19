using BookManagement.Application.Common.EventBus.Brokers;
using BookManagement.Application.Identity.Services;
using BookManagement.Application.Notifications.Events;
using BookManagement.Application.Verification.Services;
using BookManagement.Domain.Constants;
using BookManagement.Domain.Entities;
using BookManagement.Domain.Enums;

namespace BookManagement.Infrastructure.Identity.Services;

public class AccountAggregatorService(
    IUserService userService,
    IUserSettingsService userSettingsService,
    IUserInfoVerificationCodeService userInfoVerificationCodeService,
    IEventBusBroker eventBusBroker
) : IAccountAggregatorService
{
    public async ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        user.Role = Role.User;
        var createdUser = await userService.CreateAsync(user, cancellationToken: cancellationToken);
        await userSettingsService.CreateAsync(
            new UserSettings
            {
                Id = createdUser.Id,
            },
            cancellationToken: cancellationToken
        );

        var welcomeNotificationEvent = new ProcessNotificationEvent
        {
            ReceiverUserId = createdUser.Id,
            TemplateType = NotificationTemplateType.WelcomeNotification,
            Variables = new Dictionary<string, string>
            {
                { NotificationTemplateConstants.UserNamePlaceholder, createdUser.FirstName}
            }
        };

        await eventBusBroker.PublishAsync(
            welcomeNotificationEvent,
            //EventBusConstants.NotificationExchangeName,
            //EventBusConstants.ProcessNotificationQueueName,
            cancellationToken
        );

        var verificationCode = await userInfoVerificationCodeService.CreateAsync(
            VerificationCodeType.EmailAddressVerification,
            createdUser.Id,
            cancellationToken
        );

        var senderVerificationEvent = new ProcessNotificationEvent
        {
            ReceiverUserId = createdUser.Id,
            TemplateType = NotificationTemplateType.EmailAddressVerificationNotification,
            Variables = new Dictionary<string, string>
            {
                { NotificationTemplateConstants.EmailAddressVerificationLinkPlaceholder, verificationCode.VerificationLink}
            }
        };

        await eventBusBroker.PublishAsync(
            senderVerificationEvent,
            //EventBusConstants.NotificationExchangeName,
            //EventBusConstants.ProcessNotificationQueueName,
            cancellationToken
        );

        return true;
    }
}
