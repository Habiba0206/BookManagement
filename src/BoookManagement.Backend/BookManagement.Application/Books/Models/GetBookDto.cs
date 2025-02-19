namespace BookManagement.Application.Books.Models;

public class GetBookDto
{
    public Guid? Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Description { get; set; }
    public int PublicationYear { get; set; }
    public int ViewsCount { get; set; }
    public float PopularityScore { get; set; }
    public bool IsDeleted { get; set; }
}
