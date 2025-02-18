using BookManagement.Domain.Common.Entities;
using BookManagement.Domain.Enums;

namespace BookManagement.Domain.Entities;

public class UserSettings : IEntity
{
    public NotificationType? PreferredNotificationType { get; set; }

    /// <summary>
    ///     Gets or sets the user Id
    /// </summary>
    public Guid Id { get; set; }
}