namespace BookManagement.Application.Books.Models;

public class CreateBookDto
{
    public Guid? Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = default!;
    public string Author { get; set; } = default!;
    public string Description { get; set; }
}
