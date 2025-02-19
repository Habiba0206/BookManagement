using BookManagement.Domain.Common.Commands;
using BookManagement.Domain.Common.Queries;
using BookManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using BookManagement.Persistence.Repositories.Interfaces;
using BookManagement.Persistence.Caching.Brokers;
using BookManagement.Persistence.DataContexts;

namespace BookManagement.Persistence.Repositories;

public class UserInfoVerificationCodeRepository(AppDbContext appDbContext, ICacheBroker cacheBroker) :
    EntityRepositoryBase<UserInfoVerificationCode, AppDbContext>(appDbContext, cacheBroker),
    IUserInfoVerificationCodeRepository

{
    public IQueryable<UserInfoVerificationCode> Get(
        Expression<Func<UserInfoVerificationCode, bool>>? predicate = null,
        QueryOptions queryOptions = default) =>
    base.Get(predicate, queryOptions);

    public ValueTask<UserInfoVerificationCode?> GetByIdAsync(
        Guid id,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdAsync(id, queryOptions, cancellationToken);

    public ValueTask<IList<UserInfoVerificationCode>> GetByIdsAsync(
        IEnumerable<Guid> ids,
        QueryOptions queryOptions = default,
        CancellationToken cancellationToken = default) =>
    base.GetByIdsAsync(ids, queryOptions, cancellationToken);

    public ValueTask<bool> CheckByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default) =>
    base.CheckByIdAsync(id, cancellationToken);

    public new async ValueTask<UserInfoVerificationCode> CreateAsync(
        UserInfoVerificationCode verificationCode,
        CommandOptions commandOptions = default,
        CancellationToken cancellationToken = default
    )
    {
        await DbContext.UserInfoVerificationCodes.Where(code => code.UserId == verificationCode.UserId && code.CodeType == verificationCode.CodeType)
            .ExecuteUpdateAsync(setter => setter.SetProperty(code => code.IsActive, false), cancellationToken);

        return await base.CreateAsync(verificationCode, commandOptions, cancellationToken);
    }

    public async ValueTask DeactivateAsync(Guid codeId, CommandOptions commandOptions, CancellationToken cancellationToken = default)
    {
        await DbContext.UserInfoVerificationCodes.Where(code => code.Id == codeId)
            .ExecuteUpdateAsync(setter => setter.SetProperty(code => code.IsActive, false), cancellationToken);

        if (!commandOptions.SkipSaveChanges)
            await DbContext.SaveChangesAsync(cancellationToken);
    }
}
