using BookManagement.Application.Notifications.Models;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Application.Notifications.Queries;

public class EmailHistoryGetByIdQuery : IQuery<EmailHistoryDto?>
{
    public Guid EmailHistoryId { get; set; }
}
