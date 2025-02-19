using AutoMapper;
using BookManagement.Application.Notifications.Models;

namespace BookManagement.Infrastructure.Notifications.Mappers;

public class NotificationMessageMapper : Profile
{
    public NotificationMessageMapper()
    {
        CreateMap<EmailProcessNotificationEvent, EmailMessage>();
    }
}