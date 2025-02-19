using BookManagement.Domain.Entities;

namespace BookManagement.Application.Identity.Services;

public interface IAccessTokenGeneratorService
{
    AccessToken GetToken(User user);

    Guid GetTokenId(string accessToken);
}