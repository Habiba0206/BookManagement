using BookManagement.Infrastructure.Books.Validators;
using BookManagement.Application.Books.Models;
using BookManagement.Application.Books.Services;
using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using BookManagement.Domain.Enums;
using BookManagement.Persistence.Extensions;
using BookManagement.Persistence.Repositories.Interfaces;
using FluentValidation;
using System.Linq.Expressions;

namespace BookManagement.Infrastructure.Books.Services;

public class BookService(
    IBookRepository bookRepository,
    BookValidator validator)
   : IBookService
{
    public IQueryable<Book> Get(
        Expression<Func<Book, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    bookRepository.Get(predicate, queryOptions);

    public IQueryable<Book> Get(
        BookFilter answerFilter,
        QueryOptions queryOptions = default) =>
    bookRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(answerFilter);

    public ValueTask<Book?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    bookRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<Book>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    bookRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    bookRepository.CheckByIdAsync(id, cancellationToken);

    public async ValueTask<Book> CreateAsync(
        Book book,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(
            book,
            options => options
            .IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        book.PublicationYear = DateTime.UtcNow.Year;
        book.ViewsCount = 0;
        book.PopularityScore = book.ViewsCount * 0.5 + book.PublicationYear * 2;

        return await bookRepository.CreateAsync(book, commandOptions, cancellationToken);
    }

    public async ValueTask<Book> UpdateAsync(
        Book book,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await validator.ValidateAsync(
            book,
            options => options
            .IncludeRuleSets(EntityEvent.OnUpdate.ToString()),
            cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingBook = await bookRepository.GetByIdAsync(book.Id) ?? throw new ArgumentNullException("This book doesn't exist");
        
        existingBook.UserId = book.UserId;
        existingBook.Title = book.Title;
        existingBook.Description = book.Description;
        existingBook.Author = book.Author;
        existingBook.PopularityScore = book.ViewsCount * 0.5 + book.PublicationYear * 2;

        return await bookRepository.UpdateAsync(existingBook, commandOptions, cancellationToken);
    }

    public ValueTask<Book?> DeleteAsync(
        Book book,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    bookRepository.DeleteAsync(book, commandOptions, cancellationToken);

    public ValueTask<Book?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    bookRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
