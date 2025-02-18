using BookManagement.Domain.Common.Entities;

namespace BookManagement.Domain.Entities;

public class Book : AuditableEntity
{
    public Guid UserId { get; set; }
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default;
    public int PublicationYear { get; set; }
    public int ViewsCount { get; set; }
}
