using SpotifyApp.Storage.Contracts.Models;

namespace SpotifyApp.Repositories.Contracts;

public interface IOAuthTokenReadRepository
{
    Task<OAuthToken?> GetLatestUserToken(CancellationToken cancellationToken);
}