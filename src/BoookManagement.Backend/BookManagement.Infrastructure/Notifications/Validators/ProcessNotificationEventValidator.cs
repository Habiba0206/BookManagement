using BookManagement.Application.Notifications.Events;
using FluentValidation;

namespace BookManagement.Infrastructure.Notifications.Validators;

public class ProcessNotificationEventValidator : AbstractValidator<ProcessNotificationEvent>
{
    public ProcessNotificationEventValidator()
    {
        RuleFor(history => history.ReceiverUserId).NotEqual(Guid.Empty);
    }
}