using BookManagement.Domain.Enums;

namespace BookManagement.Application.Verification.Services;

public interface IVerificationCodeService
{
    ValueTask<VerificationType?> GetVerificationTypeAsync(string code, CancellationToken cancellationToken = default);
}
