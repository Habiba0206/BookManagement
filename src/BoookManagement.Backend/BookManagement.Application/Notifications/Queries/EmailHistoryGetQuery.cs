using BookManagement.Application.Notifications.Models;
using BookManagement.Domain.Common.Queries;

namespace BookManagement.Application.Notifications.Queries;

public class EmailHistoryGetQuery : IQuery<ICollection<EmailHistoryDto>>
{
    public EmailHistoryFilter EmailHistoryFilter { get; set; }
}
