using BookManagement.Domain.Entities;
using BookManagement.Domain.Enums;
using FluentValidation;

namespace BookManagement.Infrastructure.Books.Validators;
public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(book => book.Title).NotEmpty().MinimumLength(2);
        RuleFor(book => book.Author).NotEmpty().MinimumLength(2);
    }
}
