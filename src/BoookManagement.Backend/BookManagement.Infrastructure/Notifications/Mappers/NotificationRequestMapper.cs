using AutoMapper;
using BookManagement.Application.Notifications.Events;
using BookManagement.Application.Notifications.Models;

namespace BookManagement.Infrastructure.Notifications.Mappers;

public class NotificationRequestMapper : Profile
{
    public NotificationRequestMapper()
    {
        CreateMap<ProcessNotificationEvent, EmailProcessNotificationEvent>();
    }
}