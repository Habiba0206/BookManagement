using BookManagement.Infrastructure.Identity.Validators;
using BookManagement.Application.Identity.Models;
using BookManagement.Application.Identity.Services;
using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using BookManagement.Domain.Enums;
using BookManagement.Persistence.Extensions;
using BookManagement.Persistence.Repositories.Interfaces;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookManagement.Infrastructure.Identity.Services;

public class UserService(
    IUserRepository userRepository,
    UserValidator validator)
   : IUserService
{
    public IQueryable<User> Get(
        Expression<Func<User, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    userRepository.Get(predicate, queryOptions);

    public IQueryable<User> Get(
        UserFilter userFilter,
        QueryOptions queryOptions = default) =>
    userRepository
        .Get(queryOptions: queryOptions)
        .ApplyPagination(userFilter);

    public ValueTask<User?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    userRepository.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<User>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    userRepository.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public async Task<Guid?> GetIdByEmailAddressAsync(
        string emailAddress,
        CancellationToken cancellationToken = default)
    {
        var userId = await Get(user => user.EmailAddress == emailAddress, new QueryOptions(QueryTrackingMode.AsNoTracking)).Select(user => user.Id).FirstOrDefaultAsync(cancellationToken);
        return userId != Guid.Empty ? userId : default(Guid?);
    }

    public async ValueTask<User> GetSystemUserAsync(
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default)
    {
        return await Get(user => user.Role == Role.System, queryOptions).FirstAsync(cancellationToken);
    }

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    userRepository.CheckByIdAsync(id, cancellationToken);

    public ValueTask<User> CreateAsync(
        User user,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = validator.Validate(user);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return userRepository.CreateAsync(user, commandOptions, cancellationToken);
    }

    public async ValueTask<User> UpdateAsync(
        User user,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default)
    {
        var validationResult = validator.Validate(user);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingUser = await userRepository.GetByIdAsync(user.Id, cancellationToken: cancellationToken);
        if (existingUser == null)
            throw new Exception("User not found");

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.EmailAddress = user.EmailAddress;
        existingUser.Age = user.Age;

        return await userRepository.UpdateAsync(existingUser, commandOptions, cancellationToken);
    }

    public ValueTask<User?> DeleteAsync(
        User user,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    userRepository.DeleteAsync(user, commandOptions, cancellationToken);

    public ValueTask<User?> DeleteByIdAsync(
        Guid id,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default) =>
    userRepository.DeleteByIdAsync(id, commandOptions, cancellationToken);
}
