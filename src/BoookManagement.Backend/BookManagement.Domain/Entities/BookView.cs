using BookManagement.Domain.Common.Entities;

namespace BookManagement.Domain.Entities;

public class BookView : AuditableEntity
{
    public Guid BookId { get; set; }
    public DateTimeOffset AccessedAt { get; set; } 
}
